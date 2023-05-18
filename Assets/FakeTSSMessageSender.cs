using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

public class FakeTSSMessageSender : MonoBehaviour
{
    public UIAMsg fakeUIA = new UIAMsg();
    public UIAControlMsg fakeUIAControl = new UIAControlMsg();

    public GPSMsg fakeGPS = new GPSMsg();

    private void Start()
    {
        Fake_SetUIA();
    }   

    public void Fake_SetUIA()
    {
        Simulation.User.UIA = fakeUIA;
        Simulation.User.UIA_CONTROLS = fakeUIAControl;
        EventBus.Publish<UIAMsgEvent>(new UIAMsgEvent());
    }

    public void Fake_SetGPS()
    {
        Simulation.User.GPS = fakeGPS;
        EventBus.Publish<UpdatedGPSEvent>(new UpdatedGPSEvent());
    }

}
