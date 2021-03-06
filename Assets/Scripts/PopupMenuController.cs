using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component controls panel with board size choice
/// </summary>
public class PopupMenuController : MonoBehaviour
{
    [SerializeField]
    private Image popupMenuBackground;

    [SerializeField] 
    private GameStateManager gameStateManager;

    // When menu is opened, you can click on background to close it
    // This cannot happen when game has just been launched and isn't initialized
    private bool isFirstInitialization = true;

    public void On2x2ButtonClick()
    {
        gameStateManager.InitializeGameState(2, 2);
        isFirstInitialization = false;
        OnBackgroundClick();
    }

    public void On2x4ButtonClick()
    {
        gameStateManager.InitializeGameState(2, 4);
        isFirstInitialization = false;
        OnBackgroundClick();
    }

    public void On4x4ButtonClick()
    {
        gameStateManager.InitializeGameState(4, 4);
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

    #region Wrappers

    public Image PopupMenuBackground
    {
        get => popupMenuBackground;
        set => popupMenuBackground = value;
    }

    public GameStateManager GameStateManager
    {
        get => gameStateManager;
        set => gameStateManager = value;
    }

    #endregion
}
