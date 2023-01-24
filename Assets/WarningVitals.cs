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
    [SerializeField] Color normal;

    private VitalPanel[] vitalPanels;

    private 
    void Start()
    {
        EventBus.Subscribe<VitalsUpdatedEvent>(VitalLevel);
        vitalPanels = gameObject.GetComponent<UpdateAllPanels>().ReturnPanels();
    }

    private void Update()
    {
        if (largePanel.activeSelf)
        {
            panel.SetActive(false);
        }
    }

    void VitalLevel(VitalsUpdatedEvent e){
        Vitals TempVital = Simulation.User.AstronautVitals;
        //Debug.Log("hi1");
        VitalPanel vitpanel;

        vitpanel = FindPanelWithInformationType(PanelInformation.OxygenPrimary);
        if (TempVital.p_o2 >= 12){
            // var colorTheme = this.GetComponent<Interactable>().ActiveThemes[0];
            // colorTheme.StateProperties[0].Values[0].Color = Color.green;
            //Debug.Log("hi");
            // gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            //gameObject.GetComponent<MeshRenderer>().material.color = red;
            if (vitpanel != null)
            {
                vitpanel.SetPanelError(red);
            }


            //Color color = (Color)ColorConverter.ConvertFromString("#FFDFD991");
            //new Color(0.4f, 0.9f, 0.7f, 1.0f);
            if(!largePanel.activeSelf){
                panel.GetComponent<MeshRenderer>().material.color = red;
                warningText.SetText("O2 levels low");
                panel.SetActive(true);
                
                
                // panel.transform.GetChild().GetComponent<TextMeshProUGUI>.text = "O2 levels";
                

            } 
            
        } else
        {
            //Remove it from the errors on the screen if big panel is not shown
            panel.SetActive(false);
            if (vitpanel != null)
            {
                vitpanel.SetPanelError(normal);
            }
        }

         if (TempVital.p_sub >= 12) {
            
         }
         if (TempVital.p_suit >= 12) {
            
         }
         if (TempVital.cap_water >= 12) {
            
         }
        

    }

    // Update is called once per frame
    /*
    void Update()
    {
        if(gameObject.GetComponent<MeshRenderer>().enabled == false){
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
    */

    private VitalPanel FindPanelWithInformationType(PanelInformation panel)
    {
        for (int i = 0; i < vitalPanels.Length; i++)
        {
            if (vitalPanels[i].GetPanelInformation() == panel)
            {
                return vitalPanels[i];
            }
        }

        return null;
    }
}
