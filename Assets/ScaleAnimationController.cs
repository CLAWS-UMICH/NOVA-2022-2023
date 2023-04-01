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
    //private IEnumerator coroutine;
    SpeechManager _speech;
    
    private void Start()
    {
        initialScale = transform.localScale;
        currentScale = initialScale.x;

        _speech = GameObject.Find("Speech to Text Manager").GetComponent<SpeechManager>();

        //coroutine = Animate();
        //StartCoroutine(coroutine);
        //Debug.Log(_speech.speech);

    }

    private void Update()
    {
        
        if (_speech.speech)
        {
            if (scaleUp)
            {
                currentScale += Time.deltaTime * animationSpeed;
                if (currentScale >= maxScale)
                {
                    currentScale = maxScale;
                    scaleUp = false;
                }
            }
            else
            {
                currentScale -= Time.deltaTime * animationSpeed;
                if (currentScale <= minScale)
                {
                    currentScale = minScale;
                    scaleUp = true;
                }
            }

            transform.localScale = initialScale * currentScale;
        }
    }
}
