using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class YawOffset : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 360.0f)]
    private float offset;
    [SerializeField]
    GameObject playerCam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerCam.transform.position;
        while (Simulation.User.AstronautTasks.messageQueue2.TryDequeue(out string msg))
        {
            gameObject.transform.rotation = Quaternion.Euler(0, float.Parse(msg), 0);
        }
    }
}
