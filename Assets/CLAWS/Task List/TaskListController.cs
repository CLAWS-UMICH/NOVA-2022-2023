using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TaskListController : MonoBehaviour
{
    [SerializeField]
    Material CurrentTaskBackground;
    [SerializeField]
    Material FutureTaskBackground;
    [SerializeField]
    Material CurrentCircleBackground;
    [SerializeField]
    Material FutureCircleBackground;
    [SerializeField]
    Material PastCircleBackground;
    [SerializeField]
    GameObject[] taskObjects;
    //The list of 3 task objects that will be displayed to the astronaut
    TaskObj[] holdingContainer = new TaskObj[3];
    int currentIndex = 0;

    void Start()
    {
        //EventBus.Subscribe<TasksUpdatedEvent>(RecieveNewList);
        EventBus.Subscribe<CloseEvent>(Callback_CloseTask);
        refresh();
    }

    public void refresh()
    {
        UpdateHoldingContainer();
        Render();
    }

    private void RecieveNewList(TasksUpdatedEvent e)
    {
        //Update current_index
        currentIndex = e.index;
        UpdateHoldingContainer();
        Render();
    }
    //Increments or decrements the task list view as the astronaut scrolls
    public void changeCurrentIndex(int incr)
    {
        if ((incr < 0 && currentIndex > 0) || (incr > 0 && currentIndex < Simulation.User.AstronautTasks.taskList.Count - 1))
        {
            currentIndex += incr;
            UpdateHoldingContainer();
            Render();
        }
    }
    private void UpdateHoldingContainer()
    {
        int size = Simulation.User.AstronautTasks.taskList.Count;
        for (int i = currentIndex; i < currentIndex + 3; i++)
        {
            if (i < size)
            {
                holdingContainer[i - currentIndex] = Simulation.User.AstronautTasks.taskList[i];
            }
            else
            {
                holdingContainer[i - currentIndex] = new TaskObj();
            }
        }
    }
    private void Render()
    {
        for (int i = 0; i < 3; i++)
        {
            if (holdingContainer[i].taskType == '\0')
            {
                taskObjects[i].SetActive(false);
            }
            else if (holdingContainer[i].taskType == 'p')
            {
                renderHelper(i, FutureTaskBackground, PastCircleBackground, new Color32(255, 255, 255, 255), holdingContainer[i].taskId);
            }
            else if (holdingContainer[i].taskType == 'c')
            {
                renderHelper(i, CurrentTaskBackground, CurrentCircleBackground, new Color32(255, 0, 0, 0), holdingContainer[i].taskId);
            }
            else if (holdingContainer[i].taskType == 'f')
            {
                renderHelper(i, FutureTaskBackground, FutureCircleBackground, new Color32(255, 255, 255, 255), holdingContainer[i].taskId);
            }
        }
    }

    private void renderHelper(int index, Material background, Material circBackground, Color32 color, int number)
    {
        taskObjects[index].SetActive(true);
        taskObjects[index].transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = background;
        //Change Title text
        taskObjects[index].transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = holdingContainer[index].taskTitle;
        //Change Subtitle text
        taskObjects[index].transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = holdingContainer[index].taskDesc;
        //Change circle color
        taskObjects[index].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = circBackground;
        //Change Circle number color
        taskObjects[index].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().color = color;
        //Change Circle number TODO: Find actual index
        taskObjects[index].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = number.ToString();
    }

    public void Callback_CloseTask(CloseEvent e){
        if (StateMachineNOVA.LUNA == LUNAState.right) 
        {
            CloseTasks();
        }
    }

    public void CloseTasks() {
        for (int a = 0; a < transform.childCount; a++)
        {
            StartCoroutine(_CloseScreen(transform.GetChild(a).gameObject));
        }
        EventBus.Publish<ScreenChangedEvent>(new ScreenChangedEvent(Screens.Home, LUNAState.center));

    }

    IEnumerator _CloseScreen(GameObject Screen)
    {
        yield return new WaitForSeconds(1f);
        Screen.SetActive(false);
    }
}
