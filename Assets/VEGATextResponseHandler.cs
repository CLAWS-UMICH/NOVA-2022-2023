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
    private IEnumerator coroutine;


    void Start()
    {
        EventBus.Subscribe<VEGA_OutputEvent>(ProcessVEGATextResponse);
        _astronaut = Simulation.User;
    }

    void ProcessVEGATextResponse(VEGA_OutputEvent e)
    {
        VEGAVariable = "";
        

        string[] words = e.output.Split(' ');
        if (words[0] == "[text]") {
            string[] VEGAStrings = e.output.Split("'");
            ResponseString = VEGAStrings[1];
            if (words[1] == "vitals")
            {
                if (words[2] == "id")
                {
                    // TODO remove
                    // VEGAVariable = _astronaut.EVA.id.ToString();
                }
                else if (words[2] == "room")
                {
                    VEGAVariable = _astronaut.EVA.room_id.ToString();
                }
                else if (words[2] == "time")
                {
                    VEGAVariable = _astronaut.EVA.time.ToString();
                }
                else if (words[2] == "heart_bpm")
                {
                    VEGAVariable = _astronaut.EVA.heart_rate.ToString();
                }
                else if (words[2] == "oxygen") //combine oxygens and add something for T_oxygen
                {
                    VEGAVariable = _astronaut.EVA.o2_pressure.ToString();
                //     VEGAVariable = _astronaut.EVA.ox_primary.ToString();
                //     VEGAVariable = _astronaut.EVA.ox_secondary.ToString();
                }
                else if (words[2] == "batteryPercent")
                {
                    VEGAVariable = _astronaut.EVA.battery_percentage.ToString();
                }
                // else if (words[2] == "ox_primary")
                // {
                //     VEGAVariable = _astronaut.EVA.ox_primary.ToString();
                // }
                // else if (words[2] == "ox_secondary")
                // {
                //     VEGAVariable = _astronaut.EVA.ox_secondary.ToString();
                // }
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
        
        string newString = ResponseString.Replace("*", VEGAVariable);

            if (VEGAVariable != "") {
                if(words[2] == "oxygen"){
                    VEGAVariable = "Your primary oxygen is at " + _astronaut.EVA.primary_oxygen.ToString() + " percent" +
                        "\n Your secondary oxygen is at " +  _astronaut.EVA.secondary_oxygen.ToString() + " percent" +
                        "\n Your p_o2 is at " + _astronaut.EVA.o2_pressure.ToString();
                }
                else{
                    VEGAVariable = newString;
                }
                VEGAResponseTextBox();
            }
        }
}

    void VEGAResponseTextBox()
    {
        text.text = VEGAVariable;

        bool active = false;
        if (VEGAVariable != "") 
        {
            // textBox.SetActive(true);
            // active = true;
            // coroutine = PopUp(active);
            // StartCoroutine(coroutine);
            PopUpManager.MakePopupVega(VEGAVariable,3);
        }
    }

    IEnumerator PopUp(bool active){
        while(active){
            yield return new WaitForSeconds(3f);
            textBox.SetActive(false);
            active = false;
           
        }
    }
}
