using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;

[System.Serializable]
public class SubtaskListController : MonoBehaviour
{
    [SerializeField]
    Material CurrentSubTaskBackground;
    [SerializeField]
    Material FutureSubTaskBackground;
    [SerializeField]
    GameObject[] SubtaskObjects;
    [SerializeField]
    GameObject taskObject;

    public AstronautSend sendMCC;
    public TaskTextController textController;
    private TaskListController taskController;
    private Subtask[] holdingContainer = new Subtask[2];
    private TaskObj currentTask;
    //taskIndex holds the index of taskList that has the task whose subtasks we show
    private int taskIndex;
    private int currentIndex;
    private int selectedIndex;

    void Start() {
        //SubtaskObjects[1].GetComponent<MeshRenderer> ().material = CurrentTaskBackground;
        taskIndex = Simulation.User.AstronautTasks.viewTask;
        taskController = GetComponent<TaskListController>();
        currentIndex = 0;
        selectedIndex = -1;
        EventBus.Subscribe<TasksUpdatedEvent>(RecieveNewList);
    }

    private void RecieveNewList(TasksUpdatedEvent e)
    {
        UpdateHoldingContainer();
        Render();
    }

    public void changeCurrentIndex(int incr)
    {
        if((incr < 0 && currentIndex > 0) || (incr > 0 && currentIndex < currentTask.subtaskList.Count - 1))
        {
            currentIndex += incr;
            UpdateHoldingContainer();
            Render();
        }
    }
    private void UpdateHoldingContainer()
    {
        currentTask = Simulation.User.AstronautTasks.taskList[taskIndex];
        int size = currentTask.subtaskList.Count;
        for (int i = currentIndex; i < currentIndex + 2; i++)
        {
            if (i < size)
            {
                holdingContainer[i - currentIndex] = Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[i];
            }
            else
            {
                holdingContainer[i - currentIndex] = new Subtask();
            }
        }
    }
    private void Render()
    {
        taskObject.SetActive(true);
        //Change Title text
        taskObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = currentTask.taskTitle;
        //Change Subtitle text
        taskObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = currentTask.taskDesc;
        for (int i = 0; i < 2; i++)
        {
            if (holdingContainer[i].taskType == '\0')
            {
                SubtaskObjects[i].SetActive(false);
            }
            else if (holdingContainer[i].taskType == 'p')
            {
                SubtaskObjects[i].SetActive(true);
                SubtaskObjects[i].transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer> ().material = FutureSubTaskBackground;
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro> ().text = holdingContainer[i].title;
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro> ().text = holdingContainer[i].description;
                //Change number TODO: make the number update
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro> ().text = "1";
            }
            else if (holdingContainer[i].taskType == 'c')
            {
                SubtaskObjects[i].SetActive(true);
                SubtaskObjects[i].transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer> ().material = CurrentSubTaskBackground;
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro> ().text = holdingContainer[i].title;
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro> ().text = holdingContainer[i].description;
                //Change number TODO: make the number update
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro> ().text = "1";
            }
            else if (holdingContainer[i].taskType == 'f')
            {
                SubtaskObjects[i].SetActive(true);
                SubtaskObjects[i].transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer> ().material = FutureSubTaskBackground;
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro> ().text = holdingContainer[i].title;
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro> ().text = holdingContainer[i].description;
                //Change number TODO: make the number update
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro> ().text = "1";
            }
        }
    }

    public void activateEntireText(int index)
    {
        selectedIndex = index;
        if (index < 0)
        {
            textController.setEntireText
            (
                Simulation.User.AstronautTasks.taskList[taskIndex].taskTitle,
                Simulation.User.AstronautTasks.taskList[taskIndex].taskDesc
            );
        }
        else
        {
            textController.setEntireText
            (
                Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[index + currentIndex].title,
                Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[index + currentIndex].description
            );
        }
    }

    public void completeTask()
    {
        textController.gameObject.SetActive(false);
        if (selectedIndex < 0)
        {
            prepareNewTask();
            sendMCC.Send();
        }
        else
        {
            if (currentTask.subtaskList[selectedIndex + currentIndex].taskType != 'c')
            {
                return;
            }
            currentTask.subtaskList[selectedIndex + currentIndex].taskType = 'p';
            if (selectedIndex + currentIndex + 1 < currentTask.subtaskList.Count)
            {
                currentTask.subtaskList[selectedIndex + currentIndex + 1].taskType = 'c';
                changeCurrentIndex(1);
            }
            else
            {
                prepareNewTask();
            }
        }
    }

    private void prepareNewTask()
    {
        currentTask.completed = true;
        currentTask.taskType = 'p';
        if (taskIndex + 1 < Simulation.User.AstronautTasks.taskList.Count)
        {
            Simulation.User.AstronautTasks.taskList[taskIndex + 1].taskType = 'c';
        }
        taskController.changeCurrentIndex(1);
        GetComponent<TaskCollapse>().Toggle();
    }
}
