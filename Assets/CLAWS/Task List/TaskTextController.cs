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
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        title.text = _title;
        subTitle.text = _subTitle;
    }
}
