using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTasklist : MonoBehaviour
{
    public GameObject taskList;

    public void Toggle()
    {
        taskList.SetActive(false);
    }
}
