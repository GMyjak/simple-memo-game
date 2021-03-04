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

    [SerializeField] 
    private GameStateController gameStateController;

    public GameStateController GameStateController
    {
        get => gameStateController;
        set => gameStateController = value;
    }

    public void On2x2ButtonClick()
    {
        gameStateController.InitializeGameState(2, 2);
        OnBackgroundClick();
    }

    public void On2x4ButtonClick()
    {
        gameStateController.InitializeGameState(2, 4);
        OnBackgroundClick();
    }

    public void On4x4ButtonClick()
    {
        gameStateController.InitializeGameState(4, 4);
        OnBackgroundClick();
    }

    public void OnBackgroundClick()
    {
        PopupMenuBackground.gameObject.SetActive(false);
    }
}
