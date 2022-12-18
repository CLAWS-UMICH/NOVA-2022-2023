using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PositionOfMessage : MonoBehaviour
{

    private static float x;
    private static float y;
    private static float z;
    private static float heightOfBox = 0.16F;

    [SerializeField] GameObject window;
    
    // the script is not enabled (used in onclick()), 
    // so we should use awake instead of start?
    void Awake() {
        // Set up the first position of the message box basing on the position of the window
        Transform t  = transform;
        Vector3 pos = t.position;
        // Debug.Log(Camera.main.transform.position);
        x = pos[0] + 0.5F;
        y = pos[1] + 0.3F;
        z = pos[2] - 0.6F;
    }

    public Vector3 getPosition() {
        y -= heightOfBox;
        return new Vector3(x, y, z);
    }

    void Update() {
        // Debug.Log(Camera.main.transform.position);
    }

}
