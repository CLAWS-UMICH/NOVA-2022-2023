using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuVital : MonoBehaviour
{
    [SerializeField] TextMeshPro vitalText1;
    [SerializeField] TextMeshPro vitalText2;
    [SerializeField] TextMeshPro vitalText3;


    void Start() 
    {
        EventBus.Subscribe<VitalsUpdatedEvent>(UpdateVitals);
    }

    void UpdateVitals(VitalsUpdatedEvent e) {
        Debug.Log(e.ToString());

        vitalText1.SetText((Simulation.User.AstronautVitals.p_o2).ToString());
        vitalText2.SetText((Simulation.User.AstronautVitals.batteryPercent).ToString());
        vitalText3.SetText((Simulation.User.AstronautVitals.cap_water).ToString());
    }
}