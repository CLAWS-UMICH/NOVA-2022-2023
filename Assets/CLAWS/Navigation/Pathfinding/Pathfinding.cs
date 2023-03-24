using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    Grid GridReference;//For referencing the grid class
    public Transform StartPosition;//Starting position to pathfind from
    public Transform TargetPosition;//Starting position to pathfind to
    public GameObject prefabToInstantiate;
    public GameObject empty;
    [SerializeField] float distance = 5f;


    private void Awake()//When the program starts
    {
        GridReference = GetComponent<Grid>();//Get a reference to the game manager
    }

    public void startPathFinding(Transform start, Transform end)
    {
        StartPosition = start;
        TargetPosition = end;

        FindNewPath();
    }

    // Button on screen will find the path everytime when pressed to not have it run every frame
    // Could maybe be changed to have it clicked once and always running, but for now, this will suffice
    public void FindNewPath()
    {
        FindPath(StartPosition.position, TargetPosition.position);//Find a path to the goal
    }

    /*
    private void Update()//Every frame
    {
        FindPath(StartPosition.position, TargetPosition.position);//Find a path to the goal
    }
    */

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Node StartNode = GridReference.NodeFromWorldPoint(a_StartPos);//Gets the node closest to the starting position
        Node TargetNode = GridReference.NodeFromWorldPoint(a_TargetPos);//Gets the node closest to the target position

        Heap<Node> OpenList = new Heap<Node>(GridReference.MaxSize);//List of nodes for the open list
        HashSet<Node> ClosedList = new HashSet<Node>();//Hashset of nodes for the closed list

        OpenList.Add(StartNode);//Add the starting node to the open list to begin the program

        while(OpenList.Count > 0)//Whilst there is something in the open list
        {
            Node CurrentNode = OpenList.RemoveFirst();//Create a node and set it to the first item in the open list
            /*
            for(int i = 1; i < OpenList.Count; i++)//Loop through the open list starting from the second object
            {
                if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].ihCost < CurrentNode.ihCost)//If the f cost of that object is less than or equal to the f cost of the current node
                {
                    CurrentNode = OpenList[i];//Set the current node to that object
                }
            }
            OpenList.Remove(CurrentNode);//Remove that from the open list
            */
            ClosedList.Add(CurrentNode);//And add it to the closed list

            if (CurrentNode == TargetNode)//If the current node is the same as the target node
            {
                GetFinalPath(StartNode, TargetNode);//Calculate the final path
            }

            foreach (Node NeighborNode in GridReference.GetNeighboringNodes(CurrentNode))//Loop through each neighbor of the current node
            {
                if (!NeighborNode.bIsWall || ClosedList.Contains(NeighborNode))//If the neighbor is a wall or has already been checked
                {
                    continue;//Skip it
                }
                int MoveCost = CurrentNode.igCost + GetEuclideanDistance(CurrentNode, NeighborNode);//Get the F cost of that neighbor

                if (MoveCost < NeighborNode.igCost || !OpenList.Contains(NeighborNode))//If the f cost is greater than the g cost or it is not in the open list
                {
                    NeighborNode.igCost = MoveCost;//Set the g cost to the f cost
                    NeighborNode.ihCost = GetEuclideanDistance(NeighborNode, TargetNode);//Set the h cost
                    NeighborNode.ParentNode = CurrentNode;//Set the parent of the node for retracing steps

                    if(!OpenList.Contains(NeighborNode))//If the neighbor is not in the openlist
                    {
                        OpenList.Add(NeighborNode);//Add it to the list
                    }
                }
            }

        }
    }



    void GetFinalPath(Node a_StartingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();//List to hold the path sequentially 
        Node CurrentNode = a_EndNode;//Node to store the current node being checked

        while(CurrentNode != a_StartingNode)//While loop to work through each node going through the parents to the beginning of the path
        {
            FinalPath.Add(CurrentNode);//Add that node to the final path
            CurrentNode = CurrentNode.ParentNode;//Move onto its parent node

        }

        FinalPath.Reverse();//Reverse the path to get the correct order

        GridReference.FinalPath = FinalPath;//Set the final path

        // Instantiate objects along the final path
        for (int i = 0; i < FinalPath.Count; i++)
        {
            FinalPath[i].vPosition.y -= .1f;
            GameObject instantiatedObject = Instantiate(prefabToInstantiate, FinalPath[i].vPosition, Quaternion.identity);
            if (i != FinalPath.Count - 1)
            {
                Vector3 direction = FinalPath[i + 1].vPosition - FinalPath[i].vPosition;
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(90f, 0f, 0f);
                instantiatedObject.transform.rotation = rotation;
            }
            else if (i == FinalPath.Count - 1)
            {
                Vector3 direction = TargetPosition.position - FinalPath[i].vPosition;
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(90f, 0f, 0f);
                instantiatedObject.transform.rotation = rotation;
            }
            else
            {
                instantiatedObject.transform.LookAt(FinalPath[i].vPosition);
            }
            instantiatedObject.SetActive(false);
            isClose(instantiatedObject);
            instantiatedObject.transform.SetParent(empty.transform);
        }
    }

    void isClose(GameObject objectIn)
    {
        float x = Vector3.Distance(StartPosition.position, objectIn.transform.position);
        if (distance > x)
        {
            objectIn.SetActive(true);
        }
    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.iGridX - a_nodeB.iGridX);//x1-x2
        int iy = Mathf.Abs(a_nodeA.iGridY - a_nodeB.iGridY);//y1-y2

        return ix + iy;//Return the sum
    }

    int GetEuclideanDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.iGridX - nodeB.iGridX);
        int dstY = Mathf.Abs(nodeA.iGridY - nodeB.iGridY);
        return Mathf.RoundToInt(Mathf.Sqrt(dstX * dstX + dstY * dstY));
    }
}
