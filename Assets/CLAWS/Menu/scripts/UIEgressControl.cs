using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TSS.Msgs;
using TMPro;

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

    [SerializeField] TextMeshPro tmp;

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
        EventBus.Subscribe<UIACompleteEvent>(UIA_Complete);
        UIA_Updated(new UIAMsgEvent());
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(WaitForStep0());
    }

    public void UIA_Complete(UIACompleteEvent e)
    {
        Debug.Log("UIA Egress Complete");
        gameObject.SetActive(false);
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
            if (Simulation.User.UIA.emu1_pwr_switch == false 
                && Simulation.User.UIA.ev1_supply_switch == false
                && Simulation.User.UIA.emu1_water_waste == false
                && Simulation.User.UIA.emu1_o2_supply_switch == false
                && Simulation.User.UIA.o2_vent_switch == false
                && Simulation.User.UIA.depress_pump_switch == false)
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
            if (Simulation.User.UIA.emu1_pwr_switch == true 
                || Simulation.User.UIA_State.emu1_is_booted)
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
        PopUpManager.MakePopup("Switch O2 Vent to OPEN until < 23 psi", 6);
        SetSwitchFlashing("O2 VENT", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.o2_vent_switch == true)
            {
                break;
            }
        }
        SetSwitchFlashing("O2 VENT", false);
        yield return new WaitForSeconds(1f);
        tmp.text = "29 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "24 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "19 psi";
        PopUpManager.MakePopup("Switch O2 Vent to CLOSE");
        SetSwitchFlashing("O2 VENT", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.o2_vent_switch == false)
            {
                break;
            }
        }
        tmp.text = "";
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
            if (Simulation.User.UIA.emu1_o2_supply_switch == true)
            {
                break;
            }
        }
        SetSwitchFlashing("OXYGEN 1", false);
        tmp.text = "19 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "200 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "1400 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "2300 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "3200 psi";
        PopUpManager.MakePopup("Switch O2 Supply to CLOSE");
        SetSwitchFlashing("OXYGEN 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_o2_supply_switch == false)
            {
                break;
            }
        }
        tmp.text = "";
        // step complete
        SetSwitchFlashing("OXYGEN 1", false);
        yield return new WaitForSeconds(1f);

        // vent
        PopUpManager.MakePopup("Switch O2 Vent to OPEN until < 23 psi", 6);
        SetSwitchFlashing("O2 VENT", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.o2_vent_switch == true)
            {
                break;
            }
        }
        SetSwitchFlashing("O2 VENT", false);
        tmp.text = "3200 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "2100 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "1020 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "240 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "30 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "10 psi";
        PopUpManager.MakePopup("Switch O2 Vent to CLOSE");
        SetSwitchFlashing("O2 VENT", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.o2_vent_switch == false)
            {
                break;
            }
        }
        tmp.text = "";
        // step complete
        SetSwitchFlashing("O2 VENT", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaitForStep4());
    }
    IEnumerator WaitForStep4()
    {
        PopUpManager.MakePopup("Switch O2 Supply to OPEN until > 1500 psi");
        SetSwitchFlashing("OXYGEN 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_o2_supply_switch == true)
            {
                break;
            }
        }
        SetSwitchFlashing("OXYGEN 1", false);
        tmp.text = "100 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "900 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "1600 psi";
        PopUpManager.MakePopup("Switch O2 Supply to CLOSE");
        SetSwitchFlashing("OXYGEN 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_o2_supply_switch == false)
            {
                break;
            }
        }

        tmp.text = "";
        // step complete
        SetSwitchFlashing("OXYGEN 1", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaitForStep5());
    }
    IEnumerator WaitForStep5()
    {
        // dump waste
        PopUpManager.MakePopup("Switch O2 EV-1 Waste to OPEN until level < 5%", 6);
        SetSwitchFlashing("WASTE 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_water_waste == true)
            {
                break;
            }
        }
        SetSwitchFlashing("WASTE 1", false);
        tmp.text = "53 %";
        yield return new WaitForSeconds(1f);
        tmp.text = "14 %";
        yield return new WaitForSeconds(1f);
        tmp.text = "8 %";
        yield return new WaitForSeconds(1f);
        tmp.text = "4 %";
        PopUpManager.MakePopup("Switch EV-1 Waste to CLOSE");
        SetSwitchFlashing("WASTE 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_water_waste == false)
            {
                break;
            }
        }
        // step complete
        SetSwitchFlashing("WASTE 1", false);
        yield return new WaitForSeconds(1f);

        // refill waste
        PopUpManager.MakePopup("Switch O2 EV-1 Supply to OPEN until water level > 95%", 6);
        SetSwitchFlashing("SUPPLY 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.ev1_supply_switch == true)
            {
                break;
            }
        }
        SetSwitchFlashing("SUPPLY 1", false);
        tmp.text = "4 %";
        yield return new WaitForSeconds(1f);
        tmp.text = "16 %";
        yield return new WaitForSeconds(1f);
        tmp.text = "42 %";
        yield return new WaitForSeconds(1f);
        tmp.text = "85 %";
        yield return new WaitForSeconds(1f);
        tmp.text = "97 %";
        PopUpManager.MakePopup("Switch EV-1 Supply to CLOSE");
        SetSwitchFlashing("SUPPLY 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.ev1_supply_switch == false)
            {
                break;
            }
        }
        // step complete
        tmp.text = "";
        SetSwitchFlashing("SUPPLY 1", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaitForStep6());
    }
    IEnumerator WaitForStep6()
    {
        PopUpManager.MakePopup("Switch Depress Pump to ON until airlock pressure < 10.2 psi", 6);
        SetSwitchFlashing("DEPRESS PUMP", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.depress_pump_switch == true)
            {
                break;
            }
        }
        SetSwitchFlashing("DEPRESS PUMP", false);
        tmp.text = "13.5 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "11.9 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "10.1 psi";
        PopUpManager.MakePopup("Switch Depress Pump to OFF");
        SetSwitchFlashing("DEPRESS PUMP", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.depress_pump_switch == false)
            {
                break;
            }
        }
        // step complete
        tmp.text = "";
        SetSwitchFlashing("DEPRESS PUMP", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaitForStep7());
    }
    IEnumerator WaitForStep7()
    {
        PopUpManager.MakePopup("Switch O2 Supply to OPEN until > 3000 psi", 6);
        SetSwitchFlashing("OXYGEN 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_o2_supply_switch == true)
            {
                break;
            }
        }
        SetSwitchFlashing("OXYGEN 1", false);
        tmp.text = "19 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "490 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "1680 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "3100 psi";
        PopUpManager.MakePopup("Switch O2 Supply to CLOSE");
        SetSwitchFlashing("OXYGEN 1", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.emu1_o2_supply_switch == false)
            {
                break;
            }
        }
        // step complete
        tmp.text = "";
        SetSwitchFlashing("OXYGEN 1", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(WaitForStep8());
    }
    IEnumerator WaitForStep8()
    {
        PopUpManager.MakePopup("Switch Depress Pump to ON until airlock pressure < 0.1 psi", 6);
        SetSwitchFlashing("DEPRESS PUMP", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.depress_pump_switch == true)
            {
                break;
            }
        }
        SetSwitchFlashing("DEPRESS PUMP", false);
        tmp.text = "14.6 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "6.1 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "0.2 psi";
        yield return new WaitForSeconds(1f);
        tmp.text = "< 0.1 psi";
        PopUpManager.MakePopup("Switch Depress Pump to OFF");
        SetSwitchFlashing("DEPRESS PUMP", true);
        while (true)
        {
            yield return new WaitForSeconds(1f);
            // step condition
            if (Simulation.User.UIA.depress_pump_switch == false)
            {
                break;
            }
        }
        // step complete
        SetSwitchFlashing("DEPRESS PUMP", false);
        yield return new WaitForSeconds(1f);
        EventBus.Publish<UIACompleteEvent>(new UIACompleteEvent());
        PopUpManager.MakePopup("UIA Procedures are complete. You may exit the airlock");
    }


    public void Next()
    {
        switch (counter)
        {
            case 0:
                fakeUIA.fakeUIA.ev1_supply_switch = false;
                break;
            case 1:
                fakeUIA.fakeUIA.emu1_pwr_switch = true;
                break;
            case 2:
                fakeUIA.fakeUIA.o2_vent_switch = true;
                counter++;
                break;
            case 3:
                fakeUIA.fakeUIA.o2_vent_switch = true;
                break;
            case 4:
                fakeUIA.fakeUIA.o2_vent_switch = false;
                break;
            case 5:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = true;
                counter++;
                break;
            case 6:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = true;
                break;
            case 7:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = false;
                break;
            case 8:
                fakeUIA.fakeUIA.o2_vent_switch = true;
                counter++;
                break;
            case 9:
                fakeUIA.fakeUIA.o2_vent_switch = true;
                break;
            case 10:
                fakeUIA.fakeUIA.o2_vent_switch = false;
                break;
            case 11:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = true;
                counter++;
                break;
            case 12:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = true;
                break;
            case 13:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = false;
                break;
            case 14:
                fakeUIA.fakeUIA.emu1_water_waste = true;
                counter++;
                break;
            case 15:
                fakeUIA.fakeUIA.emu1_water_waste = true;
                break;
            case 16:
                fakeUIA.fakeUIA.emu1_water_waste = false;
                break;
            case 17:
                fakeUIA.fakeUIA.ev1_supply_switch = true;
                counter++;
                break;
            case 18:
                fakeUIA.fakeUIA.ev1_supply_switch = true;
                break;
            case 19:
                fakeUIA.fakeUIA.ev1_supply_switch = false;
                break;
            case 20:
                fakeUIA.fakeUIA.depress_pump_switch = true;
                counter++;
                break;
            case 21:
                fakeUIA.fakeUIA.depress_pump_switch = true;
                break;
            case 22:
                fakeUIA.fakeUIA.depress_pump_switch = false;
                break;
            case 23:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = true;
                counter++;
                break;
            case 24:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = true;
                break;
            case 25:
                fakeUIA.fakeUIA.emu1_o2_supply_switch = false;
                break;
            case 26:
                fakeUIA.fakeUIA.depress_pump_switch = true;
                break;
            case 27:
                fakeUIA.fakeUIA.depress_pump_switch = true;
                break;
            case 28:
                fakeUIA.fakeUIA.depress_pump_switch = false;
                break;
        }
        counter++;
        fakeUIA.Fake_SetUIA();
    }

    public void EgressSkip()
    {
        EventBus.Publish<UIACompleteEvent>(new UIACompleteEvent());
        StartCoroutine(egressSkip());
    }

    IEnumerator egressSkip()
    {
        yield return new WaitForSeconds(3f);
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
        i_supplyLeft.SetToggleState(msg.ev1_supply_switch);
        i_wasteLeft.SetToggleState(msg.emu1_water_waste);
        i_wasteRight.SetToggleState(msg.emu1_water_waste);
        i_pwrLeft.SetToggleState(msg.emu1_pwr_switch);
        i_oxygenLeft.SetToggleState(msg.emu1_o2_supply_switch);
        i_oxygenVent.SetToggleState(msg.o2_vent_switch);
        i_depressPump.SetToggleState(msg.depress_pump_switch);
    } 
}
