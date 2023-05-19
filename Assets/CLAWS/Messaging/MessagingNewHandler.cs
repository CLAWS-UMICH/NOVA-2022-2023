using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagingNewHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject messaging;
    public GameObject ScrollManager;
    private IEnumerator reply;

    void Start()
    {
        reply = FiveMinuteReply(true);
        StartCoroutine(reply);
        
    }
    public void CloseMessaging(){
        messaging.SetActive(false);
    }
    
    IEnumerator FiveMinuteReply(bool active){
        while(active){
            yield return new WaitForSeconds(10f); //change to 600 -- 10 minutes
            ScrollManager.GetComponent<ScrollManager>().GetMSG();
            ScrollManager.GetComponent<ScrollManager>().ScrollUp();
            PopUpManager.MakePopup("Walking over to station A right now.");
            active = false;

        }  
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
