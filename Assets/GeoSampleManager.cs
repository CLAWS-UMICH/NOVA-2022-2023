using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

public class GeoSampleManager : MonoBehaviour
{
    int id = 1;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<GeoSpecRecievedEvent>(CreateGeoSample);
    }

    private void CreateGeoSample(GeoSpecRecievedEvent e) {
        SpecMsg s = Simulation.User.GEO;
        string rockType = "rock";
        string coordinate = "42.1234 N, 24.1234 E";
        Simulation.User.AstronautGeoSamples.geoSampleList.Insert(0, new GeoSample(id, rockType, System.DateTime.Now.ToString(), coordinate, "23940329", 'n', "", s));
        EventBus.Publish<GeoSampleUpdatedEvent>(new GeoSampleUpdatedEvent(0));
        PopUpManager.MakePopup("New Geo Sample Added.");
        id++;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
