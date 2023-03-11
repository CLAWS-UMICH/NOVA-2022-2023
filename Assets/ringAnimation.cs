using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ringAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject glowingRing;
    float speed = 1f;
    float intensity = 1f;

    private Material ringMaterial;

    private void Start()
    {
        ringMaterial = glowingRing.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        float emission = Mathf.PingPong(Time.time * speed, 1.0f) * intensity;
        Color baseColor = ringMaterial.color;
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        ringMaterial.SetColor("_EmissionColor", finalColor);
    }
}
