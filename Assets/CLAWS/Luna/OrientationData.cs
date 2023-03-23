using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class OrientationData : MonoBehaviour
{
    // Start is called before the first frame update
    Thread listenThread;
    void Start()
    {
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
        UdpClient listener = new UdpClient(6002);
        string ipv4 = IPManager.GetIP(ADDRESSFAM.IPv4);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(ipv4), 6002);
        Debug.Log(IPAddress.Parse(ipv4));
        try
        {
            while (true)
            {
                byte[] bytes = listener.Receive(ref groupEP);

                Debug.Log($"Received broadcast from {groupEP} :");
                string message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Debug.Log(message);
                Simulation.User.AstronautTasks.messageQueue2.Enqueue(message);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
    }
}

