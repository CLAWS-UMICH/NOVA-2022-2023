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
    TMPro.TMP_Text simulationStatesMsgBox;
    TMPro.TMP_Text simulationFailuresMsgBox;
    TMPro.TMP_Text uiaSwitchesMsgBox;
    TMPro.TMP_Text specMsgBox;
    TMPro.TMP_Text roverMsgBox;

    TMPro.TMP_InputField uriInputField;

    View view = View.Page1;

    

    // Start is called before the first frame update
    // In 
    async void Start()
    {
        tss = new TSSConnection();

        //The rest of the Start() method is for setting up the sample
        uriInputField = GameObject.Find("Socket URI Input Field").GetComponent<TMPro.TMP_InputField>();

        Debug.Log("start");

        gpsMsgBox = GameObject.Find("GPS Msg Box").GetComponent<TMPro.TMP_Text>();
        Debug.Log("gps");

        imuMsgBox = GameObject.Find("IMU Msg Box").GetComponent<TMPro.TMP_Text>();
        Debug.Log("imu");

        simulationStatesMsgBox = GameObject.Find("Simulation States Msg Box").GetComponent<TMPro.TMP_Text>();
        Debug.Log("sim states");

        simulationFailuresMsgBox = GameObject.Find("Simulation Failures Msg Box").GetComponent<TMPro.TMP_Text>();
        Debug.Log("sim failures");
        simulationFailuresMsgBox.gameObject.SetActive(false);

        uiaSwitchesMsgBox = GameObject.Find("UIA Switches Msg Box").GetComponent<TMPro.TMP_Text>();
        Debug.Log("uia switches");
        uiaSwitchesMsgBox.gameObject.SetActive(false);

        specMsgBox = GameObject.Find("Spec Msg Box").GetComponent<TMPro.TMP_Text>();
        Debug.Log("spec");
        specMsgBox.gameObject.SetActive(false);

        roverMsgBox = GameObject.Find("Rover Msg Box").GetComponent<TMPro.TMP_Text>();
        Debug.Log("rover");
        roverMsgBox.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // Updates the websocket once per frame
        tss.Update();

    }

    public async void Connect()
    {
      
        string team_name = "Interscholar";
        string username = "Team 1";
        string university = "Some Uni";
        string user_guid = "fdbee7e5-9887-495e-aabb-f10d1386a7e9";
        tssUri = uriInputField.text;

        // Pass in your team's information here. user_guid is most important - it must match your visionkit
        var connecting = tss.ConnectToURI(tssUri, team_name, username, university, user_guid);


        Debug.Log("Connecting to " + tssUri);

        // Create a function that takes a single parameter of type TSSMsg and returns void. For example:
        // public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg) { ... }
        // Then subscribe to the OnTSSTelemetryMsg with the following syntax: tss.OnTSSTelemetryMsg += your_function_here
        tss.OnTSSTelemetryMsg += (telemMsg) =>
        {
            msgCount++;
            Debug.Log("Message #" + msgCount + "\nMessage:\n " + JsonUtility.ToJson(telemMsg, prettyPrint: true));

            gpsMsgBox.text = "GPS Msg:\n" + JsonUtility.ToJson(telemMsg.gpsMsg, prettyPrint: true);
            imuMsgBox.text = "IMU Msg:\n" + JsonUtility.ToJson(telemMsg.imuMsg, prettyPrint: true);
            simulationStatesMsgBox.text = "Simulation States Msg:\n" + JsonUtility.ToJson(telemMsg.simulationStates, prettyPrint: true);

            specMsgBox.text = "Spec Msg:\n" + JsonUtility.ToJson(telemMsg.specMsg, prettyPrint: true);
            uiaSwitchesMsgBox.text = "UIA Switches Msg:\n" + JsonUtility.ToJson(telemMsg.uiaMsg, prettyPrint: true);
            simulationFailuresMsgBox.text = "Simulation Failures Msg: " + JsonUtility.ToJson(telemMsg.simulationFailures, prettyPrint: true);

            roverMsgBox.text = "Rover Msg: " + JsonUtility.ToJson(telemMsg.roverMsg, prettyPrint: true);

            // evaMsgBox.text = "EVA Msg: " + JsonUtility.ToJson(telemMsg.simulationStates, prettyPrint: true);

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

    public void SendNavigationButtonCallback()
    {
        // However you get the lat and lon from the user, pass them to the following method
        tss.SendRoverNavigateCommand(10.023f, 11.245f);
    }
    public void SendRecallButtonCallback()
    {
        // Call the following method
        tss.SendRoverRecallCommand();
    }


    // An example handler for the OnTSSMsgReceived event which just serializes to JSON and prints it all out
    // Can be any function that returns void and has a single parameter of type TSS.Msgs.TSSMsg
    public static void PrintInfo(TSS.Msgs.TSSMsg tssMsg)
    {
        Debug.Log("Received the following telemetry data from the TSS:\n" + JsonUtility.ToJson(tssMsg, prettyPrint: true));
    }

    // This is just for the sample, it has no importance to the TSS
    public void ChangeView()
    {
        if(this.view == View.Page1)
        {
            this.view = View.Page2;
            roverMsgBox.gameObject.SetActive(false);

            gpsMsgBox.gameObject.SetActive(true);
            imuMsgBox.gameObject.SetActive(true);
            simulationStatesMsgBox.gameObject.SetActive(true);
        }
        else if (this.view == View.Page2)
        {
            this.view = View.Page3;
            gpsMsgBox.gameObject.SetActive(false);
            imuMsgBox.gameObject.SetActive(false);
            simulationStatesMsgBox.gameObject.SetActive(false);

            simulationFailuresMsgBox.gameObject.SetActive(true);
            specMsgBox.gameObject.SetActive(true);
            uiaSwitchesMsgBox.gameObject.SetActive(true);
        }
        else
        {
            this.view = View.Page1;
            simulationFailuresMsgBox.gameObject.SetActive(false);
            specMsgBox.gameObject.SetActive(false);
            uiaSwitchesMsgBox.gameObject.SetActive(false);

            roverMsgBox.gameObject.SetActive(true);
        }
    }


    public enum View
    {
        Page1, Page2, Page3
    }

}
