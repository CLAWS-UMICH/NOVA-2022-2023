using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuVital : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI vitalText1;
    [SerializeField] TextMeshProUGUI vitalText2;
    [SerializeField] TextMeshProUGUI vitalText3;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        vitalText1.SetText((Simulation.User.AstronautVitals.O2).ToString());
        vitalText2.SetText("Battery: 84%");
        vitalText3.SetText((Simulation.User.AstronautVitals.WaterPressure).ToString());
    }
}