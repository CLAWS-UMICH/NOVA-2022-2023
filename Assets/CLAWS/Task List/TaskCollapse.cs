using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Toggles between taskview and taskList expanded view
public class TaskCollapse : MonoBehaviour
{
    public GameObject expanded;
    public GameObject taskView;
    public TaskListController controller;

    private void Start()
    {
        taskView.SetActive(true);
        expanded.SetActive(false);
    }

    public void Toggle()
    {
        expanded.SetActive(!expanded.activeSelf);
        taskView.SetActive(!taskView.activeSelf);
        controller.refresh();
    }
}
