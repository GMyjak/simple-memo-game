using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component controls top bar menu
/// </summary>
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
