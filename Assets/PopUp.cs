using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    [SerializeField] TextMeshPro tmp;

    public void SetText(string text)
    {
        tmp.text = text;
    }

    public void SetTimer(float delay)
    {
        CloseAfterDelay timer = gameObject.AddComponent<CloseAfterDelay>();
        timer.delay = delay;
        timer.mode = CloseAfterDelay.Mode.onStart;
        timer.destroy = true;
    }
}
