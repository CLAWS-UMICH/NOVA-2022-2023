using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagingNewHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject closeButton;
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
        EventBus.Subscribe<BackEvent>(Callback_BackMessaging);
        reply = FiveMinuteReply(true);
        StartCoroutine(reply);
        
    }

    IEnumerator CloseChildren(GameObject child)
    {
        yield return new WaitForSeconds(1f);
        child.SetActive(false);
    }

    IEnumerator OpenChildren(GameObject child)
    {
        yield return new WaitForSeconds(1f);
        child.SetActive(true);
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

    public void OpenMessaging(){
        StartCoroutine(OpenChildren(messagingInbox));
        StartCoroutine(OpenChildren(closeButton));
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Messaging, LUNAState.center));
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
        StartCoroutine(CloseChildren(messagingInbox));
        StartCoroutine(OpenChildren(JaneContents));
        StartCoroutine(OpenChildren(JaneScreen));
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Messaging_Jane, LUNAState.center));
    }
    public void Niel(){
        StartCoroutine(CloseChildren(messagingInbox));
        StartCoroutine(OpenChildren(NielContents));
        StartCoroutine(OpenChildren(NielScreen));
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Messaging_Neil, LUNAState.center));
    }
    public void MCC(){
        StartCoroutine(CloseChildren(messagingInbox));
        StartCoroutine(OpenChildren(MccContents));
        StartCoroutine(OpenChildren(MccScreen));
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Messaging_MCC, LUNAState.center));
    }

    public void Callback_BackMessaging(BackEvent e){
        if (e.screen == Screens.Messaging_MCC) {
            backMCC();
        }
        else if(e.screen == Screens.Messaging_Jane){
            backJane();
        }
        else if(e.screen == Screens.Messaging_Neil){
            backNiel();
        }
    }

    public void backJane(){
        StartCoroutine(OpenChildren(messagingInbox));
        StartCoroutine(CloseChildren(JaneScreen));
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Messaging, LUNAState.center));
    }
    public void backNiel(){
        StartCoroutine(OpenChildren(messagingInbox));
        StartCoroutine(CloseChildren(NielScreen));
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Messaging, LUNAState.center));
    }
    public void backMCC(){
        StartCoroutine(OpenChildren(messagingInbox));
        StartCoroutine(CloseChildren(MccScreen));
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Messaging, LUNAState.center));
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
