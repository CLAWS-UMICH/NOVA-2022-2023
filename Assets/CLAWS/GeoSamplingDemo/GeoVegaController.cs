using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeoVegaController : MonoBehaviour
{
    [SerializeField] private TextMeshPro speakingText;
    [SerializeField] private TextMeshPro minSpeakingText;

    private void Update()
    {
        DisplayLastSentence();
    }

    void DisplayLastSentence()
    {
        string[] str = speakingText.text.Split(".");
        minSpeakingText.text = str[str.Length-1];
    }

    public void Retry()
    {
        speakingText.text = "";
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
