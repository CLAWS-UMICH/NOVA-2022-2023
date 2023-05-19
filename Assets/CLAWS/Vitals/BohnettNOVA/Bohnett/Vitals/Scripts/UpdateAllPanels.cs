using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TSS.Msgs;
using System;

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
    private SimulationStates vitals;

    public TextMeshProUGUI Timer;
    // Start is called before the first frame update
    void Awake()
    {
        EventBus.Subscribe<VitalsUpdatedEvent>(UpdatePanels);
        vitalPanels = FindObjectsOfType<VitalPanel>(true);
    }

    public void UpdatePanels(VitalsUpdatedEvent e)
    {
        vitals = Simulation.User.EVA;
        
        for (int i = 0; i < vitalPanels.Length; i++)
        {
            PanelInformation panel = vitalPanels[i].GetPanelInformation();
            UpdateVitalPanel(panel, vitalPanels[i]);

        }

        Timer.text = vitals.timer;
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

            case PanelInformation.Main:
                return GetMainLabelAtIndex(i);

            default:
                return "Not a valid panel";
        }
    }

    private string GetOxygenPrimaryLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return ((int)Math.Floor(vitals.oxygen_primary_time)).ToString();

            case 1:
                return vitals.o2_time_left.ToString();
      

            case 2:
                return "Rate: " + vitals.o2_rate.ToString();
       

            case 3:
                return "Pressure: " + vitals.o2_pressure.ToString();
          

            default:
                return "N/A";
            
        }

    }

    private string GetOxygenBackupLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return ((int)Math.Floor(vitals.oxygen_secondary_time)).ToString();

            case 1:
                return vitals.o2_time_left.ToString();
           

            default:
                return "N/A";
           
        }

    }

    private string GetSuitPressureLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return vitals.suits_pressure + " psi";
            
            default:
                return "N/A";
          
        }
    }

    private string GetWaterPressureLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return vitals.h2o_liquid_pressure.ToString() + " psi";

            case 1:
                return vitals.h2o_gas_pressure.ToString() + " psi";

            default:
                return "N/A";

        }
    }

    private string GetWaterLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                // Debug.Log(((int)Mathf.Floor(vitals.cap_water)).ToString());
                return ((int)Math.Floor(vitals.water_capacity)).ToString(); 

            case 1:
                return vitals.h2o_time_left.ToString();

            default:
                return "N/A";

        }
    }

    private string GetFanLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return vitals.fan_tachometer.ToString() + " rpm";

            default:
                return "N/A";

        }
    }

    private string GetBatteryBackupLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return ((int)Math.Floor(vitals.battery_percentage)).ToString();

            case 1:
                return vitals.battery_time_left.ToString();


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
                return vitals.heart_rate + " bpm";

            case 1:
                return vitals.sub_pressure + " psi";

            case 2:
                return vitals.temperature + " C";


            default:
                return "N/A";

        }

    }

    private string GetMainLabelAtIndex(int i)
    {
        switch (i)
        {
            case 0:
                return ((int)Math.Floor(vitals.primary_oxygen)).ToString();

            case 1:
                return ((int)Math.Floor(vitals.secondary_oxygen)).ToString();

            case 2:
                return vitals.o2_time_left.ToString();

            case 3:
                return vitals.battery_time_left.ToString();

            case 4:
                return vitals.suits_pressure + " psi";


            default:
                return "N/A";

        }
    }

    public VitalPanel[] ReturnPanels()
    {
        return vitalPanels;
    }
}
