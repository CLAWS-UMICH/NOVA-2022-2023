using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagingNewHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject messaging;
    public GameObject speech;
    public GameObject panel;
    string message;
    private IEnumerator coroutine;

    void Start()
    {
        
    }
    public void CloseMessaging(){
        messaging.SetActive(false);
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
            yield return new WaitForSeconds(0.8f);
            message = speech.GetComponent<SpeechManager>().GetMessage();
            i++;
            if(message!=prevMessage){
                speaking = true;
                //panel.text = message;
                //add something here to update text box with message text
            }
            if(i==3 && speaking){
                i = 0;
                speaking = false;
            }
            else if(i==3 && !speaking){
                i = 0;
                speaking = false;
                //transform.SendMSG(message);
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
