using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCollapse : MonoBehaviour
{
    public GameObject expanded;
    public GameObject taskView;
    private void Start()
    {
        taskView.SetActive(true);
        expanded.SetActive(false);
    }
    // Start is called before the first frame update
    public void Toggle()
    {
        expanded.SetActive(!expanded.activeSelf);
        taskView.SetActive(!taskView.activeSelf);
    }
}
