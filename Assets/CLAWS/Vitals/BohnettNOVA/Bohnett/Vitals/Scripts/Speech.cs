// Conditional Compilation : no speech services supported through webGL

#if UNITY_WEBGL
public class Speech : MonoBehaviour
{
}

#else
using UnityEngine;
using UnityEngine.Windows.Speech;
//using Microsoft.MixedReality.OpenXR
public class Speech : MonoBehaviour
{
    private DictationRecognizer dictationRecognizer;
    void Start()
    {
        // Create a new DictationRecognizer and assign it to the dictationRecognizer field.
        dictationRecognizer = new DictationRecognizer();
        // Subscribe to the DictationResult event.
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        // Subscribe to the DictationHypothesis event.
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        // Start the dictation recognizer.
        dictationRecognizer.Start();
    }
    void OnDestroy()
    {
        // Stop the dictation recognizer.
        dictationRecognizer.Stop();
        // Unsubscribe from the DictationResult and DictationHypothesis events.
        dictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
        dictationRecognizer.DictationHypothesis -= DictationRecognizer_DictationHypothesis;
    }
    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        // Display text in the UI.
        Debug.Log("Recognized text: " +text);
    }
    private void DictationRecognizer_DictationHypothesis(string text)
    {
        // Display updates on text in UI.
        Debug.Log("Real - time update: " +text);
    }
}
#endif
