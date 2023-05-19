using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

public class AddDummySample : MonoBehaviour
{
    int id = 1;
    string[] rocks = {"Slate", "Olivine Basalt", "Vesicular Basalt", "Mare Basalt"};
    SpecMsg spec;

    [ContextMenu("AddRandomSample")]
    public void AddRandomSample() {
        Simulation.User.AstronautGeoSamples.geoSampleList.Insert(0, new GeoSample(id, rocks[id % 4], System.DateTime.Now.ToString(), "26.234 N, 42.2345 W", (1203948329 * 1.02 * id).ToString(), 'n', "This is a sample description for task " + id.ToString(), spec));
        EventBus.Publish<GeoSampleUpdatedEvent>(new GeoSampleUpdatedEvent(0));
        id++;
    }
}
