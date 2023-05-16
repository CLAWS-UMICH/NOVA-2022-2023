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
    public GameObject geosample;
    //geosample
    //uia
    //messages
    //nav -> create waypoint, go to certain waypoint

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
        Debug.Log(e.output);
        if(String.Equals(words[0],"[command]")){
            Debug.Log("detected");
            if(String.Equals(words[1],"navigation")){
                Debug.Log("nav");
                //opens panel
                //words[2] is command
                if(String.Equals(words[2],"open")){ 
                    //opens navigation
                    navigation.GetComponent<NavScreenController>().OpenNavMainMenu();
                    //navigation.SetActive(true);
                }
                else if(String.Equals(words[2],"close")){
                    //close navigation
                    // Debug.Log("nav close");
                    // navigation.SetActive(false);
                    navigation.GetComponent<NavScreenController>().CloseAll();
                }
                else if(String.Equals(words[2],"danger_waypoint")){
                    navigation.GetComponent<NavScreenController>().OpenWaypoint("danger");
                }
                else if(String.Equals(words[2],"regular_waypoint")){
                    navigation.GetComponent<NavScreenController>().OpenWaypoint("regular");
                }
                else if(String.Equals(words[2],"geosample_waypoint")){
                    navigation.GetComponent<NavScreenController>().OpenWaypoint("geosample");
                }
                else if(String.Equals(words[2],"create_title")){
                    //check that size is correct to prevent erroring out
                    navigation.GetComponent<NavScreenController>().SetWaypointTitle(words[3]); 
                }
                else if(String.Equals(words[2],"confirm_waypoint")){
                    navigation.GetComponent<NavScreenController>().CreateAPoint();
                }
                else if(String.Equals(words[2],"open_waypoint")){
                    navigation.GetComponent<NavScreenController>().SelectWaypointLetter(words[3]);
                }
                else if(String.Equals(words[2],"open_crew")){
                    navigation.GetComponent<NavScreenController>().OpenCrewScreen();
                }
                else if(String.Equals(words[2],"open_geosample")){
                    navigation.GetComponent<NavScreenController>().OpenGeoScreen();
                }
                else if(String.Equals(words[2],"open_mission")){
                    navigation.GetComponent<NavScreenController>().OpenMissionScreen();
                }
                else if(String.Equals(words[2],"open_rover")){
                    navigation.GetComponent<NavScreenController>().OpenVehiclesScreen();
                }
                else if(String.Equals(words[2],"open_lander")){
                    navigation.GetComponent<NavScreenController>().OpenObstaclesScreen();
                }
                else if(String.Equals(words[2],"start_nav")){
                    navigation.GetComponent<NavScreenController>().StartNav();
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
                    //menu.GetComponent<MenuBarController>().DropBar();
                }
                else if(String.Equals(words[2],"close")){
                    //close menu
                }
            }
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
            else if(String.Equals(words[1],"geosample")){
                if(String.Equals(words[2],"open_button_a")){
                    geosample.GetComponent<GeoSampleVegaController>().openButton1();
                }
                else if(String.Equals(words[2],"open_button_b")){
                    geosample.GetComponent<GeoSampleVegaController>().openButton2();
                }
                else if(String.Equals(words[2],"open_button_c")){
                    geosample.GetComponent<GeoSampleVegaController>().openButton3();
                }
                else if(String.Equals(words[2],"scroll_down")){
                    geosample.GetComponent<GeoSampleVegaController>().scrollDown();
                }
                else if(String.Equals(words[2],"scroll_up")){
                    geosample.GetComponent<GeoSampleVegaController>().scrollUp();
                }
                else if(String.Equals(words[2],"expand")){
                    geosample.GetComponent<GeoSampleVegaController>().expand();
                }
                else if(String.Equals(words[2],"minimize")){
                    geosample.GetComponent<GeoSampleVegaController>().minimize();
                }
                else if(String.Equals(words[2],"close")){
                    geosample.GetComponent<GeoSampleVegaController>().close();
                }
                else if(String.Equals(words[2],"open")){
                    geosample.GetComponent<GeoSampleVegaController>().open();
                }
                else if(String.Equals(words[2],"new_sample")){
                    geosample.GetComponent<GeoSampleVegaController>().recordNote();
                }
                else if(String.Equals(words[2],"record_note")){
                    geosample.GetComponent<GeoSampleVegaController>().recordNote();
                }
                else if(String.Equals(words[2],"take_photo")){
                    geosample.GetComponent<GeoSampleVegaController>().take_photo();
                }
                else if(String.Equals(words[2],"open_gallery")){
                    geosample.GetComponent<GeoSampleVegaController>().open_gallery();
                }
                else if(String.Equals(words[2],"page_right")){
                    geosample.GetComponent<GeoSampleVegaController>().page_right();
                }
                else if(String.Equals(words[2],"page_left")){
                    geosample.GetComponent<GeoSampleVegaController>().page_left();
                }
                else if(String.Equals(words[2],"close_gallery")){
                    geosample.GetComponent<GeoSampleVegaController>().close_gallery();
                }
                else if(String.Equals(words[2],"enable_camera")){
                    geosample.GetComponent<GeoSampleVegaController>().enable_camera();
                }
                else if(String.Equals(words[2],"cancel_photo")){
                    geosample.GetComponent<GeoSampleVegaController>().cancel_photo();
                }
            }
        }
    }
}
