using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconRigidRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(90, 0, q.eulerAngles.z);
        transform.rotation = q;
    }
}
