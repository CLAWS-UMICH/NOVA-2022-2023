using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagingNewHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject messaging;
    public GameObject messagingInbox;
    public GameObject JaneScreen;
    public GameObject JaneContents;
    public GameObject JaneGrid;
    public GameObject NielScreen;
    public GameObject NielContents;
    public GameObject NielGrid;
    public GameObject MccScreen;
    public GameObject MccContents;
    public GameObject MccGrid;
    public GameObject ScrollManager;
    private IEnumerator reply;

    void Start()
    {
        EventBus.Subscribe<CloseEvent>(Callback_CloseMessaging);
        reply = FiveMinuteReply(true);
        StartCoroutine(reply);
        
    }

    IEnumerator CloseChildren(GameObject child)
    {
        yield return new WaitForSeconds(1f);
        child.SetActive(false);
    }

    public void Callback_CloseMessaging(CloseEvent e){
        if (e.screen == Screens.Messaging || e.screen == Screens.Messaging_MCC 
            || e.screen == Screens.Messaging_Jane || e.screen == Screens.Messaging_Neil) 
        {
            CloseMessaging();

        }
    }

    public void CloseMessaging() {
        for (int a = 0; a < transform.childCount; a++)
        {
            StartCoroutine(CloseChildren(transform.GetChild(a).gameObject));
        }
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Home, LUNAState.center));

    }

    public void OpenMessaging(CloseEvent e){
        e.screen = Screens.Messaging;

         for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }
    }

    public void Recorder(){
        if(JaneScreen.activeSelf){
            JaneGrid.GetComponent<ScrollManager>().recordMessage();
        }
        else if(NielScreen.activeSelf){
            NielGrid.GetComponent<ScrollManager>().recordMessage();
        }
        else if(MccScreen.activeSelf){
            MccGrid.GetComponent<ScrollManager>().recordMessage();
        }
    }

    public void Jane(){
        messagingInbox.SetActive(false);
        JaneContents.SetActive(true);
        JaneScreen.SetActive(true);
    }
    public void Niel(){
        messagingInbox.SetActive(false);
        NielContents.SetActive(true);
        NielScreen.SetActive(true);
    }
    public void MCC(){
        messagingInbox.SetActive(false);
        MccContents.SetActive(true);
        MccScreen.SetActive(true);
    }
    
    IEnumerator FiveMinuteReply(bool active){
        while(active){
            yield return new WaitForSeconds(30f); //change to 600 -- 10 minutes
            ScrollManager.GetComponent<ScrollManager>().GetMSG();
            ScrollManager.GetComponent<ScrollManager>().ScrollUp();
            PopUpManager.MakePopupMessaging("Walking over to station A right now.",3);
            active = false;

        }  
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
