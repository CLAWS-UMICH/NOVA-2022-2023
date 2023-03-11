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

    private void Start()
    {
        initialScale = transform.localScale;
        currentScale = initialScale.x;
    }

    private void Update()
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
