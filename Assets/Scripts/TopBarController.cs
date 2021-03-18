using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component controls top bar menu
/// </summary>
public class TopBarController : MonoBehaviour
{
    [SerializeField]
    private Image popupMenuBackground;

    private Controls controls; 

    public Image PopupMenuBackground
    {
        get => popupMenuBackground;
        set => popupMenuBackground = value;
    }

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.UI.OpenMenu.performed += ctx => OnMenuButtonClicked();

        controls.UI.Enable();
    }

    public void OnDisable()
    {
        controls.UI.Disable();
    }

    public void OnMenuButtonClicked()
    {
        PopupMenuBackground.gameObject.SetActive(true);
    }
}
