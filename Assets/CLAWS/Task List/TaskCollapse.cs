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
        taskView.SetActive(false);
        expanded.SetActive(true);
    }

    public void Toggle()
    {
        StartCoroutine(ToggleCoroutine());
    }

    IEnumerator ToggleCoroutine()
    {
        yield return new WaitForSeconds(1f);
        expanded.SetActive(!expanded.activeSelf);
        taskView.SetActive(!taskView.activeSelf);
        controller.refresh();
    }
}
