using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarController : MonoBehaviour
{
    [SerializeField]
    private Image popupMenuBackground;

    public Image PopupMenuBackground
    {
        get => popupMenuBackground;
        set => popupMenuBackground = value;
    }

    public void OnMenuButtonClicked()
    {
        PopupMenuBackground.gameObject.SetActive(true);
    }
}
