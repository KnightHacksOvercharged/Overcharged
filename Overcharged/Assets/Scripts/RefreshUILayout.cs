using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshUILayout : MonoBehaviour
{
    // Refresh the content fitters of parent and all descendants
    //------------------------------------------------------------//
    public static void RefreshContentFitters(RectTransform parent)
    //------------------------------------------------------------//
    {
        RefreshContentFitter(parent);
    }

    // Recursively refresh the UI elements under transform with content fitters
    // Parent objects require the size of the children
    //---------------------------------------------------------------//
    private static void RefreshContentFitter(RectTransform transform)
    //---------------------------------------------------------------//
    {
        if (transform == null || !transform.gameObject.activeSelf)
        {
            return;
        }

        foreach (RectTransform child in transform)
        {
            RefreshContentFitter(child);
        }

        ContentSizeFitter contentSizeFitter = transform.GetComponent<ContentSizeFitter>();

        if (contentSizeFitter != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
        }
    }
}
