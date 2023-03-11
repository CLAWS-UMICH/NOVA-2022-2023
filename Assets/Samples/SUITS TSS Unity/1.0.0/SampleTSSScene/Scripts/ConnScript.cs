using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS;

public class ConnScript : MonoBehaviour
{
    TSSConnection tss;
    string tssUri;

    int msgCount = 0;

    TMPro.TMP_Text gpsMsgBox;
    TMPro.TMP_Text imuMsgBox;
    TMPro.TMP_Text evaMsgBox;

    TMPro.TMP_InputField inputField;

    // Start is called before the first frame update
    async void Start()
    {
        tss = new TSSConnection();
        inputField = GameObject.Find("Socket URI Input Field").GetComponent<TMPro.TMP_InputField>();

        gpsMsgBox = GameObject.Find("GPS Msg Box").GetComponent<TMPro.TMP_Text>();
        imuMsgBox = GameObject.Find("IMU Msg Box").GetComponent<TMPro.TMP_Text>();
        evaMsgBox = GameObject.Find("EVA Msg Box").GetComponent<TMPro.TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        // Updates the websocket once per frame
        tss.Update();

    }

    public async void Connect()
    {
        tssUri = inputField.text;
        var connecting = tss.ConnectToURI(tssUri);
        Debug.Log("Connecting to " + tssUri);
        // Create a function that takes asing TSSMsg parameter and returns void. For example:
        // public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg) { ... }
        // Then just subscribe to the OnTSSTelemetryMsg
        tss.OnTSSTelemetryMsg += (telemMsg) =>
        {
            msgCount++;
            Debug.Log("Message #" + msgCount + "\nMessage:\n " + JsonUtility.ToJson(telemMsg, prettyPrint: true));

            if (telemMsg.GPS.Count > 0)
            {
                gpsMsgBox.text = "GPS Msg: " + JsonUtility.ToJson(telemMsg.GPS[0], prettyPrint: true);
            }
            else
            {
                gpsMsgBox.text = "No GPS Msg received";
            }

            if (telemMsg.IMU.Count > 0)
            {
                imuMsgBox.text = "IMU Msg: " + JsonUtility.ToJson(telemMsg.IMU[0], prettyPrint: true);
            }
            else
            {
                imuMsgBox.text = "No IMU Msg received";
            }

            if (telemMsg.EVA.Count > 0)
            {
                evaMsgBox.text = "EVA Msg: " + JsonUtility.ToJson(telemMsg.EVA[0], prettyPrint: true);
            }
            else
            {
                evaMsgBox.text = "No EVA Msg received";
            }
        };

        // tss.OnOpen, OnError, and OnClose events just re-raise events from websockets.
        // Similar to OnTSSTelemetryMsg, create functions with the appropriate return type and parameters, and subscribe to them
        tss.OnOpen += () =>
        {
            Debug.Log("Websocket connectio opened");
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

    // An example handler for the OnTSSMsgReceived event which just serializes to JSON and prints it all out
    // Can be any function that returns void and has a single parameter of type TSS.Msgs.TSSMsg
    public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg)
    {
        Debug.Log("Received the following telemetry data from the TSS:\n" + JsonUtility.ToJson(tssMsg, prettyPrint: true));
    }
}