using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimationController : MonoBehaviour
{
    [SerializeField]
    float maxScale = 2f;
    [SerializeField]
    float minScale = 0.5f;
    [SerializeField]
    float animationSpeed = 1f;

    private Vector3 initialScale;
    private float currentScale;
    private bool scaleUp = true;
    SpeechManager _speech;

    private void Start()
    {
        initialScale = transform.localScale;
        currentScale = initialScale.x;

        _speech = GameObject.Find("Speech to Text Manager").GetComponent<SpeechManager>();

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            if (_speech.speech)
            {
                if (scaleUp)
                {
                    currentScale += Time.deltaTime * animationSpeed; // Increase animation speed
                    if (currentScale >= maxScale)
                    {
                        currentScale = maxScale;
                        scaleUp = false;
                    }
                }
                else
                {
                    currentScale -= Time.deltaTime * animationSpeed; // Increase animation speed
                    if (currentScale <= minScale)
                    {
                        currentScale = minScale;
                        scaleUp = true;
                    }
                }

                transform.localScale = initialScale * currentScale;
            }

            yield return null; // Wait for the next frame
        }
    }
}
