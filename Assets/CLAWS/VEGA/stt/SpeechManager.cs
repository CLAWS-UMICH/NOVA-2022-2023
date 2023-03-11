using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using System.Threading.Tasks;
using TMPro;
public class SpeechManager : MonoBehaviour
{
    public TextMeshPro text;
    //public Button playbackBtn;
    public SpeechRecognizer recognizer;
    public SpeechSynthesizer synthesizer;
    public SpeechConfig config;
    private object threadLocker = new object();
    private string message;
    UnityEngine.Windows.Speech.KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keyWords = new Dictionary<string, System.Action>();

    private void RecognizingHandler(object sender, SpeechRecognitionEventArgs e)
    {
        lock (threadLocker)
        {
            message = e.Result.Text;
        }
    }

    void Start()
    {
        config = SpeechConfig.FromSubscription("88b980bf22dc44d7949347d3d784be94", "eastus");
        config.SpeechRecognitionLanguage = "en-US";
        config.SpeechSynthesisVoiceName = "en-US-JennyNeural";
        
        recognizer = new SpeechRecognizer(config);
        synthesizer = new SpeechSynthesizer(config);
        recognizer.Recognizing += RecognizingHandler;

        recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

        //playbackBtn.onClick.AddListener(onClick);

        //keywordRecognizer = new UnityEngine.Windows.Speech.KeywordRecognizer(keyWords.Keys.ToArray());
    }

    void Update()
    {
        lock (threadLocker)
        {
            text.text = message;
        }
    }
    public void onClick()
    {
        Debug.Log("hi");
        synthesizer.SpeakTextAsync(message);
    }
}
