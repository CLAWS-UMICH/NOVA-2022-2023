using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;

[System.Serializable]
public class OrientationData : MonoBehaviour
{
    static int port = 7000;
    static string ipv4 = IPManager.GetIP(ADDRESSFAM.IPv4);
    string debugTestString = "UDP Host: " + ipv4 + "\n" + "Port: " + port;
    [SerializeField]
    TextMeshPro DebugText;

    
    // Start is called before the first frame update
    Thread listenThread;
    void Start()
    {
        DebugText.text = debugTestString;
        listenThread = new Thread(udpFunction);
        listenThread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // while (Simulation.User.AstronautTasks.messageQueue2.TryDequeue(out string msg))
        // {
        //     Debug.Log("GOT mesg" + msg);
        // }
    }

    public void udpFunction()
    {
        
        UdpClient listener = new UdpClient(port);
        
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(ipv4), port);
        Debug.Log(IPAddress.Parse(ipv4));
        
        try
        {
            while (true)
            {
                Debug.Log("Looking for broadcast");
                byte[] bytes = listener.Receive(ref groupEP);

                Debug.Log($"Received broadcast from {groupEP} :");
                string message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Debug.Log(message);
                Simulation.User.AstronautTasks.messageQueue2.Enqueue(message);
            }
        }
        catch (SocketException e)
        {
            Debug.Log(e);
        }
    }
}

