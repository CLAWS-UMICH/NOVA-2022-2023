using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningVitals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void VitalLevel(){
        Vitals TempVital = Simulation.User.AstronautVitals;
        //Debug.Log("hi1");
        if(TempVital.p_o2 >= 11){
            // var colorTheme = this.GetComponent<Interactable>().ActiveThemes[0];
            // colorTheme.StateProperties[0].Values[0].Color = Color.green;
            //Debug.Log("hi");
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        // if(TempVital.CO2 >= 12){
            
        // }
        // if(TempVital.SuitPressure >= 12){
            
        // }
        // if(TempVital.WaterPressure >= 12){
            
        // }
        

    }

    // Update is called once per frame
    void Update()
    {
        VitalLevel();
    }
}
