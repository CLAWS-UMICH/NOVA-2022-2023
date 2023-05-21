using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class CountdownScript : MonoBehaviour
{
    [SerializeField]
    TextMeshPro Number;
    [SerializeField]
    int CountdownNumber;

    void Awake()
    {
        StartCoroutine(Countdown());
        Number.text = CountdownNumber.ToString();
    }

    IEnumerator Countdown() {
        while(CountdownNumber > 0) {
            yield return new WaitForSeconds(1f);
            CountdownNumber--;
            Number.text = CountdownNumber.ToString();
        }
        gameObject.SetActive(false);
    }
}
