using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;
using System;
public class FakeTSSMessageSender : MonoBehaviour
{
    public UIAMsg fakeUIA = new UIAMsg();
    public SpecMsg fakeSpecMsg = new SpecMsg();
    public GPSMsg fakeGPS = new GPSMsg();
    public SimulationStates fakeVitals = new SimulationStates();

    private void Start()
    {
        Fake_SetUIA();

        StartCoroutine(FakeVitals());
    }
    [ContextMenu("SetUIA")]
    public void Fake_SetUIA()
    {
        Simulation.User.UIA = fakeUIA;
        EventBus.Publish<UIAMsgEvent>(new UIAMsgEvent());
    }
    [ContextMenu("SetGPS")]
    public void Fake_SetGPS()
    {
        Simulation.User.GPS = fakeGPS;
        EventBus.Publish<UpdatedGPSEvent>(new UpdatedGPSEvent());
    }
    [ContextMenu("SetSpectrometer")]
    public void Fake_SetSpectrometer() {
        Simulation.User.GEO = fakeSpecMsg;
        EventBus.Publish<GeoSpecRecievedEvent>(new GeoSpecRecievedEvent());
    }

    IEnumerator FakeVitals()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            fakeVitals.time++;
            var totalSeconds = 228.10803;
            var ss = (((int)totalSeconds) % 60).ToString("00");
            var mm = (Math.Floor(totalSeconds / 60) % 60).ToString("00");
            var hh = Math.Floor(totalSeconds / 60 / 60).ToString("00");
            fakeVitals.timer = hh.ToString() + ":" + mm.ToString() + ":" + ss.ToString();
        }
    }

}
