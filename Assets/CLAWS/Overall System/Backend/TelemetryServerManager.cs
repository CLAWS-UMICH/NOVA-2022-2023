using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelemetryServerManager : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(GetTSS());
        Vitals TEMP_TSSVitalsDeleteThis = Simulation.User.AstronautVitals;
        TEMP_TSSVitalsDeleteThis.p_o2 = 100;
        TEMP_TSSVitalsDeleteThis.p_sub = 100;
        TEMP_TSSVitalsDeleteThis.p_suit = 100;
        TEMP_TSSVitalsDeleteThis.cap_water = 100;
        TEMP_TSSVitalsDeleteThis.p_sub = 100;
        TEMP_TSSVitalsDeleteThis.p_suit = 100;
        TEMP_TSSVitalsDeleteThis.t_sub = 100;
        TEMP_TSSVitalsDeleteThis.v_fan = 100;
        TEMP_TSSVitalsDeleteThis.p_o2 = 100;
        TEMP_TSSVitalsDeleteThis.rate_o2 = 100;
        TEMP_TSSVitalsDeleteThis.batteryPercent = 100;
        TEMP_TSSVitalsDeleteThis.cap_battery = 100;
        TEMP_TSSVitalsDeleteThis.battery_out = 100;
        TEMP_TSSVitalsDeleteThis.p_h2o_g = 100;
        TEMP_TSSVitalsDeleteThis.p_h2o_l = 100;
        TEMP_TSSVitalsDeleteThis.p_sop = 100;
        TEMP_TSSVitalsDeleteThis.rate_sop = 100;
        TEMP_TSSVitalsDeleteThis.t_oxygenPrimary = 100;
        TEMP_TSSVitalsDeleteThis.t_oxygenSec = 100;
        TEMP_TSSVitalsDeleteThis.ox_primary = 100;
        TEMP_TSSVitalsDeleteThis.ox_secondary = 100;
        TEMP_TSSVitalsDeleteThis.cap_water = 100;

        SetVitals(TEMP_TSSVitalsDeleteThis);
    }

    IEnumerator GetTSS()
    {
        // simulating getting information from the TSS
        while (true)
        {
            // fake new vitals information from the server
            Vitals TEMP_TSSVitalsDeleteThis = Simulation.User.AstronautVitals;
            TEMP_TSSVitalsDeleteThis.p_o2--;
            TEMP_TSSVitalsDeleteThis.p_sub--;
            TEMP_TSSVitalsDeleteThis.p_suit--;
            TEMP_TSSVitalsDeleteThis.cap_water--;
            TEMP_TSSVitalsDeleteThis.p_sub--;
            TEMP_TSSVitalsDeleteThis.p_suit--;
            TEMP_TSSVitalsDeleteThis.t_sub--;
            TEMP_TSSVitalsDeleteThis.v_fan--;
            TEMP_TSSVitalsDeleteThis.p_o2--;
            TEMP_TSSVitalsDeleteThis.rate_o2--;
            TEMP_TSSVitalsDeleteThis.batteryPercent--;
            TEMP_TSSVitalsDeleteThis.cap_battery--;
            TEMP_TSSVitalsDeleteThis.battery_out--;
            TEMP_TSSVitalsDeleteThis.p_h2o_g--;
            TEMP_TSSVitalsDeleteThis.p_h2o_l--;
            TEMP_TSSVitalsDeleteThis.p_sop--;
            TEMP_TSSVitalsDeleteThis.rate_sop--;
            TEMP_TSSVitalsDeleteThis.t_oxygenPrimary--;
            TEMP_TSSVitalsDeleteThis.t_oxygenSec--;
            TEMP_TSSVitalsDeleteThis.ox_primary--;
            TEMP_TSSVitalsDeleteThis.ox_secondary--;
            TEMP_TSSVitalsDeleteThis.cap_water--;

            // update our simulation based on those vitals
            // we should try to only update this when it is changed, but for now use a loop
            SetVitals(TEMP_TSSVitalsDeleteThis);

            yield return new WaitForSeconds(3);
        }
    }

    private void SetVitals(Vitals vitals_in)
    {
        Simulation.User.AstronautVitals = vitals_in;

        // publish event that vitals were set
        EventBus.Publish<VitalsUpdatedEvent>(new VitalsUpdatedEvent());
    }
}