using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component controls panel that will be displayed when game ends (win or loss)
/// </summary>
public class GameOverPanelController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gameStatusText;

    [SerializeField]
    private TMP_Text scoreDisplayText;

    [SerializeField]
    private TMP_Text additionalMessageText;

    [SerializeField] 
    private Image parentBackground;

    void Awake()
    {
        // For some reason this makes gameObject set active never possible again
        //gameObject.SetActive(false);
    }

    public void Show(string gameOverMessage, string scoreDisplayMessage, string additionalInfoMessage)
    {
        GameStatusText.text = gameOverMessage;
        ScoreDisplayText.text = scoreDisplayMessage;
        AdditionalMessageText.text = additionalInfoMessage;
        ParentBackground.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    #region Wrappers
    
    public TMP_Text GameStatusText
    {
        get => gameStatusText;
        set => gameStatusText = value;
    }

    public TMP_Text ScoreDisplayText
    {
        get => scoreDisplayText;
        set => scoreDisplayText = value;
    }

    public TMP_Text AdditionalMessageText
    {
        get => additionalMessageText;
        set => additionalMessageText = value;
    }

    public Image ParentBackground
    {
        get => parentBackground;
        set => parentBackground = value;
    }

    #endregion
}
