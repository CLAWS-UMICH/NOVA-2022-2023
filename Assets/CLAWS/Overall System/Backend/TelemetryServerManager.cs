using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelemetryServerManager : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(GetTSS());
    }

    IEnumerator GetTSS()
    {
        // simulating getting information from the TSS
        while (true)
        {
            // fake new vitals information from the server
            Vitals TEMP_TSSVitalsDeleteThis = Simulation.User.AstronautVitals;
            TEMP_TSSVitalsDeleteThis.O2++;
            TEMP_TSSVitalsDeleteThis.CO2++;
            TEMP_TSSVitalsDeleteThis.SuitPressure++;
            TEMP_TSSVitalsDeleteThis.WaterPressure++;

            // update our simulation based on those vitals
            // we should try to only update this when it is changed, but for now use a loop
            SetVitals(TEMP_TSSVitalsDeleteThis);

            yield return new WaitForSeconds(1);
        }
    }


    private void SetVitals(Vitals vitals_in)
    {
        Simulation.User.AstronautVitals = vitals_in;

        // publish event that vitals were set
        EventBus.Publish<VitalsUpdatedEvent>(new VitalsUpdatedEvent());
    }
}