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

    public TaskTextController textController;
    private TaskListController taskController;
    //Subtasks that are visible to the astronaut
    private Subtask[] holdingContainer = new Subtask[2];
    private TaskObj currentTask;
    //taskIndex holds the index of taskList that has the task whose subtasks we show
    private int taskIndex;
    //Current index of the subtask list
    private int currentIndex;
    //Inputed index when the astronaut clicks on either task or subtask
    private int selectedIndex;

    void Start() {
        taskIndex = Simulation.User.AstronautTasks.activeTask;
        taskController = GetComponent<TaskListController>();
        currentIndex = 0;
        selectedIndex = -1;
        UpdateList();
        //EventBus.Subscribe<TasksUpdatedEvent>(RecieveNewList);
    }
    private void UpdateList()
    {
        UpdateHoldingContainer();
        Render();
    }
    // Increments or decrements the current subtask index as the astronaut scrolls
    public void changeCurrentIndex(int incr)
    {
        if((incr < 0 && currentIndex > 0) || (incr > 0 && currentIndex < currentTask.subtaskList.Count - 1))
        {
            currentIndex += incr;
            UpdateHoldingContainer();
            Render();
        }
    }

    // Updates the list of 2 subtasks that will be visible to the astronaut
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
        //Rerender task object
        taskObject.SetActive(true);
        if (currentTask.taskType == 'p')
        {
            taskObject.SetActive(false);
        }
        taskObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = currentTask.taskTitle;
        taskObject.transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = currentTask.taskDesc;
        //Rerender each subtask object
        for (int i = 0; i < 2; i++)
        {
            switch (holdingContainer[i].taskType)
            {
                case '\0':
                    SubtaskObjects[i].SetActive(false);
                    break;
                case 'p':
                    renderHelper(i, FutureSubTaskBackground, holdingContainer[i].subTaskId);
                    break;
                case 'c':
                    renderHelper(i, CurrentSubTaskBackground, holdingContainer[i].subTaskId);
                    break;
                case 'f':
                    renderHelper(i, FutureSubTaskBackground, holdingContainer[i].subTaskId);
                    break;
            }
        }
    }

    private void renderHelper(int index, Material background, int number)
    {
        SubtaskObjects[index].SetActive(true);
        SubtaskObjects[index].transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer>().material = background;
        SubtaskObjects[index].transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro>().text = holdingContainer[index].title;
        SubtaskObjects[index].transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro>().text = holdingContainer[index].description;
        //Change number TODO: make the number update
        SubtaskObjects[index].transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro>().text = number.ToString();
    }
    //Updates the task detailed view window
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
        //If the task object is completed
        if (selectedIndex < 0)
        {
            prepareNewTask();
            EventBus.Publish<TaskCompletedEvent>(new TaskCompletedEvent(currentTask.taskId));
        }
        //If the subtask object is completed
        else
        {
            if (currentTask.subtaskList[selectedIndex + currentIndex].taskType != 'c')
            {
                return;
            }

            //Updates subtask to be previous and then increments the current active subtask
            currentTask.subtaskList[selectedIndex + currentIndex].taskType = 'p';

            if (currentIndex + 1 < currentTask.subtaskList.Count)
            {
                currentIndex += 1;
                currentTask.subtaskList[currentIndex + 1].taskType = 'c';
                changeCurrentIndex(1);
            }
            else
            {
                prepareNewTask();
            }
        }
    }
    //Prepares a new active task once the previous one has been completed
    private void prepareNewTask()
    {
        currentTask.completed = true;
        currentTask.taskType = 'p';
        if (taskIndex + 1 < Simulation.User.AstronautTasks.taskList.Count)
        {
            taskIndex += 1;
            currentTask = Simulation.User.AstronautTasks.taskList[taskIndex];
            Simulation.User.AstronautTasks.taskList[taskIndex].taskType = 'c';
        }
        currentIndex = 0;
        taskController.changeCurrentIndex(1);
        UpdateHoldingContainer();
        Render();
    }
}
