using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// None of the updatePanel function have any sort of rounding, truncation, or anything similar of long decimal points
// with the exception of Mathf.Floor()

// This only works if the updatedPanels lists for each panel is order from what needs to be updated
// top to bottom!!!!

// Example: Primary Oxygen tab has an updatedFields list of gameobjects that are ordered as:
//          Progress meter integer
//          Time
//          Rate
//          Pressure
public class UpdateAllPanels : MonoBehaviour
{
    private VitalPanel[] vitalPanels;
    private Vitals vitals;
    // Start is called before the first frame update
    void Awake()
    {
        EventBus.Subscribe<VitalsUpdatedEvent>(UpdatePanels);
        vitalPanels = FindObjectsOfType<VitalPanel>(true);
    }

    public void UpdatePanels(VitalsUpdatedEvent e)
    {
        vitals = Simulation.User.AstronautVitals;
        
        for (int i = 0; i < vitalPanels.Length; i++)
        {
            PanelInformation panel = vitalPanels[i].GetPanelInformation();
            UpdateVitalPanel(panel, vitalPanels[i]);

        }
    }

    private void UpdateVitalPanel(PanelInformation panel, VitalPanel vital)
    {
        if (vital.GetPanelInformation() != panel)
        {
            return;
        } else
        {
            GameObject[] updateFields = vital.GetUpdateFields();

            for (int i = 0; i < updateFields.Length; i++)
            {

                updateFields[i].GetComponent<TMP_Text>().text = GetCorrectLabelByPanelInformation(panel, i);
            }
        }
    }

    // Chooses which of the functions below this one to read from to get proper values
    private string GetCorrectLabelByPanelInformation(PanelInformation panel, int i)
    {
        switch(panel)
        {
            case PanelInformation.OxygenPrimary:
                return GetOxygenPrimaryLabelAtIndex(i);

            case PanelInformation.OxygenBackup:
                return GetOxygenBackupLabelAtIndex(i);

            case PanelInformation.SuitPressure:
                return GetSuitPressureLabelAtIndex(i);

            case PanelInformation.WaterPressure:
                return GetWaterPressureLabelAtIndex(i);

            case PanelInformation.Water:
                return GetWaterLabelAtIndex(i);

            case PanelInformation.Fan:
                return GetFanLabelAtIndex(i);

            case PanelInformation.BatteryBackup:
                return GetBatteryBackupLabelAtIndex(i);

            case PanelInformation.Body:
                return GetBodyLabelAtIndex(i);

            default:
                return "Not a valid panel";
        }
    }

    private string GetOxygenPrimaryLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return ((int)Mathf.Floor(vitals.ox_primary)).ToString();

            case 1:
                return vitals.t_oxygen;
      

            case 2:
                return "Rate: " + vitals.rate_o2;
       

            case 3:
                return "Pressure: " + vitals.p_o2;
          

            default:
                return "N/A";
            
        }

    }

    private string GetOxygenBackupLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return ((int)Mathf.Floor(vitals.ox_secondary)).ToString();

            case 1:
                return vitals.t_oxygen;
           

            default:
                return "N/A";
           
        }

    }

    private string GetSuitPressureLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return vitals.p_suit + " psi";
            
            default:
                return "N/A";
          
        }
    }

    private string GetWaterPressureLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return vitals.p_h2o_l + " psi";

            case 1:
                return vitals.p_h2o_g + " psi";

            default:
                return "N/A";

        }
    }

    private string GetWaterLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                Debug.Log(((int)Mathf.Floor(vitals.cap_water)).ToString());
                return ((int)Mathf.Floor(vitals.cap_water)).ToString(); 

            case 1:
                return vitals.t_water;

            default:
                return "N/A";

        }
    }

    private string GetFanLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return vitals.v_fan + " rpm";

            default:
                return "N/A";

        }
    }

    private string GetBatteryBackupLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return ((int)Mathf.Floor(vitals.cap_battery)).ToString();

            case 1:
                return vitals.t_battery;


            default:
                return "N/A";

        }

    }

    private string GetBodyLabelAtIndex(int i)
    {
        // Change to degrees C
        switch (i)
        {
            case 0:
                return vitals.heart_bpm + " bpm";

            case 1:
                return vitals.p_sub + " psi";

            case 2:
                return vitals.t_sub + " C";


            default:
                return "N/A";

        }

    }

    public VitalPanel[] ReturnPanels()
    {
        return vitalPanels;
    }
}
