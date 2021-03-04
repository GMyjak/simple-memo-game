using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenuController : MonoBehaviour
{
    [SerializeField]
    private Image popupMenuBackground;
    public Image PopupMenuBackground
    {
        get => popupMenuBackground;
        set => popupMenuBackground = value;
    }

    public void On2x2ButtonClick()
    {
        Debug.Log("2x2");
    }

    public void On2x4ButtonClick()
    {
        Debug.Log("2x4");
    }

    public void On4x4ButtonClick()
    {
        Debug.Log("4x4");
    }

    public void OnBackgroundClick()
    {
        PopupMenuBackground.gameObject.SetActive(false);
    }
}
