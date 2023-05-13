using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;

public class UIEgressControl : MonoBehaviour
{
    public int counter = 0;
    public FakeTSSMessageSender fakeUIA;

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

        // fake UIA
        fakeUIA = GameObject.Find("Simulation Manager").GetComponent<FakeTSSMessageSender>();

        // subscribe to UIA updates
        EventBus.Subscribe<UIAMsgEvent>(UIA_Updated);
        UIA_Updated(new UIAMsgEvent());

        StartCoroutine(WaitForStep0());
    }

    
    // handle each step of the UIA procedure
    IEnumerator WaitForStep0()
    {
        yield return new WaitForSeconds(3f);
        PopUpManager.MakePopup("Set All UIA Switches to Off");
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step 0 condition
            if (Simulation.User.UIA.emu1 == false 
                && Simulation.User.UIA.ev1_supply == false
                && Simulation.User.UIA.emu1_O2 == false
                && Simulation.User.UIA.emu2 == false
                && Simulation.User.UIA.ev2_supply == false
                && Simulation.User.UIA.ev_waste == false
                && Simulation.User.UIA.emu2_O2 == false
                && Simulation.User.UIA.O2_vent == false
                && Simulation.User.UIA.depress_pump == false)
            {
                break;
            }
        }
        // step 0 complete
        yield return new WaitForSeconds(3f);
        StartCoroutine(WaitForStep1());
    }

    IEnumerator WaitForStep1()
    {
        PopUpManager.MakePopup("Switch EMU 1 PWR to ON");
        SetSwitchFlashing("POWER 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1 == true)
            {
                break;
            }
        }
        // step complete
        SetSwitchFlashing("POWER 1", false);
        SetEMUOne(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaitForStep2());
    }

    IEnumerator WaitForStep2()
    {
        PopUpManager.MakePopup("Switch O2 Vent to OPEN until < 23 psi");
        SetSwitchFlashing("O2 VENT", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.O2_vent == true)
            {
                break;
            }
        }
        SetSwitchFlashing("O2 VENT", false);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA_CONTROLS.o2_supply_pressure1 < 23)
            {
                break;
            }
            
        }
        PopUpManager.MakePopup("Switch O2 Vent to CLOSE");
        SetSwitchFlashing("O2 VENT", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.O2_vent == false)
            {
                break;
            }
        }
        // step complete
        SetSwitchFlashing("O2 VENT", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaitForStep3());
    }

    IEnumerator WaitForStep3()
    {
        // supply
        PopUpManager.MakePopup("Switch O2 Supply to OPEN until > 3000 psi");
        SetSwitchFlashing("OXYGEN 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_O2 == true)
            {
                break;
            }
        }
        SetSwitchFlashing("OXYGEN 1", false);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA_CONTROLS.o2_supply_pressure1 > 3000)
            {
                break;
            }
        }
        PopUpManager.MakePopup("Switch O2 Supply to CLOSE");
        SetSwitchFlashing("OXYGEN 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_O2 == false)
            {
                break;
            }
        }
        // step complete
        SetSwitchFlashing("OXYGEN 1", false);
        yield return new WaitForSeconds(1f);

        // vent
        PopUpManager.MakePopup("Switch O2 Vent to OPEN until < 23 psi");
        SetSwitchFlashing("O2 VENT", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.O2_vent == true)
            {
                break;
            }
        }
        SetSwitchFlashing("O2 VENT", false);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA_CONTROLS.o2_supply_pressure1 < 23)
            {
                break;
            }

        }
        PopUpManager.MakePopup("Switch O2 Vent to CLOSE");
        SetSwitchFlashing("O2 VENT", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.O2_vent == false)
            {
                break;
            }
        }
        // step complete
        SetSwitchFlashing("O2 VENT", false);
        yield return new WaitForSeconds(1f);
        // StartCoroutine(WaitForStep4());
    }




    public void Next()
    {
        switch (counter)
        {
            case 0:
                fakeUIA.fakeUIA.ev1_supply = false;
                fakeUIA.fakeUIA.emu2 = false;
                break;
            case 1:
                fakeUIA.fakeUIA.emu1 = true;
                break;
            case 2:
                fakeUIA.fakeUIA.O2_vent = true;
                break;
            case 3:
                fakeUIA.fakeUIAControl.o2_supply_pressure1 = 22;
                break;
            case 4:
                fakeUIA.fakeUIA.O2_vent = false;
                break;
            case 5:
                fakeUIA.fakeUIA.emu1_O2 = true;
                break;
            case 6:
                fakeUIA.fakeUIAControl.o2_supply_pressure1 = 3000;
                break;
            case 7:
                fakeUIA.fakeUIA.emu1_O2 = false;
                break;
            case 8:
                fakeUIA.fakeUIA.O2_vent = true;
                break;
            case 9:
                fakeUIA.fakeUIAControl.o2_supply_pressure1 = 22;
                break;
            case 10:
                fakeUIA.fakeUIA.O2_vent = false;
                break;
            case 11:
                fakeUIA.fakeUIA.emu1_O2 = true;
                break;
            case 12:
                fakeUIA.fakeUIAControl.o2_supply_pressure1 = 1500;
                break;
            case 13:
                fakeUIA.fakeUIA.emu1_O2 = false;
                break;
            case 14:
                fakeUIA.fakeUIA.ev_waste = true;
                break;
            case 15:
                fakeUIA.fakeUIAControl.ev1_waste = "true";
                break;
            case 16:
                fakeUIA.fakeUIA.ev_waste = false;
                break;
            case 17:
                fakeUIA.fakeUIA.ev1_supply = true;
                break;
            case 18:
                fakeUIA.fakeUIAControl.ev1_supply = "true";
                break;
            case 19:
                fakeUIA.fakeUIA.ev1_supply = false;
                break;
            case 20:
                fakeUIA.fakeUIA.depress_pump = true;
                break;
            case 21:
                fakeUIA.fakeUIAControl.depress_pump = 10;
                break;
            case 22:
                fakeUIA.fakeUIA.depress_pump = false;
                break;
            case 23:
                fakeUIA.fakeUIA.emu1_O2 = true;
                break;
            case 24:
                fakeUIA.fakeUIAControl.o2_supply_pressure1 = 3000;
                break;
            case 25:
                fakeUIA.fakeUIA.emu1_O2 = false;
                break;
            case 26:
                fakeUIA.fakeUIA.depress_pump = true;
                break;
            case 27:
                fakeUIA.fakeUIAControl.depress_pump = 0;
                break;
            case 28:
                fakeUIA.fakeUIA.depress_pump = false;
                break;
        }
        counter++;
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
