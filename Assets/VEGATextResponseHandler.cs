using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VEGATextResponseHandler : MonoBehaviour
{
    // SELINA
    [SerializeField]
    GameObject textBox;
    [SerializeField]
    TextMeshPro text;

    Astronaut _astronaut;
    string VEGAVariable;


    void Start()
    {
        EventBus.Subscribe<VEGA_OutputEvent>(ProcessVEGATextResponse);
    }

    void ProcessVEGATextResponse(VEGA_OutputEvent e)
    {
        VEGAVariable = "";

        string[] words = e.output.Split(' ');
        if (words[0] == "[text]") {
            if (words[1] == "vitals")
            {
                if (words[2] == "id")
                {
                    VEGAVariable = _astronaut.AstronautVitals.id.ToString();
                }
                if (words[2] == "room")
                {
                    VEGAVariable = _astronaut.AstronautVitals.room.ToString();
                }
                if (words[2] == "isRunning")
                {
                    VEGAVariable = _astronaut.AstronautVitals.isRunning.ToString();
                }
                if (words[2] == "isPaused")
                {
                    VEGAVariable = _astronaut.AstronautVitals.isPaused.ToString();
                }
                if (words[2] == "time")
                {
                    VEGAVariable = _astronaut.AstronautVitals.time.ToString();
                }
                if (words[2] == "timer")
                {
                    VEGAVariable = _astronaut.AstronautVitals.timer.ToString();
                }
                if (words[2] == "started_at")
                {
                    VEGAVariable = _astronaut.AstronautVitals.started_at.ToString();
                }
                if (words[2] == "heart_bpm")
                {
                    VEGAVariable = _astronaut.AstronautVitals.heart_bpm.ToString();
                }
                if (words[2] == "p_sub")
                {
                    VEGAVariable = _astronaut.AstronautVitals.p_sub.ToString();
                }
                if (words[2] == "p_suit")
                {
                    VEGAVariable = _astronaut.AstronautVitals.p_suit.ToString();
                }
                if (words[2] == "t_sub")
                {
                    VEGAVariable = _astronaut.AstronautVitals.t_sub.ToString();
                }
                if (words[2] == "v_fan")
                {
                    VEGAVariable = _astronaut.AstronautVitals.v_fan.ToString();
                }
                if (words[2] == "p_o2")
                {
                    VEGAVariable = _astronaut.AstronautVitals.p_o2.ToString();
                }
                if (words[2] == "rate_o2")
                {
                    VEGAVariable = _astronaut.AstronautVitals.rate_o2.ToString();
                }
                if (words[2] == "batteryPercent")
                {
                    VEGAVariable = _astronaut.AstronautVitals.batteryPercent.ToString();
                }
                if (words[2] == "cap_battery")
                {
                    VEGAVariable = _astronaut.AstronautVitals.cap_battery.ToString();
                }
                if (words[2] == "battery_out")
                {
                    VEGAVariable = _astronaut.AstronautVitals.battery_out.ToString();
                }
                if (words[2] == "p_h2o_g")
                {
                    VEGAVariable = _astronaut.AstronautVitals.p_h2o_g.ToString();
                }
                if (words[2] == "p_h2o_l")
                {
                    VEGAVariable = _astronaut.AstronautVitals.p_h2o_l.ToString();
                }
                if (words[2] == "p_sop")
                {
                    VEGAVariable = _astronaut.AstronautVitals.p_sop.ToString();
                }
                if (words[2] == "rate_sop")
                {
                    VEGAVariable = _astronaut.AstronautVitals.rate_sop.ToString();
                }
                if (words[2] == "t_battery")
                {
                    VEGAVariable = _astronaut.AstronautVitals.t_battery.ToString();
                }
                if (words[2] == "t_oxygenPrimary")
                {
                    VEGAVariable = _astronaut.AstronautVitals.t_oxygenPrimary.ToString();
                }
                if (words[2] == "ox_primary")
                {
                    VEGAVariable = _astronaut.AstronautVitals.ox_primary.ToString();
                }
                if (words[2] == "ox_secondary")
                {
                    VEGAVariable = _astronaut.AstronautVitals.ox_secondary.ToString();
                }
                if (words[2] == "t_oxygen")
                {
                    VEGAVariable = _astronaut.AstronautVitals.t_oxygen.ToString();
                }
                if (words[2] == "cap_water")
                {
                    VEGAVariable = _astronaut.AstronautVitals.cap_water.ToString();
                }
                if (words[2] == "t_water")
                {
                    VEGAVariable = _astronaut.AstronautVitals.t_water.ToString();
                }
            }
            if (words[1] == "menu")
            {

            }
            if (words[1] == "task_list")
            {
                if (words[2] == "current_task")
                {
                    VEGAVariable = _astronaut.AstronautTasks.messageQueue.ToString();
                }
                if (words[2] == "next_task")
                {
                    VEGAVariable = _astronaut.AstronautTasks.messageQueue2.ToString();
                }
            }
            if (words[1] == "luna")
            {

            }
            if (words[1] == "messaging")
            {
                if (words[2] == "current_message")
                {
                    VEGAVariable = _astronaut.AstronautMessaging.messageQueue.ToString();
                }
            }
            if (words[1] == "navigation")
            {

            }

            //phase 3
            if (words[1] == "uia_egress")
            {

            }
            if (words[1] == "rover")
            {

            }
            if (words[1] == "geosample")
            {

            }
        }
        if (VEGAVariable != "") {
            VEGAResponseTextBox();
        }
    }

    void VEGAResponseTextBox()
    {
        text.text = VEGAVariable;
        if (VEGAVariable == "")
        {
            textBox.SetActive(false);
        }
        else
        {
            textBox.SetActive(true);
        }
    }
}
