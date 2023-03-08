using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


/// <summary>
/// Example script of how to navigate a <see cref="Microsoft.MixedReality.Toolkit.UI.ScrollingObjectCollection"/> by pagination.
/// Allows the call to scroll pagination methods from the inspector.
/// </summary>
public class ChatScroll : MonoBehaviour
{
    [SerializeField]
    private ScrollingObjectCollection scrollView;

    /// <summary>
    /// The ScrollingObjectCollection to navigate.
    /// </summary>
    public ScrollingObjectCollection ScrollView
    {
        get
        {
            if (scrollView == null)
            {
                scrollView = GetComponent<ScrollingObjectCollection>();
            }
            return scrollView;
        }
        set
        {
            scrollView = value;
        }
    }

    /// <summary>
    /// Smoothly moves the scroll container a relative number of tiers of cells.
    /// </summary>
    public void ScrollByTier(int amount)
    {
        Debug.Assert(ScrollView != null, "Scroll view needs to be defined before using pagination.");
        scrollView.MoveByTiers(amount);
    }
}

//public class ChatScroll : MonoBehaviour
//{
//    [SerializeField]
//    private ScrollingObjectCollection scrollView;

//    public void ScrollByTier(int amount)
//    {
//        scrollView.MoveByTiers(amount);
//    }
//}
