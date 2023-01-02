using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskItem : MonoBehaviour
{
    //List<SubtaskData> Subtasks = new List<SubtaskData>();
    //Subtasks.Add(SubtaskData("task1"));
    //Subtasks.Add(SubtaskData("task2"));
    //Subtasks.Add(SubtaskData("task3"));
    //
    private bool active = false;
    [SerializeField]
    GameObject SubtaskPrefab;
    GameObject subtaskObject;

    public void OnActivated() {
	if(active) {
	    Destroy(subtaskObject);
	    active = false;
	}
	else {
	    subtaskObject = Instantiate(SubtaskPrefab, gameObject.transform);	
	    active = true;
	}
    }
}
public class SubtaskData {
    string SubtaskText;  
    SubtaskData(string input) {
	SubtaskText = input;
    }
}
