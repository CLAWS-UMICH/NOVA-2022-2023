using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// Subscription works correctly if the first element in the updatedFields list
// is a meter with an integer for the representation
public class VitalPanel : MonoBehaviour
{
    [SerializeField] PanelInformation panelInformation;
    [SerializeField] GameObject[] updateFields;
    [SerializeField] GameObject meter;
    [SerializeField] MeshRenderer meshRenderer;

    

    private void Start()
    {
        EventBus.Subscribe<VitalsUpdatedEvent>(SetMeterFill);
    }
    public PanelInformation GetPanelInformation()
    {
        return panelInformation;
    }

    public GameObject[] GetUpdateFields()
    {
        return updateFields;
    }

    public void SetMeterFill(VitalsUpdatedEvent e)
    {
        if (meter != null && meter.GetComponent<MeterFill>() != null)
        {
            Debug.Log(updateFields[0].GetComponent<TMP_Text>().text);
            meter.GetComponent<MeterFill>().SetFillOnMeter(Int32.Parse(updateFields[0].GetComponent<TMP_Text>().text));
        }
    }

    public void SetPanelError(Color color)
    {
        meshRenderer.enabled = true;
        meshRenderer.material.color = color;
    }

}
