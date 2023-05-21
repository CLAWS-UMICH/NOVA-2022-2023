using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS;
using TSS.Msgs;


public class TelemetryServerManager : MonoBehaviour
{
    public TSSConnection tss;
    string tssUri;

    int msgCount = 0;

    async void Start()
    {
        tss = new TSSConnection();

        // TODO tie this to an input field and button
        Connect();
    }

    void Update()
    {
        tss.Update();
    }

    public async void Connect()
    {
        string team_name = "CLAWS";
        string username = "Patrick";
        string university = "University of Michigan";
        string user_guid = "eb0dde22-a403-45cd-a3bc-45c797634d32";
        tssUri = "ws://192.168.50.10:3001"; // "ws://localhost:3001"; 
        var connecting = tss.ConnectToURI(tssUri, team_name, username, university, user_guid);
        Debug.Log("Connecting to " + tssUri);

        tss.OnOpen += () =>
        {
            Debug.Log("Connection Successful");
            PopUpManager.MakePopup("Connected to TSS");
        };
        
        tss.OnTSSTelemetryMsg += (telemMsg) =>
        {
            msgCount++;
            Debug.Log("telem message updated");
            Debug.Log(telemMsg.simulationStates.time);

            if (telemMsg.gpsMsg != null)
            {
                Simulation.User.GPS = telemMsg.gpsMsg;
                EventBus.Publish<UpdatedGPSEvent>(new UpdatedGPSEvent());
            }
            else
            {
                Debug.LogError("No GPS Msg received");
            }

            if (telemMsg.imuMsg != null)
            {
                Simulation.User.IMU = telemMsg.imuMsg;
            }
            else
            {
                Debug.LogError("No IMU Msg received");
            }

            if (telemMsg.simulationStates != null)
            {
                Simulation.User.EVA = telemMsg.simulationStates;
                EventBus.Publish<VitalsUpdatedEvent>(new VitalsUpdatedEvent());
            }
            else
            {
                Debug.LogError("No EVA Msg received");
            }

            if (telemMsg.uiaMsg != null)
            {
                Simulation.User.UIA = telemMsg.uiaMsg;
                EventBus.Publish<UIAMsgEvent>(new UIAMsgEvent());
            }
            else
            {
                Debug.LogError("No UIA Msg received");
            }

            if (telemMsg.uiaState != null)
            {
                Simulation.User.UIA_State = telemMsg.uiaState;
                EventBus.Publish<UIAMsgEvent>(new UIAMsgEvent());
            }
            else
            {
                Debug.LogError("UIA State Updated");
            }

            if (telemMsg.roverMsg != null)
            {
                Simulation.User.ROVER = telemMsg.roverMsg;
            }
            else
            {
                Debug.LogError("No rover Msg received");
            }
            if (telemMsg.specMsg != null)
            {
                if(Simulation.User.GEO.SiO2 != telemMsg.specMsg.SiO2 &&
                Simulation.User.GEO.TiO2 != telemMsg.specMsg.TiO2 &&
                Simulation.User.GEO.Al2O3 != telemMsg.specMsg.Al2O3 &&
                Simulation.User.GEO.FeO != telemMsg.specMsg.FeO &&
                Simulation.User.GEO.MnO != telemMsg.specMsg.MgO &&
                Simulation.User.GEO.CaO != telemMsg.specMsg.CaO &&
                Simulation.User.GEO.K2O != telemMsg.specMsg.K2O &&
                Simulation.User.GEO.P2O3 != telemMsg.specMsg.P2O3) {
                    Simulation.User.GEO = telemMsg.specMsg;
                    EventBus.Publish<GeoSpecRecievedEvent>(new GeoSpecRecievedEvent());
                }
            }
            else
            {
                Debug.LogError("No geo spec Msg received");
            }
        };

        // tss.OnOpen, OnError, and OnClose events just re-raise events from websockets.
        // Similar to OnTSSTelemetryMsg, create functions with the appropriate return type and parameters, and subscribe to them
        tss.OnOpen += () =>
        {
            Debug.Log("Websocket connection opened");
        };

        tss.OnError += (string e) =>
        {
            Debug.Log("Websocket error occured: " + e);
        };

        tss.OnClose += (e) =>
        {
            Debug.Log("Websocket closed with code: " + e);
        };

        await connecting;

    }

}