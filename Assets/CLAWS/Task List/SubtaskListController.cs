using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubtaskListController : MonoBehaviour
{
    [SerializeField]
    Material CurrentTaskBackground;
    [SerializeField]
    GameObject[] SubtaskObjects;
    void Start() {
        SubtaskObjects[1].GetComponent<MeshRenderer> ().material = CurrentTaskBackground;
    }
    Subtask[] holdingContainer = new Subtask[2];
    //taskIndex holds the index of taskList that has the task whose subtasks we show
    int taskIndex = 0;
    int current_index = 0;
    public void changeCurrentIndex(int index)
    {
        current_index = index;
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
}
