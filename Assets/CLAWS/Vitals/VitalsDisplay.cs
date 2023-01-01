using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class VitalsDisplay : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
	EventBus.Subscribe<VitalsUpdated>(OnVitalUpdatedEvent);
    }
    void OnVitalUpdatedEvent(VitalsUpdated e) {
	text.SetText(
	    "O2: " + Simulation.User.vitals.O2 +
	    "\nCO2: " + Simulation.User.vitals.CO2 +
	    "\nWaterPressure: " + Simulation.User.vitals.WaterPressure +
	    "\nSuitPressure:" + Simulation.User.vitals.SuitPressure
	);
    }
}
