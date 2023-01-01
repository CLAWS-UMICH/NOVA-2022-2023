using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Astronaut: MonoBehaviour
{
    [SerializeField]
    public Vitals vitals;
    //This function is temporary and may be deleted when a proper vitals update is
    //made. It's job is to give an idea of how the event system works.
    public void Temporary_UpdateVitals() {
       Simulation.User.vitals = this.vitals; 
	vitals.setVitals();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	
    }
}
