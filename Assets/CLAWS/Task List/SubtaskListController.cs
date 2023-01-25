using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Subtask[] holdingContainer = new Subtask[2];

    //taskIndex holds the index of taskList that has the task whose subtasks we show
    private int taskIndex = Simulation.User.AstronautTasks.viewTask;
    private int current_index = 0;

    void Start() {
        //SubtaskObjects[1].GetComponent<MeshRenderer> ().material = CurrentTaskBackground;
    }
    public void changeCurrentIndex(int incr)
    {
        current_index += incr;
        UpdateHoldingContainer();
        Render();
    }
    private void UpdateHoldingContainer()
    {
        int size = Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList.Count;
        for (int i = current_index; i < current_index + 2; i++)
        {
            if (i < size)
            {
                holdingContainer[i - current_index] = Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[i];
            }
            else
            {
                holdingContainer[i - current_index] = new Subtask();
            }
        }
    }
    private void Render()
    {
        for (int i = 0; i < 3; i++)
        {
            if (holdingContainer[i].taskType == '\0')
            {
                SubtaskObjects[i].SetActive(false);
            }
            else if (holdingContainer[i].taskType == 'p')
            {
                //TODO: Implement changing taskType
            }
            else if (holdingContainer[i].taskType == 'c')
            {
                //TODO: Implement changing taskType
            }
            else if (holdingContainer[i].taskType == 'f')
            {
                //TODO: Implement changing taskType
            }
        }
    }

    public void activateEntireText(int index)
    {
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
                Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[index + current_index].title,
                Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[index + current_index].description
            );
        }
    }
}
