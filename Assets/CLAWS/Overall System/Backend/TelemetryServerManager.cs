using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS;



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
        tssUri = "ws://localhost:3001"; // TODO fix
        var connecting = tss.ConnectToURI(tssUri);
        Debug.Log("Connecting to " + tssUri);
        
        tss.OnTSSTelemetryMsg += (telemMsg) =>
        {
            msgCount++;

            if (telemMsg.GPS.Count > 0)
            {
                Simulation.User.GPS = telemMsg.GPS[0];
                EventBus.Publish<UpdatedGPSEvent>(new UpdatedGPSEvent());
            }
            else
            {
                Debug.LogError("No GPS Msg received");
            }

            if (telemMsg.IMU.Count > 0)
            {
                Simulation.User.IMU = telemMsg.IMU[0];
            }
            else
            {
                Debug.LogError("No IMU Msg received");
            }

            if (telemMsg.EVA.Count > 0)
            {
                Simulation.User.EVA = telemMsg.EVA[0];
                EventBus.Publish<VitalsUpdatedEvent>(new VitalsUpdatedEvent());
            }
            else
            {
                Debug.LogError("No EVA Msg received");
            }

            if (telemMsg.UIA.Count > 0)
            {
                Simulation.User.UIA = telemMsg.UIA[0];
            }
            else
            {
                Debug.LogError("No UIA Msg received");
            }

            if (telemMsg.UIA_CONTROLS.Count > 0)
            {
                Simulation.User.UIA_CONTROLS = telemMsg.UIA_CONTROLS[0];
            }
            else
            {
                Debug.LogError("No UIA_CONTROLS Msg received");
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