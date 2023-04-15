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
    string ResponseString;


    void Start()
    {
        EventBus.Subscribe<VEGA_OutputEvent>(ProcessVEGATextResponse);
        _astronaut = Simulation.User;
    }

    void ProcessVEGATextResponse(VEGA_OutputEvent e)
    {
        VEGAVariable = "";
        string[] VEGAStrings = e.output.Split("'");
        ResponseString = VEGAStrings[1];

        string[] words = e.output.Split(' ');
        if (words[0] == "[text]") {
            if (words[1] == "vitals")
            {
                if (words[2] == "id")
                {
                    VEGAVariable = _astronaut.AstronautVitals.id.ToString();
                }
                else if (words[2] == "room")
                {
                    VEGAVariable = _astronaut.AstronautVitals.room.ToString();
                }
                else if (words[2] == "time")
                {
                    VEGAVariable = _astronaut.AstronautVitals.time.ToString();
                }
                else if (words[2] == "heart_bpm")
                {
                    VEGAVariable = _astronaut.AstronautVitals.heart_bpm.ToString();
                }
                else if (words[2] == "p_o2")
                {
                    VEGAVariable = _astronaut.AstronautVitals.p_o2.ToString();
                }
                else if (words[2] == "batteryPercent")
                {
                    VEGAVariable = _astronaut.AstronautVitals.batteryPercent.ToString();
                }
                else if (words[2] == "ox_primary")
                {
                    VEGAVariable = _astronaut.AstronautVitals.ox_primary.ToString();
                }
                else if (words[2] == "ox_secondary")
                {
                    VEGAVariable = _astronaut.AstronautVitals.ox_secondary.ToString();
                }
            }
            else if (words[1] == "menu")
            {

            }
            else if (words[1] == "task_list")
            {
                if (words[2] == "current_task")
                {
                    VEGAVariable = _astronaut.AstronautTasks.messageQueue.ToString();
                }
                else if (words[2] == "next_task")
                {
                    VEGAVariable = _astronaut.AstronautTasks.messageQueue2.ToString();
                }
            }
            else if (words[1] == "luna")
            {

            }
            else if (words[1] == "messaging")
            {
                if (words[2] == "current_message")
                {
                    VEGAVariable = _astronaut.AstronautMessaging.messageQueue.ToString();
                }
            }
            else if (words[1] == "navigation")
            {

            }

            //phase 3
            else if (words[1] == "uia_egress")
            {

            }
            else if (words[1] == "rover")
            {

            }
            else if (words[1] == "geosample")
            {

            }
        }

        ResponseString.Replace("*", VEGAVariable);

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
