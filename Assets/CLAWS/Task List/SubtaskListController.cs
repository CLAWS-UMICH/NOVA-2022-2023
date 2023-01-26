using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private int taskIndex;
    private int current_index;

    void Start() {
        //SubtaskObjects[1].GetComponent<MeshRenderer> ().material = CurrentTaskBackground;
        taskIndex = Simulation.User.AstronautTasks.viewTask;
        current_index = 0;
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
                SubtaskObjects[i].SetActive(true);
                //Change Background
                SubtaskObjects[i].transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer> ().material = FutureSubTaskBackground;
                //Change title
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro> ().text = holdingContainer[i].title;
                //Change description 
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro> ().text = holdingContainer[i].description;
                //Change number TODO: make the number update
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro> ().text = "1";
            }
            else if (holdingContainer[i].taskType == 'c')
            {
                //TODO: Implement changing taskType
                SubtaskObjects[i].SetActive(true);
                //Change Background
                SubtaskObjects[i].transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer> ().material = CurrentSubTaskBackground;
                //Change title
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro> ().text = holdingContainer[i].title;
                //Change description 
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro> ().text = holdingContainer[i].description;
                //Change number TODO: make the number update
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro> ().text = "1";
            }
            else if (holdingContainer[i].taskType == 'f')
            {
                //TODO: Implement changing taskType
                SubtaskObjects[i].SetActive(true);
                //Change Background
                SubtaskObjects[i].transform.GetChild(2).transform.GetChild(0).GetComponent<MeshRenderer> ().material = FutureSubTaskBackground;
                //Change title
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshPro> ().text = holdingContainer[i].title;
                //Change description 
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(2).GetComponent<TextMeshPro> ().text = holdingContainer[i].description;
                //Change number TODO: make the number update
                SubtaskObjects[i].transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshPro> ().text = "1";
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
            Debug.Log("LKJHGFDSDFGHJKLKJHGFDFGHJKL");
            Debug.Log(Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[index + current_index].title);
            textController.setEntireText
            (
                Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[index + current_index].title,
                Simulation.User.AstronautTasks.taskList[taskIndex].subtaskList[index + current_index].description
            );
        }
    }
}
