using System.Collections;
using System.Collections.Generic;
//using System.Windows.Media;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class WarningVitals : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject panel;
    [SerializeField] TextMeshPro warningText;
    [SerializeField] GameObject largePanel;
    [SerializeField] Color red;
    void Start()
    {
        EventBus.Subscribe<VitalsUpdatedEvent>(VitalLevel);
    }

    void VitalLevel(VitalsUpdatedEvent e){
        Vitals TempVital = Simulation.User.AstronautVitals;
        //Debug.Log("hi1");
        if(TempVital.p_o2 >= 3){
            // var colorTheme = this.GetComponent<Interactable>().ActiveThemes[0];
            // colorTheme.StateProperties[0].Values[0].Color = Color.green;
            //Debug.Log("hi");
            // gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            gameObject.GetComponent<MeshRenderer>().material.color = red;
            //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            //new Color(0.4f, 0.9f, 0.7f, 1.0f);
            if(!largePanel.activeSelf){
                panel.GetComponent<MeshRenderer>().material.color = red;
                warningText.SetText("O2 levels low");
                panel.SetActive(true);
                
                
                // panel.transform.GetChild().GetComponent<TextMeshProUGUI>.text = "O2 levels";
                

            }
            
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
        if(gameObject.GetComponent<MeshRenderer>().enabled == false){
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
