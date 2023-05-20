using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadcrumbs : MonoBehaviour
{
    [SerializeField] private GameObject breadcrumb;
    [SerializeField] private GameObject allBread;
    [SerializeField] private GameObject placedAllBread;
    [SerializeField] private GameObject cam;
    private Vector3 currentPosition;
    private Vector3 prevCrumbPosition;
    static bool backtracingMode = false;
    private List<Vector3> crumbPositions = new();
    [SerializeField] float crumbDistance = 2f;

    void Start()
    {
        prevCrumbPosition = cam.transform.position;
        EventBus.Subscribe<BreadCrumbCollisionEvent>(BreadCrumbRemove);
        
    }

    void Update()
    {
        if (backtracingMode)
        {
            foreach (Transform crumbTransform in allBread.transform)
            {
                crumbTransform.Rotate(new Vector3(0f, 180f, 0f));
            }
        }
        currentPosition = cam.transform.position;
        float distanceMoved = Vector3.Distance(currentPosition, prevCrumbPosition);

        while (distanceMoved >= crumbDistance)
        {
            PlaceBreadcrumb();
            prevCrumbPosition = currentPosition;
            distanceMoved = Vector3.Distance(currentPosition, prevCrumbPosition);
        }
    }

    float offset = -0.1f;
    private void PlaceBreadcrumb()
    {
        Vector3 direction = prevCrumbPosition - currentPosition;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(90f, 0f, 0f);
        GameObject instantiated = Instantiate(breadcrumb, prevCrumbPosition + new Vector3(0, offset, 0), rotation);
       //crumbPositions.Insert(0, prevCrumbPosition); //List of positions for whenever needed
        instantiated.transform.SetParent(placedAllBread.transform);
        instantiated.transform.SetAsFirstSibling();
    }
    
    private void BreadCrumbRemove(BreadCrumbCollisionEvent e)
    {
        bool willBreak = false;
        for (int i = 0; i < e.breadCrumb.transform.parent.childCount; i++)
        {
            GameObject crumbChild = e.breadCrumb.transform.parent.GetChild(i).gameObject;

            willBreak = e.breadCrumb == crumbChild;
            /*
            if (e.breadCrumb.transform.parent.gameObject == placedAllBread.gameObject)
            {
                //Debug.Log(crumbPositions.Count);
                //crumbPositions.RemoveAt(i);
            }
            */

            Destroy(crumbChild.gameObject);

            

            if (willBreak) break;

        }
        
    }

    static public void SetBacktrackingMode(bool toggle)
    {
        backtracingMode = toggle;
    }
}

