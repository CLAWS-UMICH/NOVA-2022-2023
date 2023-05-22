using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Toggles between taskview and taskList expanded view
public class TaskCollapse : MonoBehaviour
{
    public GameObject expanded;
    public GameObject taskView;
    public GameObject cb;
    public GameObject line;
    public TaskListController controller;

    public Screens state;

    private void Start()
    {
        taskView.SetActive(false);
        expanded.SetActive(true);
        state = Screens.TaskList;
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

    public void OpenTask(){
        StartCoroutine(OpenChildren(expanded));
        StartCoroutine(OpenChildren(cb));
        StartCoroutine(OpenChildren(line));
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.TaskList, LUNAState.right));
    }

    public void forward()
    {
        Toggle();
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.TaskList_CurrentTask, LUNAState.right));
        state = Screens.TaskList_CurrentTask;
    }

    public void backward()
    {
        Toggle();
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.TaskList, LUNAState.right));
        state = Screens.TaskList;
    }

    IEnumerator OpenChildren(GameObject Screen)
    {
        yield return new WaitForSeconds(1f);
        Screen.SetActive(true);
    }
}
