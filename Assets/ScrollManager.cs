using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;


public class ScrollManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Children;
    public GameObject MessageOutPrefab;
    public GameObject MessageInPrefab;
    int firstMessage;

    public GameObject speech;
    public TextMeshPro panel;
    public GameObject backplate;
    string message;
    private IEnumerator coroutine;
    //GameObject gridObjectCollection;

    void Start()
    {
        EventBus.Subscribe<ScrollEvent>(ScrollDown);
        EventBus.Subscribe<ScrollEvent>(ScrollUp);
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

    public void GetMSG(){
        GameObject msg = Instantiate(MessageInPrefab, transform);
        msg.SetActive(false);
        msg.GetComponent<TextHandler>().SetText("Walking over to station A right now.");
        Children.Add(msg);
    }

    
    public void ScrollDown(ScrollEvent e){
        Debug.Log(firstMessage);
        e.direction = Direction.down;
        e.screen = Screens.Messaging;
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

    public void ScrollDown(){
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

    public void ScrollUp(ScrollEvent e){ 
        e.direction = Direction.up;
        e.screen = Screens.Messaging;
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

    public void ScrollUp(){ 
        
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

    public void recordMessage() {
        speech.SetActive(true);
        message = speech.GetComponent<SpeechManager>().GetMessage();
        bool active = true;
        coroutine = StartListeningMSG(active);
        StartCoroutine(coroutine);
        speech.SetActive(false);
    }

    IEnumerator StartListeningMSG(bool active){
        int i = 0;
        string prevMessage = message;
        bool speaking = false;
        while(active){
            yield return new WaitForSeconds(1.5f);
            message = speech.GetComponent<SpeechManager>().GetMessage();
            i++;
            if(message!=prevMessage){
                speaking = true;
                panel.text = message;
                //add something here to update text box with message text
            }
            if(i==3 && speaking){
                i = 0;
                speaking = false;
            }
            else if(i==3 && !speaking){
                i = 0;
                speaking = false;
                Debug.Log("message: " + message);
                SendMSG(message);
                panel.text = "";
                backplate.SetActive(false);
                ScrollUp();
                
                //finished speaking so stop recording. store message as description
                active = false;
            }
            prevMessage = message;
        }  
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
