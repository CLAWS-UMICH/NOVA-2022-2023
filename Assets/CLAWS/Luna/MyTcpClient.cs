using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnityEngine;

#if !UNITY_EDITOR && !UNITY_WEBGL
using System.Threading.Tasks;
#endif

public class MyTcpClient : MonoBehaviour
{

    //String variable that will be sent to the server
    [SerializeField]
    public string clientMessage;

#if !UNITY_EDITOR && !UNITY_WEBGL
    private bool _useUWP = true;
    private Windows.Networking.Sockets.StreamSocket socket;
    private Task exchangeTask;
#else
    private bool _useUWP = false;
    System.Net.Sockets.TcpClient client;
    System.Net.Sockets.NetworkStream stream;
    private Thread exchangeThread;
#endif

    private Byte[] bytes = new Byte[256];
    private StreamWriter writer;
    private StreamReader reader;

    public void Start()
    {
        //Server ip address and port
        Connect("192.168.191.251", "8000");
    }

    public void Connect(string host, string port)
    {
        if (_useUWP)
        {
            ConnectUWP(host, port);
        }
        else
        {
            ConnectUnity(host, port);
        }
    }

#if UNITY_EDITOR || UNITY_WEBGL
    private void ConnectUWP(string host, string port)
#else
    private async void ConnectUWP(string host, string port)
#endif
    {
#if UNITY_EDITOR || UNITY_WEBGL
        errorStatus = "UWP TCP client used in Unity!";
#else
        try
        {
            if (exchangeTask != null) StopExchange();

            socket = new Windows.Networking.Sockets.StreamSocket();
            Windows.Networking.HostName serverHost = new Windows.Networking.HostName(host);
            await socket.ConnectAsync(serverHost, port);

            Stream streamOut = socket.OutputStream.AsStreamForWrite();
            writer = new StreamWriter(streamOut) { AutoFlush = true };

            Stream streamIn = socket.InputStream.AsStreamForRead();
            reader = new StreamReader(streamIn);

            successStatus = "Connected!";
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }

    private void ConnectUnity(string host, string port)
    {
#if !UNITY_EDITOR && !UNITY_WEBGL
        errorStatus = "Unity TCP client used in UWP!";
#else
        try
        {
            if (exchangeThread != null) StopExchange();

            client = new System.Net.Sockets.TcpClient(host, Int32.Parse(port));
            stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };

            successStatus = "Connected!";
        }
        catch (Exception e)
        {
            errorStatus = e.ToString();
        }
#endif
    }

    private bool exchanging = false;
    private bool exchangeStopRequested = false;
    private string lastPacket = null;

    private string errorStatus = null;
    private string successStatus = null;

    public void Update()
    {

        if (errorStatus != null)
        {
            Debug.Log(errorStatus);
            errorStatus = null;
        }
        if (successStatus != null)
        {
            Debug.Log(successStatus);
            successStatus = null;
        }
    }

    public void ExchangePackets(){
        try {
            if (clientMessage == "Ping")
            {
                writer.Write("Ping");
            }
            else if (clientMessage == "Start Game")
            {
                writer.Write("Start Game");
            }

        } catch(Exception e) {
            Debug.Log(e.ToString());
        }

    }


    public void StopExchange()
    {
        Debug.Log("Stopped Exchange");
        exchangeStopRequested = true;

#if UNITY_EDITOR || UNITY_WEBGL
        if (exchangeThread != null)
        {
            exchangeThread.Abort();
            stream.Close();
            client.Close();
            writer.Close();
            reader.Close();

            stream = null;
            exchangeThread = null;
        }
#else
        if (exchangeTask != null)
        {
            exchangeTask.Wait();
            socket.Dispose();
            writer.Dispose();
            reader.Dispose();

            socket = null;
            exchangeTask = null;
        }
#endif
        writer = null;
        reader = null;
    }

    public void OnDestroy()
    {
        StopExchange();
    }

    public void ButtonPressed() {
        ExchangePackets();
        Debug.Log("Button Pressed");
    }
}