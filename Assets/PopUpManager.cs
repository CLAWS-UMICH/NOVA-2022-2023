using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;

public class PopUpManager : MonoBehaviour
{
    // SINGLETON
    private static PopUpManager _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // instance variables
    public GameObject PopUpPrefab;
    public GridObjectCollection gridObjectCollection;

    public static void MakePopup(string contents)
    {
        PopUp popup = Instantiate(_instance.PopUpPrefab, _instance.transform).GetComponent<PopUp>();
        popup.SetText(contents);
        popup.SetTimer(3);
        _instance.gridObjectCollection.UpdateCollection();
    }

    public static void MakePopup(string contents, float delay)
    {
        PopUp popup = Instantiate(_instance.PopUpPrefab, _instance.transform).AddComponent<PopUp>();
        popup.SetText(contents);
        popup.SetTimer(delay);
        _instance.gridObjectCollection.UpdateCollection();
    }
}
