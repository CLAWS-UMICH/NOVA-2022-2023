using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagingNewHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject messaging;
    void Start()
    {
        
    }
    public void CloseMessaging(){
        messaging.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
