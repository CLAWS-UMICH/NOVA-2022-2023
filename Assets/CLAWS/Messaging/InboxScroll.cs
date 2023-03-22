using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InboxScroll : MonoBehaviour
{
    public ChatWindowInteractions chatWindow;
    public GameObject[] chatObjects;
    List<Chat> displayedChats = new List<Chat>();
    int currentIndex;

    void Start()
    {
        ResetCurrentIndex();
        UpdateDisplayList();
        RenderInbox();
    }

    //FIXME need event system for new chats
    public void IncrementIndex(int incr) //incr is a 1 or -1
    {
        int messagesCount = Simulation.User.AstronautMessaging.chatList.Count;
        if ((incr < 0 && currentIndex > 2) || (incr > 0 && currentIndex < messagesCount - 1))
        {
            currentIndex += incr;
            ResetCurrentIndex();
            UpdateDisplayList();
            RenderInbox();
        }
    }

    public void UpdateDisplayList()
    {
        Debug.Log("CurrentINDEX");
        Debug.Log(currentIndex);
        displayedChats = new List<Chat>();
        for (int i = currentIndex; i > currentIndex - 3; --i)
        {
            if (i >= 0)
            {
                displayedChats.Add(Simulation.User.AstronautMessaging.chatList[i]);
            }
        }
    }

    public void ResetCurrentIndex()
    {
        currentIndex = Math.Max(Simulation.User.AstronautMessaging.chatList.Count - 1, -1);
    }

    public void RenderInbox()//Assume valid currentIndex -Less than 3 messages, handle here
    {
        Debug.Log("PrINTINGINGINGdiSNGID");
        Debug.Log(displayedChats.Count);
        for (int i = 0; i < displayedChats.Count; ++i)
        {
            int last = displayedChats[i].messages.Count - 1;
            chatObjects[i].SetActive(true);
            chatObjects[i].transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshPro>().text = displayedChats[i].title;
            if (last > 0)
            {
                chatObjects[i].transform.GetChild(3).GetChild(1).gameObject.GetComponent<TextMeshPro>().text = displayedChats[i].messages[last].content;
            }
        }
        for (int i = displayedChats.Count; i < 3; i++)
        {
            chatObjects[i].SetActive(false);
        }
    }

    public void ClickOnChat(int index)
    {
        // Deactivate inbox
        //set chat window active
        // Callback chat render
        this.chatWindow.RenderChatWindow(displayedChats[index].chatID);
    }
}
