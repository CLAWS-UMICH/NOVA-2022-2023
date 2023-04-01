using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VEGACommandHandler : MonoBehaviour
{
    // KRITI

    void Start()
    {
        EventBus.Subscribe<VEGA_OutputEvent>(ProcessVEGACommand);
    }

    void ProcessVEGACommand(VEGA_OutputEvent e)
    {

    }
}
