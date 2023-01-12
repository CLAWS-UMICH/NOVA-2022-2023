using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuVital : MonoBehaviour
{
    [SerializeField] TextMeshPro vitalText1;
    [SerializeField] TextMeshPro vitalText2;
    [SerializeField] TextMeshPro vitalText3;

    // Update is called once per frame
    void Update()
    {
        // vitalText1.SetText((Simulation.User.AstronautVitals.O2).ToString());
        vitalText2.SetText("84%");
        vitalText3.SetText((Simulation.User.AstronautVitals.SuitPressure).ToString());
    }
}