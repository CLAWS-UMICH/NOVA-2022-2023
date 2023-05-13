using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

public class FakeTSSMessageSender : MonoBehaviour
{
    public UIAMsg fakeUIA = new UIAMsg();


    public void Fake_SetUIAMsg()
    {
        Simulation.User.UIA = fakeUIA;
        EventBus.Publish<UIAMsgEvent>(new UIAMsgEvent());
    }
}
