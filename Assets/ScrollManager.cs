using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;


public class ScrollManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Children;
    public GameObject MessageOutPrefab;
    //GameObject gridObjectCollection;

    void Start()
    {
        foreach (Transform child in transform)
        {
           Children.Add(child.gameObject);
        }

        //gridObjectCollection = GetComponent<GridObjectCollection>();

    }

    public void ScrollDown(string message){
        GameObject msg = Instantiate(MessageOutPrefab, transform);
        msg.GetComponent<TextHandler>().SetText(message);
        Children.Add(msg);
        int len = Children.Count;
        Children[len-1].SetActive(false);
        Children[len-4].SetActive(true);
        transform.GetComponent<GridObjectCollection>().UpdateCollection();
    }

    public void ScrollUp(string message){ 
        string temp = "hi";
        Debug.Log("scroll up");
        GameObject msg = Instantiate(MessageOutPrefab, transform);
        msg.GetComponent<TextHandler>().SetText(message);
        Children.Add(msg);
        int len = Children.Count;
        Children[len-4].SetActive(false);
        transform.GetComponent<GridObjectCollection>().UpdateCollection();
        //msg.

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
