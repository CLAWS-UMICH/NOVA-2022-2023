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
    [SerializeField] private List<Astronaut> _mcc_Astronauts;

    public int user_index = -1;

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

    public static List<Astronaut> MCC_Astronauts
    {
        get
        {
            return instance._mcc_Astronauts;
        }
        set
        {
            instance._mcc_Astronauts = value;
            // update User
            if (instance.user_index != -1)
            {
                User = instance._mcc_Astronauts[instance.user_index];
            }
        }
    }


}
