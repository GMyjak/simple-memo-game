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

    private bool isFirstInitialization = true;

    public void On2x2ButtonClick()
    {
        gameStateController.InitializeGameState(2, 2);
        isFirstInitialization = false;
        OnBackgroundClick();
    }

    public void On2x4ButtonClick()
    {
        gameStateController.InitializeGameState(2, 4);
        isFirstInitialization = false;
        OnBackgroundClick();
    }

    public void On4x4ButtonClick()
    {
        gameStateController.InitializeGameState(4, 4);
        isFirstInitialization = false;
        OnBackgroundClick();
    }

    public void OnBackgroundClick()
    {
        if (!isFirstInitialization)
        {
            PopupMenuBackground.gameObject.SetActive(false);
        }
    }
}
