using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;

public class ChatWindowInteractions : MonoBehaviour
{
    // Messaging Window related objects
    public GameObject MessagingWindow;
    public GameObject GridParent;
    public GridObjectCollection GridCollection;
    public ScrollingObjectCollection ScrollCollection;
    public ClippingBox Clipper;
    public GameObject OtherMessagePrefab;
    public GameObject SelfMessagePrefab;
    //FIXME add scrolling parent
    // Add message prefab

    public TextMeshPro recipientNames;
    public HashSet<string> recipientSet = new HashSet<string>();

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //Create new Chat
    public void CreateNewChat()
    {
        List<string> recipients = recipientSet.ToList();
        recipients.Sort();
        string chatID = string.Join("", recipients);
        if (Simulation.User.AstronautMessaging.chatLookup.ContainsKey(chatID))
        {
            // Pull up window with the existing chat rendered
            //RenderChatWindow(chatID);
        }
        Chat newChat = new Chat(chatID, recipientSet);

        //Add to astronaut class
        Simulation.User.AstronautMessaging.chatList.Add(newChat);
        int index = Simulation.User.AstronautMessaging.chatList.Count() - 1;
        Simulation.User.AstronautMessaging.chatLookup[chatID] = index;
        //Close contact list window
        //RenderChatWindow(chatID);
    }

    //FIXME
    public void RenderChatWindow()
    {
        string self = "Jason";
        HashSet<string> testMembers = new HashSet<string>();
        testMembers.Add("Joel");
        testMembers.Add("Jason");
        Chat testChat = new Chat("testChat", testMembers);
        testChat.messages.Add(new Message("testMessage1", "Joel", "1"));
        testChat.messages.Add(new Message("testMessage2", "Jason", "2"));
        testChat.messages.Add(new Message("testMessage3", "Joel", "3"));
        testChat.messages.Add(new Message("testMessage4", "Jason", "4"));

        // Set Chat window active
        MessagingWindow.SetActive(true);
        Debug.Log("ButtonWOrks");
        // Clear scrolling parent and create number of prefabs based on chat messages
        Transform grid = GridParent.transform;
        
        for (int i = 0; i < grid.childCount; ++i)
        {
            Debug.Log(i);
            ScrollCollection.RemoveItem(grid.GetChild(i).gameObject);
            GameObject.Destroy(grid.GetChild(i).gameObject);
        }
        Clipper.ClearRenderers();
        //foreach (Transform messageTrans in grid)
        //{
        //    ScrollCollection.RemoveItem(messageTrans.gameObject);
        //    GameObject.Destroy(messageTrans.gameObject);
        //}

        //// Somehow start scrolling from the bottom of the list
        foreach (Message msg in testChat.messages)
        {
            GameObject msgObj;
            if (msg.sender == self)
            {
                msgObj = Instantiate(SelfMessagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                msgObj = Instantiate(SelfMessagePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            ScrollCollection.AddContent(msgObj);
        }
        ScrollCollection.UpdateContent();
    }
    //FIXME
    public void EditChatName(string customName)
    {

    }

    // Function to display contents of the recipient list
    public void RecipientHandler(string name)
    {
        if (!recipientSet.Contains(name))
        {
            recipientSet.Add(name);
        }
        else
        {
            recipientSet.Remove(name);
        }

        recipientNames.text = string.Join(", ", recipientSet.ToList());
        return;
    }
}
