using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationIconHandler : MonoBehaviour
{

    void Update()
    {
        // Set the child object's rotation to the negative of the parent object's rotation
        transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
