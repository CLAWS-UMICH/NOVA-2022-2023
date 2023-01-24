using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Subtask
{
    public string title;
    public string description;
    public char taskType;

    public Subtask() {
        title = null;
        description = null;
        taskType = '\0';
    }
    public Subtask(string title_in, string desc_in, char type_in) {
        title = title_in;
        description = desc_in;
        taskType = type_in;
    }
}
