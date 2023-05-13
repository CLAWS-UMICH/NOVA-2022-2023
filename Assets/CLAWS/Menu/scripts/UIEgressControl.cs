using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEgressControl : MonoBehaviour
{
    // Drag Switches here so they can be added to dictionary
    [SerializeField] GameObject supplyLeft;
    [SerializeField] GameObject wasteLeft;
    [SerializeField] GameObject supplyRight;
    [SerializeField] GameObject wasteRight;
    [SerializeField] GameObject pwrLeft;
    [SerializeField] GameObject pwrRight;
    [SerializeField] GameObject oxygenLeft;
    [SerializeField] GameObject oxygenRight;
    [SerializeField] GameObject oxygenVent;
    [SerializeField] GameObject depressPump;

    Dictionary<string, GameObject> switchDict = new Dictionary<string, GameObject>();

    // Green panels
    [SerializeField] GameObject oxygenGreen;
    [SerializeField] GameObject waterGreen;

    // EMU lights, depress pump fault and enable
    [SerializeField] GameObject emu1;
    [SerializeField] GameObject emu2;
    [SerializeField] GameObject fault;
    [SerializeField] GameObject enable;

    // Initialize dictionary
    void Start()
    {
        switchDict["POWER 1"] = pwrLeft;
        switchDict["POWER 2"] = pwrRight;
        switchDict["SUPPLY 1"] = supplyLeft;
        switchDict["SUPPLY 2"] = supplyRight;
        switchDict["WASTE 1"] = wasteLeft;
        switchDict["WASTE 2"] = wasteRight;
        switchDict["OXYGEN 1"] = oxygenLeft;
        switchDict["OXYGEN 2"] = oxygenRight;
        switchDict["O2 VENT"] = oxygenVent;
        switchDict["DEPRESS PUMP"] = depressPump;

        //SetEMUOne(true);
        SetEMUTwo(true);
        //SetEnable(false);
        SetFault(false);
        //SetWaterOK(true);
    }

    // Use to set the yellow flashing square on or off for switches
    // name is the name of the switch you want to affect
    // set isFlashing to true to enable flashing
    // set isFlashing to false to disable flashing
    public void SetSwitchFlashing(string name, bool isFlashing)
    {
        // 
        if (switchDict[name] == null)
        {
            Debug.LogError("Switch name not found in UIEgressPanel. Make sure you are using the correct name when enabling flashing.");
            return;
        }

        switchDict[name].GetComponent<UIEgressSwitchControl>().SetFlashing(isFlashing);
    }

    // If set to true, will turn the oxygen panel green to show that all switches are okay
    public void SetOxygenOK(bool value)
    {
        oxygenGreen.SetActive(value);
    }

    // If set to true, will turn the water panel green to show that all switches are okay
    public void SetWaterOK(bool value)
    {
        waterGreen.SetActive(value);
    }

    // Sets the emu 1 light on or off
    public void SetEMUOne(bool value)
    {
        if (value)
            emu1.GetComponent<SpriteRenderer>().color = Color.green;
        else
            emu1.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    // Sets the emu 2 light on or off
    public void SetEMUTwo(bool value)
    {
        if (value)
            emu2.GetComponent<SpriteRenderer>().color = Color.green;
        else
            emu2.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    // Sets the fault light on or off
    public void SetFault(bool value)
    {
        if (value)
            fault.GetComponent<SpriteRenderer>().color = Color.green;
        else
            fault.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    // Sets the enable light on or off
    public void SetEnable(bool value)
    {
        if (value)
            enable.GetComponent<SpriteRenderer>().color = Color.green;
        else
            enable.GetComponent<SpriteRenderer>().color = Color.gray;
    }
}
