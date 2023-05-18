using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

[System.Serializable]
public class Astronaut : MonoBehaviour
{
    public Vitals AstronautVitals;
    public TaskList AstronautTasks;
    public Messaging AstronautMessaging;
    public QueueClass UdpQueue;
    public GeoSampleList AstronautGeoSamples; 

    // TSS objects
    public EVAMsg EVA; // vitals
    public IMUMsg IMU;
    public GPSMsg GPS;
    public UIAMsg UIA;
    public UIAControlMsg UIA_CONTROLS;
    public specMsg GEO;


}
