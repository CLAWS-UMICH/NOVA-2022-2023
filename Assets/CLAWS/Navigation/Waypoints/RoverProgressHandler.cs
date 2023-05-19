using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoverProgressHandler : MonoBehaviour
{
    public Slider slider;
    public TextMeshPro valueText;

    public void UpdateProgressBar(float value)
    {
        //// Ensure the percentage value is between 0 and 100
        value = Mathf.Clamp(value, 0f, 100f);

        //// Update the progress bar's value
        slider.value = value / 100f;
        valueText.text = value.ToString();
    }
}
