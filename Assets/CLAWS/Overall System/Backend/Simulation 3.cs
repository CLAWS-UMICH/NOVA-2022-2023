using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation: MonoBehaviour
{
    // SINGLETON
    private static Simulation instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // INSTANCE
    [SerializeField] private Astronaut _user;

    // STATIC INTERFACE
    public static Astronaut User
    {
        get
        {
            return instance._user;
        }
        set
        {
            instance._user = value;
        }
    }


}
