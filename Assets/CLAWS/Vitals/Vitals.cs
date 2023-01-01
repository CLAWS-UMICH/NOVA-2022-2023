using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vitals
{
    public double O2;
    public double CO2;
    public double WaterPressure;
    public double SuitPressure;
    public void setVitals() {
	EventBus.Publish<VitalsUpdated>(new VitalsUpdated());
    }
}
