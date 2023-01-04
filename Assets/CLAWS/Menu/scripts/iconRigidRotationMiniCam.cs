using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconRigidRotationMiniCam : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(0, 0, q.eulerAngles.z);
        transform.rotation = q;
    }
}
