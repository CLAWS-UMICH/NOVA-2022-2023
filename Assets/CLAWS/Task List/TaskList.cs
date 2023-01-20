using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

[System.Serializable]
public class TaskList : MonoBehaviour
{
    [SerializeField]
    public GameObject[] taskObjects;
    public double progress;
    //taskList holds all tasks including previously completed tasks and future tasks.
    public static List<TaskObj> taskList = new List<TaskObj>();
    public ConcurrentQueue<string> messageQueue;
    public class currentView {
        TaskObj[] holdingContainer = new TaskObj[3];
        int current_index = 0;
        public void changeCurrentIndex(int index) {
            current_index = index;
            UpdateHoldingContainer();
            Render();
        }
        private void UpdateHoldingContainer() {
            int size = taskList.Count;
            for(int i = current_index; i < current_index + 3; i++) {
                if(i < size) {
                    holdingContainer[i - current_index] = taskList[i];
                }
                else {
                    holdingContainer[i-current_index] = new TaskObj();
                }
            }
        }
        private void Render() {
            for(int i = 0; i < 3; i++) {
                if(holdingContainer[i].taskType == '\0') {
                    Simulation.User.AstronautTasks.taskObjects[i].SetActive(false);
                }
                else if(holdingContainer[i].taskType == 'p') {
                    //TODO: Implement changing taskType
                }
                else if(holdingContainer[i].taskType == 'c') {
                    //TODO: Implement changing taskType
                }
                else if(holdingContainer[i].taskType == 'f') {
                    //TODO: Implement changing taskType
                }
            }
        }
    };

    public TaskList()
    {
        messageQueue = new ConcurrentQueue<string>();
        taskList = new List<TaskObj>();
        progress = 0;
    }
    // This function will take the json data from a websocket and parse the data into the tasklist.
    public void tasksUpdated() {}
}

