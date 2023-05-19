using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;


public class ScrollManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Children;
    public GameObject MessageOutPrefab;
    int firstMessage;
    //GameObject gridObjectCollection;

    void Start()
    {
        foreach (Transform child in transform)
        {
           Children.Add(child.gameObject);
           firstMessage = 0;
        }

        //gridObjectCollection = GetComponent<GridObjectCollection>();

    }

    public void SendMSG(string message){
        GameObject msg = Instantiate(MessageOutPrefab, transform);
        msg.SetActive(false);
        msg.GetComponent<TextHandler>().SetText(message);
        Children.Add(msg);
    }

    public void ScrollDown(string message){
        Debug.Log(firstMessage);
        // GameObject msg = Instantiate(MessageOutPrefab, transform);
        // msg.GetComponent<TextHandler>().SetText(message);
        // Children.Add(msg);
        int len = Children.Count;
        if(firstMessage+3 < len  && firstMessage >=0){
            if(firstMessage != 0){
                Debug.Log("firstMessage");
                Children[firstMessage-1].SetActive(true);
                Children[firstMessage+3].SetActive(false);
                firstMessage--;
                transform.GetComponent<GridObjectCollection>().UpdateCollection();
            }
        }
        
        // int len = Children.Count;
        // Children[len-1].SetActive(false);
        // Children[len-4].SetActive(true);
        
    }

    public void ScrollUp(string message){ 
        string temp = "hi";
        Debug.Log(firstMessage);
        
        int len = Children.Count;
        if(firstMessage+4 < len && firstMessage >=0){
            Children[firstMessage].SetActive(false);
            Children[firstMessage+4].SetActive(true);
            // if(firstMessage != 0){
            //     Children[firstMessage-1].SetActive(false);
            // }
            firstMessage++;
            transform.GetComponent<GridObjectCollection>().UpdateCollection();
        }
        
        //msg.

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
