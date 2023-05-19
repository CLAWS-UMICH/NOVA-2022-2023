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
    public SimulationStates EVA; // vitals TODO;
    public SimulationFailures EVA_failures;
    public IMUMsg IMU;
    public GPSMsg GPS;
    public UIAMsg UIA;
    public RoverMsg ROVER;
    public SpecMsg GEO;



}
