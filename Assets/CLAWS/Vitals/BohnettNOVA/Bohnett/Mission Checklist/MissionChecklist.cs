using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionChecklist : MonoBehaviour
{
    // Just for testing purposes
    [SerializeField] GameObject taskBox;
    [SerializeField] Sprite icon;
    [SerializeField] string testTitle = "Test";
    [SerializeField] string testDescription = "This is a description";

    private List<GameObject> currentTasks = new List<GameObject>();

    public void AddTask()
    {
        GameObject finalTaskBox = Instantiate(taskBox, gameObject.transform);

        TaskBox task = finalTaskBox.GetComponentInChildren<TaskBox>();

        task.ConstructTask(icon, testTitle, testDescription);
        currentTasks.Add(finalTaskBox);
    }


    /* Doesn't actually properly delete stuff, for now, just the checkboxes will check marking complete

    public void FinishTask(GameObject task)
    {
        if (currentTasks.Contains(task))
        {
            currentTasks.Remove(task);
            Destroy(task);
        }
        

    }
    */
}
