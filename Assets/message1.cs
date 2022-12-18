using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class message1 : MonoBehaviour
{

    [SerializeField] TextMeshPro windowsTmp;
    [SerializeField] string buttonTmp;
    /*
     From prefab. Once we get the reference by dragging, it refers to that GameObject in the prefab.
     Any changes on message will be actually saved in that prefab GameObject.

     For example, if we change the TextMeshPro to "now is 2" by message, that GameObject (prototype) 
     will save and keep "now is 2" as the text and will not be renewed even you exit the game mode. 
     
     So, if you modify the message here, you modify the prototype in prefab permanently. 
    */
    [SerializeField] GameObject message; // from prefab



    public void click() {


        PositionOfMessage p = new PositionOfMessage();
        // windowsTmp.text = buttonTmp; 
        // Instantiate at position (0, 0, 0) and zero rotation.
        // messageObject gets the real instance and message is the prototype in prefab
        GameObject messageObject = Instantiate(message, p.getPosition(), Quaternion.identity);
        // Debug.Log(buttonTmp);

        // 1. Set up the first kind of messageObject: messageBox
        GameObject messageBox = messageObject.transform.GetChild(0).gameObject;; // from prefab
        // To find `child1` which is the first index(0)
        // Get the GameObject Text(TMP) from the box
        GameObject messageContent = messageBox.transform.GetChild(0).gameObject;
        // Get the Text(TMP)'s component
        TextMeshPro tmp = messageContent.GetComponent<TextMeshPro>();
        // Debug.Log(messageObject.GetComponent<Transform>().position);
        tmp.text = buttonTmp; 
        Debug.Log(tmp.text);
        // 2. Set up the second kid of messageObject: Person

    }

}

    
    /*
        Test the idea about the diff between message and messageObject
    */
    // int count = 0;
    // public void click() {

    //     // when count is 2, change the prototype so the rest of the instance will have "now is 2".
    //     if (count == 2) {
    //         // create distance for the next box
    //     // GameObject messageBox = messageObject.transform.GetChild(0).gameObject;; // from prefab
    //     GameObject messageBox = message.transform.GetChild(0).gameObject;; // from prefab
    //     // To find `child1` which is the first index(0)
    //     // Get the GameObject Text(TMP)
    //     GameObject messageContent = messageBox.transform.GetChild(0).gameObject;
    //     // Get the Text(TMP)'s component
    //     TextMeshPro tmp = messageContent.GetComponent<TextMeshPro>();
        
    //     tmp.text = "now is 2"; 
    //     }
    //     count++;
    //     Debug.Log(count);

    //     GameObject messageObject = Instantiate(message, PositionOfMessage.getPosition(), Quaternion.identity);
    // }


