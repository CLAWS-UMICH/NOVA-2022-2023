using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;

public class ChatWindowInteractions : MonoBehaviour
{
    public MessageHandler Sender;
    public InboxScroll inbox;
    public GameObject MessagingWindow;
    public TextMeshPro recipientNames;
    public TextMeshPro testpreview;
    public TextMeshPro chatTitle;
    public HashSet<string> recipientSet = new HashSet<string>();
    public GameObject[] messageObjects;
    public GameObject inboxWindow;
    int currentMessage = 3;
    int currentIndex;
    string chatID = null;
    List<Message> displayedMessages = new List<Message>();
    Chat currentChat;
    public string self = "Jason";
    //FIxME add sorting of chat messages based on timestamp

    private void Start()
    {
        self = Sender.self;
    }

    //Create new Chat
    public void CreateNewChat()
    {
        recipientSet.Add(self);
        List<string> recipients = recipientSet.ToList();
        recipients.Sort();
        string chatID = string.Join("", recipients);
        // Send created groupchat message
        if (recipients.Count > 2)
        {
            Sender.CreateGroupChat(chatID, recipientSet);
        }
        Debug.Log(chatID);
        Chat newChat = new Chat(chatID, recipientSet);
        //Reset the contact window screen and reset selection
        this.recipientSet = new HashSet<string>();
        this.recipientNames.text = "";

        if (Simulation.User.AstronautMessaging.chatLookup.Contains(chatID))
        {
            // Pull up window with the existing chat rendered
            RenderChatWindow(chatID);
            return;
        }

        //Add to astronaut class
        Simulation.User.AstronautMessaging.chatList.Add(newChat);
        Simulation.User.AstronautMessaging.chatLookup.Add(chatID);
        //FiXME send chat creation message
        //Close contact list window (dont need for now)
        RenderChatWindow(chatID);
    }

    public void IncrementIndex(int incr) //incr is a 1 or -1
    {
        int indexIntoDictionary = GetChatIndex(chatID);
        List<Message> messagesList = Simulation.User.AstronautMessaging.chatList[indexIntoDictionary].messages;
        if ((incr < 0 && currentIndex > 2) || (incr > 0 && currentIndex < messagesList.Count - 1))
        {
            currentIndex += incr;
            updateText();
        }
    }

    public void updateCurrentIndex()
    {
        int index = GetChatIndex(this.chatID);
        this.currentChat = Simulation.User.AstronautMessaging.chatList[index];
        Debug.Log(this.currentChat.messages.Count);
        this.currentIndex = Math.Max(this.currentChat.messages.Count - 1, -1);
    }

    //FIXME
    public void RenderChatWindow(string chatID)
    {
        this.chatID = chatID;
        MessagingWindow.SetActive(true);
        int index = GetChatIndex(chatID);
        this.currentChat = Simulation.User.AstronautMessaging.chatList[index];
        chatTitle.text = this.currentChat.title;
        Debug.Log(this.currentChat.title);
        currentIndex = Math.Max(this.currentChat.messages.Count - 1, -1);
        Debug.Log(currentIndex);
        updateText();
    }

    public void UpdateDisplayList()
    {
        displayedMessages = new List<Message>();
        for (int i = currentIndex; i > currentIndex - 3; --i)
        {
            if (i >= 0)
            {
                displayedMessages.Add(currentChat.messages[i]);
            }
        }
    }

    public void updateText() //Assume valid currentIndex -Less than 3 messages, handle here
    {
        UpdateDisplayList();
        for (int i = 0; i < 3; i++)
        {
            messageObjects[i].SetActive(false);
        }
        for (int i = 0; i < displayedMessages.Count; ++i)
        {
            messageObjects[i].SetActive(true);
            messageObjects[i].transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = displayedMessages[i].sender;
            messageObjects[i].transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = displayedMessages[i].content + "\n" + displayedMessages[i].timeStamp;

        }
        for (int i = displayedMessages.Count; i < 3; i++)
        {
            messageObjects[i].SetActive(false);
        }
    }

    public void EditChatName(string customName)
    {
        if(this.chatID != null)
        {
            int index = GetChatIndex(chatID);
            Simulation.User.AstronautMessaging.chatList[index].title = customName;
            this.currentChat = Simulation.User.AstronautMessaging.chatList[index];
            chatTitle.text = this.currentChat.title;
        }
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

    public void TestMessageButton(int template)
    {
        this.currentMessage = template;
        testpreview.text = template.ToString();
    }

    public void SendButton()
    {
        List<string> memberCopy = currentChat.members;
        memberCopy.Remove(self);
        Sender.SendDM(this.currentMessage, this.chatID, memberCopy);
        Debug.Log("Exits from sendDM");
        string testmsg = "youmessedup";
        switch (this.currentMessage)
        {
            case 0:
                testmsg = "Hello";
                break;
            case 1:
                testmsg = "Goodbye";
                break;
            case 2:
                testmsg = "pls";
                break;
        }
        Message newMsg = new Message(testmsg, this.self, DateTime.Now.ToString("HH:mm"));
        newMsg.chatID = this.chatID;
        int index = GetChatIndex(this.chatID);
        Simulation.User.AstronautMessaging.chatList[index].messages.Add(newMsg);
        updateCurrentIndex();
        Debug.Log("Exits from updatecurrent index");
        updateText();
    }

    public void OnMessageRecieved(string chatID, Message msg)
    {
        if (!Simulation.User.AstronautMessaging.chatLookup.Contains(chatID))
        {
            //Make chat
            HashSet<string> members = new HashSet<string>() { msg.sender };
            Chat newChat = new Chat(chatID, members);
            newChat.messages.Add(msg);
            Simulation.User.AstronautMessaging.chatList.Add(newChat);
            Simulation.User.AstronautMessaging.chatLookup.Add(chatID);
        }
        else
        {
            int index = GetChatIndex(chatID);
            Chat targetChat = Simulation.User.AstronautMessaging.chatList[index];
            Simulation.User.AstronautMessaging.chatList[index].messages.Add(msg);
            Simulation.User.AstronautMessaging.chatList.Remove(targetChat);
            Simulation.User.AstronautMessaging.chatList.Add(targetChat);
        }
        if (inboxWindow.activeSelf)
        {
            //refresh inbox window if active
            inbox.ResetCurrentIndex();
            inbox.UpdateDisplayList();
            inbox.RenderInbox();
        }
        else if ((chatID == this.chatID) && !inboxWindow.activeSelf)
        {
            updateCurrentIndex();
            updateText();
        }
    }

    public void CreateGroup(GroupClass group)
    {
        if (!Simulation.User.AstronautMessaging.chatLookup.Contains(chatID))
        {
            Chat groupChat = new Chat(group.chatID, group.recipients);
            Simulation.User.AstronautMessaging.chatList.Add(groupChat);
            Simulation.User.AstronautMessaging.chatLookup.Add(group.chatID);
        }
    }

    public int GetChatIndex(string chatID)
    {
        for (int i = 0; i < Simulation.User.AstronautMessaging.chatList.Count; ++i)
        {
            if (Simulation.User.AstronautMessaging.chatList[i].chatID == chatID)
            {
                return i;
            }   
        }
        return -1;
    }
}
