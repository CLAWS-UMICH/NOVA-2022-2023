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
    /*
    public GameObject MessagingWindow;
    public GameObject GridParent;
    public GridObjectCollection GridCollection;
    public ScrollingObjectCollection ScrollCollection;
    public ClippingBox Clipper;
    public GameObject OtherMessagePrefab;
    public GameObject SelfMessagePrefab;
    */

    //FIXME add scrolling parent
    // Add message prefab
    public GameObject MessagingWindow;
    public TextMeshPro recipientNames;
    public HashSet<string> recipientSet = new HashSet<string>();
    public GameObject[] taskObjects;
    int currentIndex;
    string chatID;
    Chat currentChat;

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
        RenderChatWindow(chatID);
    }

    //Increment
        //Update currentIndex
        //Call updateText

    void incrementIndex(int incr) //incr is a 1 or -1
    {
        int indexIntoDictionary = Simulation.User.AstronautMessaging.chatLookup[chatID];
        List<Message> messagesList = Simulation.User.AstronautMessaging.chatList[indexIntoDictionary].messages;
        if (currentIndex == 2 && incr == -1) //At the top and trying to go up
        {
            updateText(currentIndex);
        }
        else if (currentIndex == messagesList.Count - 1 && incr == 1) //At the bottom and trying to go down
        {
            updateText(currentIndex);
        } 
        else
        {
            if (incr == -1)
            {
                currentIndex += 1;
                updateText(currentIndex);
            }
            else
            {
                currentIndex -= 1;
                updateText(currentIndex);
            }
        }
    }

    //FIXME
    public void RenderChatWindow(string chatID) //Pull the chat history 
        //Determine currentIndex - Edge case where less than 3 messages, edge cases where can't scroll pass bounds
        //Use currentIndex to do updateText
        //Saved chatID as member variable
        //Do updateText Function
        //Connect button to this function (input is testChat)
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
        int indexIntoDictionary = Simulation.User.AstronautMessaging.chatLookup[chatID];
        List<Message> messagesList = Simulation.User.AstronautMessaging.chatList[indexIntoDictionary].messages;
        currentIndex = messagesList.Count - 1;
        updateText(currentIndex);

        // Set Chat window active
        MessagingWindow.SetActive(true);
        Debug.Log("ButtonWOrks");
        // Clear scrolling parent and create number of prefabs based on chat messages

    }

    void updateText(int currentIndex) //Assume valid currentIndex -Less than 3 messages, handle here
    {
        int indexIntoDictionary = Simulation.User.AstronautMessaging.chatLookup[chatID];
        List<Message> messagesList = Simulation.User.AstronautMessaging.chatList[indexIntoDictionary].messages;
        if (messagesList.Count == 1) //Edge case where only 1 message
        {
            Message mostRecentMessage = messagesList[currentIndex];
            taskObjects[0].transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = mostRecentMessage.sender;
            taskObjects[0].transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = mostRecentMessage.text;
        }
        else if (messagesList.Count == 2) //Edge case where only 2 messages
        {
            Message mostRecentMessage = messagesList[currentIndex];
            Message secondMostRecentMessage = messagesList[currentIndex - 1];
            taskObjects[0].transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = secondMostRecentMessage.sender;
            taskObjects[0].transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = secondMostRecentMessage.text;
            taskObjects[1].transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = mostRecentMessage.sender;
            taskObjects[1].transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = mostRecentMessage.text;
        }
        else
        {
            Message mostRecentMessage = messagesList[currentIndex];
            Message secondMostRecentMessage = messagesList[currentIndex - 1];
            Message thirdMostRecentMessage = messagesList[currentIndex - 2];
            taskObjects[0].transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = thirdMostRecentMessage.sender;
            taskObjects[0].transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = thirdMostRecentMessage.text;
            taskObjects[1].transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = secondMostRecentMessage.sender;
            taskObjects[1].transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = secondMostRecentMessage.text;
            taskObjects[2].transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = mostRecentMessage.sender;
            taskObjects[2].transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = mostRecentMessage.text;
        }
        
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
