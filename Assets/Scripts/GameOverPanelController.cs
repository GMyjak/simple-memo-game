using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gameStatusText;

    public TMP_Text GameStatusText
    {
        get => gameStatusText;
        set => gameStatusText = value;
    }

    [SerializeField]
    private TMP_Text scoreDisplayText;

    public TMP_Text ScoreDisplayText
    {
        get => scoreDisplayText;
        set => scoreDisplayText = value;
    }

    [SerializeField]
    private TMP_Text additionalMessageText;

    public TMP_Text AdditionalMessageText
    {
        get => additionalMessageText;
        set => additionalMessageText = value;
    }

    [SerializeField] 
    private Image parentBackground;

    public Image ParentBackground
    {
        get => parentBackground;
        set => parentBackground = value;
    }

    void Awake()
    {
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
}
