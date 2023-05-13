using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

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

    [SerializeField] UIEgressSwitchControl i_supplyLeft;
    [SerializeField] UIEgressSwitchControl i_wasteLeft;
    [SerializeField] UIEgressSwitchControl i_supplyRight;
    [SerializeField] UIEgressSwitchControl i_wasteRight;
    [SerializeField] UIEgressSwitchControl i_pwrLeft;
    [SerializeField] UIEgressSwitchControl i_pwrRight;
    [SerializeField] UIEgressSwitchControl i_oxygenLeft;
    [SerializeField] UIEgressSwitchControl i_oxygenRight;
    [SerializeField] UIEgressSwitchControl i_oxygenVent;
    [SerializeField] UIEgressSwitchControl i_depressPump;

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
        //SetEMUTwo(true);
        //SetEnable(false);
        //SetFault(false);
        //SetWaterOK(true);

        i_supplyLeft = supplyLeft.GetComponent<UIEgressSwitchControl>();
        i_wasteLeft = wasteLeft.GetComponent<UIEgressSwitchControl>();
        i_supplyRight = supplyRight.GetComponent<UIEgressSwitchControl>();
        i_wasteRight = wasteRight.GetComponent<UIEgressSwitchControl>();
        i_pwrLeft = pwrLeft.GetComponent<UIEgressSwitchControl>();
        i_pwrRight = pwrRight.GetComponent<UIEgressSwitchControl>();
        i_oxygenLeft = oxygenLeft.GetComponent<UIEgressSwitchControl>();
        i_oxygenRight = oxygenRight.GetComponent<UIEgressSwitchControl>();
        i_oxygenVent = oxygenVent.GetComponent<UIEgressSwitchControl>();
        i_depressPump = depressPump.GetComponent<UIEgressSwitchControl>();

        // subscribe to UIA updates
        EventBus.Subscribe<UIAMsgEvent>(UIA_Updated);
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


    // callback functions for UIA updates
    public void UIA_Updated(UIAMsgEvent e)
    {
        UIAMsg msg = Simulation.User.UIA;
        i_supplyLeft.SetToggleState(msg.ev1_supply);
        i_wasteLeft.SetToggleState(msg.ev_waste);
        i_supplyRight.SetToggleState(msg.ev2_supply);
        i_wasteRight.SetToggleState(msg.ev_waste);
        i_pwrLeft.SetToggleState(msg.emu1);
        i_pwrRight.SetToggleState(msg.emu2);
        i_oxygenLeft.SetToggleState(msg.emu1_O2);
        i_oxygenRight.SetToggleState(msg.emu2_O2);
        i_oxygenVent.SetToggleState(msg.O2_vent);
        i_depressPump.SetToggleState(msg.depress_pump);
    } 
}
