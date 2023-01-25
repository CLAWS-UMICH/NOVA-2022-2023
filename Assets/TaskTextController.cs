using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TaskTextController : MonoBehaviour
{
    public TextMeshPro title;
    public TextMeshPro subTitle;

    public void setEntireText(string _title = "", string _subTitle = "")
    {
        gameObject.SetActive(!gameObject.activeSelf);
        title.text = _title;
        subTitle.text = _subTitle;
    }
}
