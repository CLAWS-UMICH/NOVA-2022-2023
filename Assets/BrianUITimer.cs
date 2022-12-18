using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BrianUITimer : MonoBehaviour
{

    [SerializeField] private GameObject clockText;
    [SerializeField] private BrianTimerData btd;

    // Update is called once per frame
    void Update()
    {
        // Sets clocktext to the text from other script
        clockText.GetComponent<TextMeshPro>().text = btd.cText;
    }
}
