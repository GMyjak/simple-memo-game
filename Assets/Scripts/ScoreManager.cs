using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// This component manages player score and high score
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text scoreDisplay;

    [SerializeField] 
    private TMP_Text highScoreDisplay;

    [SerializeField] 
    private GameStateManager stateManager;

    [SerializeField] 
    private int initialScore = 30;

    [SerializeField] 
    private GameOverPanelController gameOverPanel;

    private const string HighScoreKey = "highScore";

    private int score;
    private int highScore = 0;
    private bool isPlaying = false;
    private Coroutine decrementCoroutine;

    private void Awake()
    {
        // Configure score and high score
        score = initialScore;

        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            highScore = PlayerPrefs.GetInt(HighScoreKey);
        }
        else
        {
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }

        // Configure callbacks
        stateManager.OnGameReset += Reset;
        stateManager.OnGameFinished += GameWon;

        stateManager.OnElementsGuessed += () =>
        {
            score += 10;
            UpdateUi();
        };
        
        stateManager.OnElementGuessedWrong += () =>
        {
            score -= 2;
            UpdateUi();
        };

        UpdateUi();
    }

    private void Reset()
    {
        score = initialScore;
        isPlaying = true;
        if (decrementCoroutine != null)
        {
            StopCoroutine(decrementCoroutine);
            decrementCoroutine = null;
        }
        decrementCoroutine = StartCoroutine(DecrementScore());
        UpdateUi();
    }

    private void UpdateUi()
    {
        ScoreDisplay.text = score.ToString();
        HighScoreDisplay.text = highScore.ToString();
    }

    // This coroutine decrements score every one second
    private IEnumerator DecrementScore()
    {
        while (score > 0)
        {
            yield return new WaitForSeconds(1);
            if (isPlaying)
            {
                score--;
                UpdateUi();
            }

            if (score < 0)
            {
                score = 0;
            }
        }

        GameLost();
    }

    private void GameLost()
    {
        isPlaying = false;
        StopCoroutine(decrementCoroutine);
        decrementCoroutine = null;
        gameOverPanel.Show("You lost!", $"Score: {score}", "");
    }

    private void GameWon()
    {
        isPlaying = false;
        StopCoroutine(decrementCoroutine);
        decrementCoroutine = null;

        if (score > highScore)
        {
            highScore = score;
            UpdateUi();
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
            gameOverPanel.Show("You won!", $"Score: {score}", 
                "New highscore!");
        }

        gameOverPanel.Show("You won!", $"Score: {score}", "");
    }

    #region Wrappers

    public TMP_Text ScoreDisplay
    {
        get => scoreDisplay;
        set => scoreDisplay = value;
    }

    public TMP_Text HighScoreDisplay
    {
        get => highScoreDisplay;
        set => highScoreDisplay = value;
    }

    public GameStateManager StateManager
    {
        get => stateManager;
        set => stateManager = value;
    }

    public int InitialScore
    {
        get => initialScore;
        set => initialScore = value;
    }

    public GameOverPanelController GameOverPanel
    {
        get => gameOverPanel;
        set => gameOverPanel = value;
    }

    #endregion
}
