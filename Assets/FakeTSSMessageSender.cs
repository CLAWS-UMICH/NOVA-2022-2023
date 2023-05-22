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
        StartCoroutine(FakeVitals2());
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
        fakeVitals.oxygen_primary_time = 1000;
        fakeVitals.oxygen_secondary_time = 1000;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            fakeVitals.time++;
            fakeVitals.oxygen_primary_time--;
            fakeVitals.h2o_time_left = getTime(2000 - fakeVitals.time);

            fakeVitals.timer = getTime(fakeVitals.time);

            Simulation.User.EVA = fakeVitals;
            EventBus.Publish<VitalsUpdatedEvent>(new VitalsUpdatedEvent());
        }
    }

    IEnumerator FakeVitals2()
    {
        
        fakeVitals.battery_output = 2;
        fakeVitals.battery_percentage = 100;

        while (true)
        {
             
            yield return new WaitForSeconds(2f);
        }
    }

    string getTime(float time)
    {
        var ss = (((int)fakeVitals.time) % 60).ToString("00");
        var mm = (Math.Floor(fakeVitals.time / 60) % 60).ToString("00");
        var hh = Math.Floor(fakeVitals.time / 60 / 60).ToString("00");
        return hh.ToString() + ":" + mm.ToString() + ":" + ss.ToString();
    }

}
