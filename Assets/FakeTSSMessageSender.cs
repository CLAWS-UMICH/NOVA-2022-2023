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

        // StartCoroutine(FakeVitals());
        // StartCoroutine(FakeVitals2());
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
        fakeVitals.oxygen_primary_time = 100;
        fakeVitals.oxygen_secondary_time = 100;
        fakeVitals.secondary_oxygen = 99;
        fakeVitals.primary_oxygen = 100;
        fakeVitals.water_capacity = 100;

        fakeVitals.battery_capacity = 100;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            fakeVitals.time++;
            fakeVitals.h2o_time_left = getTime(2000 - fakeVitals.time);
            fakeVitals.o2_time_left = getTime(2000 - fakeVitals.time);

            fakeVitals.timer = getTime(fakeVitals.time);
            fakeVitals.temperature = UnityEngine.Random.Range(59, 70);
            fakeVitals.fan_tachometer = UnityEngine.Random.Range(3021, 4521);
            fakeVitals.heart_rate = UnityEngine.Random.Range(70, 100);
            fakeVitals.h2o_gas_pressure = UnityEngine.Random.Range(70, 100);
            fakeVitals.h2o_liquid_pressure = UnityEngine.Random.Range(70, 100);
            fakeVitals.sub_pressure = UnityEngine.Random.Range(14, 20);
            fakeVitals.suit_pressure = UnityEngine.Random.Range(14, 20);


            Simulation.User.EVA = fakeVitals;
            EventBus.Publish<VitalsUpdatedEvent>(new VitalsUpdatedEvent());
        }
    }

    IEnumerator FakeVitals2()
    {
        fakeVitals.battery_output = 0.3f;
        fakeVitals.battery_percentage = 100;

        while (true)
        {
            yield return new WaitForSeconds(7f);
            fakeVitals.primary_oxygen -= 0.2f ;
            fakeVitals.water_capacity -= -0.4f;
            fakeVitals.oxygen_primary_time--;
            fakeVitals.battery_percentage -= 0.3f;
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
