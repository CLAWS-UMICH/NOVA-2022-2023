using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoSampleCollapse : MonoBehaviour
{
    public void Toggle(GameObject newView)
    {
        StartCoroutine(ToggleCoroutine(newView));
    }
    
    public void Close() 
    {
        StartCoroutine(CloseCoroutine());
    }

    IEnumerator ToggleCoroutine(GameObject newView)
    {
        yield return new WaitForSeconds(1f);
        newView.SetActive(!newView.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }
    IEnumerator CloseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
