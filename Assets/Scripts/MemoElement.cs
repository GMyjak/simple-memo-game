using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This component is mounted on MemoElement prefab (single card on game board)
/// </summary>
public class MemoElement : MonoBehaviour
{
    [SerializeField]
    private bool isGuessed;

    [SerializeField]
    private Image coverImage;

    [SerializeField]
    private Image contentImage;

    [SerializeField] 
    private float timeToHide = 1.5f;

    [SerializeField] 
    private float fadeTime = 0.5f;


    public int ImageId { get; set; } = -1;
    public Action OnMemoElementRevealed { get; set; } = () => { };
    public Action OnMemoElementHidden { get; set; } = () => { };

    // Two elements have been clicked, are currently revealed and so you can't click any other
    private static bool actionLockedByCountdown = false;

    void Awake()
    {
        // New material has to be instantiated, otherwise fading one element will fae them all
        coverImage.material = new Material(coverImage.material);
        coverImage.material.SetFloat("_Fade", 1);
    }

    public void OnCoverButtonClicked()
    {
        if (actionLockedByCountdown)
        {
            return;
        }
        StartCoroutine(FadeOutCoroutine());
    }

    public IEnumerator CountdownAndFadeInCoroutine()
    {
        actionLockedByCountdown = true;
        yield return new WaitForSeconds(timeToHide - fadeTime);
        if (!isGuessed)
        {
            Material coverImageMat = coverImage.material;
            coverImageMat.SetFloat("_Fade", 1);
        }
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float alpha = 1;
        Material coverImageMat = coverImage.material;

        coverImageMat.SetFloat("_Fade", alpha);

        while (alpha > 0)
        {
            alpha -= (Time.deltaTime / fadeTime);
            yield return new WaitForEndOfFrame();
            if (alpha < 0)
            {
                alpha = 0;
            }
            coverImageMat.SetFloat("_Fade", alpha);
        }

        OnMemoElementRevealed?.Invoke();
    }

    private IEnumerator FadeInCoroutine()
    {
        float alpha = 0;
        Material coverImageMat = coverImage.material;

        coverImageMat.SetFloat("_Fade", alpha);

        while (alpha < 1)
        {
            alpha += (Time.deltaTime / fadeTime);
            yield return new WaitForEndOfFrame();
            if (alpha > 1)
            {
                alpha = 1;
            }
            coverImageMat.SetFloat("_Fade", alpha);
        }

        OnMemoElementHidden?.Invoke();
        actionLockedByCountdown = false;
    }

    #region Wrappers

    public bool IsGuessed
    {
        get => isGuessed;
        set => isGuessed = value;
    }

    public Image CoverImage
    {
        get => coverImage;
        set => coverImage = value;
    }

    public Image ContentImage
    {
        get => contentImage;
        set => contentImage = value;
    }

    public float TimeToHide
    {
        get => timeToHide;
        set => timeToHide = value;
    }

    public float FadeTime
    {
        get => fadeTime;
        set => fadeTime = value;
    }

    #endregion
}
