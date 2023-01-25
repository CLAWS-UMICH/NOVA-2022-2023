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
    TaskObj[] holdingContainer = new TaskObj[3];
    int current_index = 0;

    void Start()
    {
        EventBus.Subscribe<TasksUpdatedEvent>(RecieveNewList);
    }

    void RecieveNewList(TasksUpdatedEvent e)
    {
        //Update current_index
        current_index = e.index;
        UpdateHoldingContainer();
        Render();
    }

    public void changeCurrentIndex(int incr)
    {
        current_index += incr;
        UpdateHoldingContainer();
        Render();
    }
    private void UpdateHoldingContainer()
    {
        int size = Simulation.User.AstronautTasks.taskList.Count;
        for (int i = current_index; i < current_index + 3; i++)
        {
            if (i < size)
            {
                holdingContainer[i - current_index] = Simulation.User.AstronautTasks.taskList[i];
            }
            else
            {
                holdingContainer[i - current_index] = new TaskObj();
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
                taskObjects[i].SetActive(true);
                taskObjects[i].transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material = FutureTaskBackground;
                //Change Title text
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro> ().text = holdingContainer[i].taskTitle;
                //Change Subtitle text
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro> ().text = holdingContainer[i].taskDesc;
                //Change circle color
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material = PastCircleBackground;
                //Change Circle number color
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro> ().color = new Color32(255, 255, 255, 255);
                //Change Circle number TODO: Find actual index
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro> ().text = "1";
            }
            else if (holdingContainer[i].taskType == 'c')
            {
                taskObjects[i].SetActive(true);
                taskObjects[i].transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material = CurrentTaskBackground;
                //Change Title text
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro> ().text = holdingContainer[i].taskTitle;
                //Change Subtitle text
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro> ().text = holdingContainer[i].taskDesc;
                //Change circle color
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material = CurrentCircleBackground;
                //Change Circle number color
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro> ().color = new Color32(255, 0, 0, 0);
                //Change Circle number TODO: Find actual index
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro> ().text = "1";
                
            }
            else if (holdingContainer[i].taskType == 'f')
            {
                taskObjects[i].SetActive(true);
                taskObjects[i].transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material = FutureTaskBackground;
                //Change Title text
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshPro> ().text = holdingContainer[i].taskTitle;
                //Change Subtitle text
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro> ().text = holdingContainer[i].taskDesc;
                //Change circle color
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material = FutureCircleBackground;
                //Change Circle number color
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro> ().color = new Color32(255, 255, 255, 255);
                //Change Circle number TODO: Find actual index
                taskObjects[i].transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro> ().text = "1";
            }
        }
    }
}
