using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS;
using TSS.Msgs;


public class TelemetryServerManager : MonoBehaviour
{
    TSSConnection tss;
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
        string username = "Tester 1";
        string university = "University of Michigan";
        string user_guid = "fdbee7e5-9887-495e-aabb-f10d1386a7e9";
        tssUri = "ws://localhost:3001"; // TODO fix
        var connecting = tss.ConnectToURI(tssUri, team_name, username, university, user_guid);
        Debug.Log("Connecting to " + tssUri);
        
        tss.OnTSSTelemetryMsg += (telemMsg) =>
        {
            msgCount++;

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
                Simulation.User.GEO = telemMsg.specMsg;
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