using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VEGACommandHandler : MonoBehaviour
{
    // KRITI
    public GameObject menu;
    public GameObject vitals;
    public GameObject navigation;
    public GameObject task_list;
    //public GameObject luna;
    //add a close all
    void Start()
    {
        EventBus.Subscribe<VEGA_OutputEvent>(ProcessVEGACommand);
        Debug.Log("start");
    }

    void ProcessVEGACommand(VEGA_OutputEvent e)
    {
        Debug.Log("command");
        string[] words = e.output.Split(' ');
        if(String.Equals(words[0],"[command]")){
            Debug.Log("detected");
            if(String.Equals(words[1],"navigation")){
                Debug.Log("nav");
                //opens panel
                //words[2] is command
                if(String.Equals(words[2],"open")){ 
                    //opens navigation
                    navigation.SetActive(true);
                }
                else if(String.Equals(words[2],"close")){
                    //close navigation
                    Debug.Log("nav close");
                    navigation.SetActive(false);
                }
            }
            else if(String.Equals(words[1],"task_list")){
                if(String.Equals(words[2],"open")){
                    //open task_list
                    task_list.SetActive(true);
                }
                else if(String.Equals(words[2],"close")){
                    //close task_list
                    task_list.SetActive(false);
                }
            }
            
            else if(String.Equals(words[1],"vitals")){
                if(String.Equals(words[2],"open")){
                    //open vitals
                    vitals.SetActive(true);
                }
                else if(String.Equals(words[2],"close")){
                    //close vitals
                    vitals.SetActive(false);
                }
            }
            else if(String.Equals(words[1],"menu")){
                if(String.Equals(words[2],"open")){
                    //open menu
                    menu.GetComponent<MenuBarController>().DropBar();
                }
                else if(String.Equals(words[2],"close")){
                    //close menu
                }
            }
            // else if(String.Equals(words[1],"luna")){
            //     if(String.Equals(words[2],"open")){
            //         luna.SetActive(true);
            //     }
            // }
            // else if(String.Equals(words[1],"luna")){
            //     if(String.Equals(words[2],"close")){
            //         luna.SetActive(false);
            //     }
            // }
            else if(String.Equals(words[1],"messaging")){
                if(String.Equals(words[2],"open")){
                }
                else if(String.Equals(words[2],"close")){
                }
            }
           
            else if(String.Equals(words[1],"uia_egress")){
                if(String.Equals(words[2],"")){
                }
            }
            else if(String.Equals(words[1],"rover")){
                if(String.Equals(words[2],"")){
                }
            }
            else if(String.Equals(words[1],"geosample")){
                if(String.Equals(words[2],"")){
                }
            }
            
            
            
        }
    }
}
