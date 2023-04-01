using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VEGATextResponseHandler : MonoBehaviour
{
    // SELINA

    void Start()
    {
        EventBus.Subscribe<VEGA_OutputEvent>(ProcessVEGATextResponse);
    }

    void ProcessVEGATextResponse(VEGA_OutputEvent e)
    {

    }
}
