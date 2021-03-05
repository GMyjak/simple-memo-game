using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text scoreDisplay;
    
    public TMP_Text ScoreDisplay
    {
        get => scoreDisplay;
        set => scoreDisplay = value;
    }

    [SerializeField] 
    private TMP_Text highScoreDisplay;
    
    public TMP_Text HighScoreDisplay
    {
        get => highScoreDisplay;
        set => highScoreDisplay = value;
    }

    [SerializeField] 
    private GameStateManager stateManager;

    public GameStateManager StateManager
    {
        get => stateManager;
        set => stateManager = value;
    }

    [SerializeField] 
    private int initialScore = 30;

    public int InitialScore
    {
        get => initialScore;
        set => initialScore = value;
    }

    [SerializeField] 
    private GameOverPanelController gameOverPanel;

    public GameOverPanelController GameOverPanel
    {
        get => gameOverPanel;
        set => gameOverPanel = value;
    }

    private int score;
    private int highScore = 0;
    private string highScoreKey = "highScore";
    private bool isPlaying = false;
    private Coroutine decrementCoroutine;

    private void Awake()
    {
        score = initialScore;

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

        if (PlayerPrefs.HasKey(highScoreKey))
        {
            highScore = PlayerPrefs.GetInt(highScoreKey);
        }
        else
        {
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }

        UpdateUi();
    }

    private void Reset()
    {
        score = initialScore;
        isPlaying = true;
        decrementCoroutine = StartCoroutine(DecrementScore());
        UpdateUi();
    }

    private void UpdateUi()
    {
        ScoreDisplay.text = score.ToString();
        HighScoreDisplay.text = highScore.ToString();
    }

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
        gameOverPanel.Show("You lost!", $"Score: {score}", "");
    }

    private void GameWon()
    {
        isPlaying = false;
        StopCoroutine(decrementCoroutine);
        
        if (score > highScore)
        {
            highScore = score;
            UpdateUi();
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
            gameOverPanel.Show("You won!", $"Score: {score}", 
                "New highscore!");
        }

        gameOverPanel.Show("You won!", $"Score: {score}", "");
    }
}
