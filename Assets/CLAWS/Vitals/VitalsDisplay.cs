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
		EventBus.Subscribe<VitalsUpdatedEvent>(UpdateVitalsDisplay);
    }

    void UpdateVitalsDisplay(VitalsUpdatedEvent e) {

		Debug.Log(e.ToString());

		text.SetText(
			"O2: " + Simulation.User.AstronautVitals.p_o2 +
			"\nCO2: " + Simulation.User.AstronautVitals.p_sub +
			"\nWaterPressure: " + Simulation.User.AstronautVitals.cap_water +
			"\nSuitPressure:" + Simulation.User.AstronautVitals.p_suit
		);
    }
}
