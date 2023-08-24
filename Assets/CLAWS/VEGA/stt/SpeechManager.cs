// Conditional Compilation : no speech services supported through webGL

#if UNITY_WEBGL
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;

public class SpeechManager : MonoBehaviour
{
    void onClick()
    {
    }
}
#else
using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Windows.Speech;
using System.Threading.Tasks;
using TMPro;
public class SpeechManager : MonoBehaviour
{
    public bool speech = false;
    public TextMeshPro text;
    //public Button playbackBtn;
    private IEnumerator coroutine;
    public GameObject panel;
    public SpeechRecognizer recognizer;
    public SpeechSynthesizer synthesizer;
    public SpeechConfig config;
    private object threadLocker = new object();
    private string message;
    GameObject _speech;
    GameObject _animation;
    
    UnityEngine.Windows.Speech.KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keyWords = new Dictionary<string, System.Action>();

    private void RecognizingHandler(object sender, SpeechRecognitionEventArgs e)
    {
        lock (threadLocker)
        {
            message = e.Result.Text;
        }
    }

    // void Start()
    // {
    //     _speech = GameObject.Find("Speech to Text Manager");
    //     _animation = GameObject.Find("GlowingRing");
    //     //_speech = GameObject.Find("Speech to Text Manager").GetComponent<GameObject>();
    //     config = SpeechConfig.FromSubscription("88b980bf22dc44d7949347d3d784be94", "eastus");
    //     config.SpeechRecognitionLanguage = "en-US";
    //     config.SpeechSynthesisVoiceName = "en-US-JennyNeural";
        
    //     recognizer = new SpeechRecognizer(config);
    //     synthesizer = new SpeechSynthesizer(config);
    //     recognizer.Recognizing += RecognizingHandler;

    //     recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

    //     //playbackBtn.onClick.AddListener(onClick);

    //     //keywordRecognizer = new UnityEngine.Windows.Speech.KeywordRecognizer(keyWords.Keys.ToArray());
    //     coroutine = NoSpeech();
    //     StartCoroutine(coroutine);
    // }

    void OnEnable(){
        _speech = GameObject.Find("Speech to Text Manager");
        _animation = GameObject.Find("GlowingRing");
        //_speech = GameObject.Find("Speech to Text Manager").GetComponent<GameObject>();
        config = SpeechConfig.FromSubscription("ee65b57801d0472c919ed6506f0c11f5", "southcentralus");
        config.SpeechRecognitionLanguage = "en-US";
        config.SpeechSynthesisVoiceName = "en-US-JennyNeural";
        
        recognizer = new SpeechRecognizer(config);
        synthesizer = new SpeechSynthesizer(config);
        recognizer.Recognizing += RecognizingHandler;

        recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

        //playbackBtn.onClick.AddListener(onClick);

        //keywordRecognizer = new UnityEngine.Windows.Speech.KeywordRecognizer(keyWords.Keys.ToArray());
        coroutine = NoSpeech();
        StartCoroutine(coroutine);
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
        //Debug.Log(message);
        synthesizer.SpeakTextAsync(message);
    }

    public string GetMessage(){
        return message;
    }

    IEnumerator NoSpeech(){
        int i = 0;
        string prevMessage = message;
        
        while(true){
            yield return new WaitForSeconds(0.8f);
            
            i++;
            if(message!=prevMessage){
                
                speech = true;
                if(panel.activeSelf == false){
                    panel.SetActive(true);
                }

                //Debug.Log("new message");
            }
            // else if(message==""){
            //     Debug.Log("hi");
            // }
            if(i==3 && speech){
                i = 0;
                speech = false;
                //Debug.Log("speech happened");
            }
            else if(i==3 && !speech){
                Debug.Log("hey");
                //Debug.Log("speech did not happen");
                panel.SetActive(false);

                i = 0;
                speech = false;

                EventBus.Publish<VEGA_InputEvent>(new VEGA_InputEvent(prevMessage));
                _speech.SetActive(false);
                // _animation.SetActive(false); //Object reference not set to an instance of an object
//SpeechManager+<NoSpeech>d__17.MoveNext () (at Assets/CLAWS/VEGA/stt/SpeechManager.cs:126)
//UnityEngine.SetupCoroutine.InvokeMoveNext (System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress) 
                
                //_animation.SetActive(false);
            }
            prevMessage = message;
        }
    }
}
#endif
