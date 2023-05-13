using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

public class FakeTSSMessageSender : MonoBehaviour
{
    public UIAMsg fakemsg = new UIAMsg();


    public void Fake_SetUIAMsg()
    {
        Simulation.User.UIA = fakemsg;
        EventBus.Publish<UIAMsgEvent>(new UIAMsgEvent());
    }
}
