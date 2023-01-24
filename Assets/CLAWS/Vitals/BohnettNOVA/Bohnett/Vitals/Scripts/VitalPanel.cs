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
    [SerializeField] GameObject[] meter;
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
        for (int i = 0; i < meter.Length; i++)
        {
            if (meter[i] != null && meter[i].GetComponent<MeterFill>() != null)
            {
                meter[i].GetComponent<MeterFill>().SetFillOnMeter(Int32.Parse(updateFields[i].GetComponent<TMP_Text>().text));
            }
        }
        
    }

    public void SetPanelError(Color color)
    {
        if (meshRenderer == null)
        {
            return;
        }
        meshRenderer.enabled = true;
        meshRenderer.material.color = color;
    }

}
