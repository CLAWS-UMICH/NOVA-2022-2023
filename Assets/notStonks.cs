using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notStonks : MonoBehaviour
{
    // Update is called once per frame
    public void stonks(int stonkState)
    {
        Application.LoadLevel(stonkState);
    }
}
