using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        GetComponent<AudioSource>().Play();
    }
}
