using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoElement : MonoBehaviour
{
    [SerializeField]
    private bool isGuessed;

    public bool IsGuessed
    {
        get => isGuessed;
        set => isGuessed = value;
    }

    [SerializeField]
    private Image coverImage;

    public Image CoverImage
    {
        get => coverImage;
        set => coverImage = value;
    }

    [SerializeField]
    private Image contentImage;

    public Image ContentImage
    {
        get => contentImage;
        set => contentImage = value;
    }

    [SerializeField] 
    private float timeToHide = 1.5f;

    public float TimeToHide
    {
        get => timeToHide;
        set => timeToHide = value;
    }

    [SerializeField] 
    private float fadeTime = 0.5f;

    public float FadeTime
    {
        get => fadeTime;
        set => fadeTime = value;
    }

    public int ImageId { get; set; } = -1;

    public Action OnMemoElementRevealed { get; set; } = () => { };
    public Action OnMemoElementHidden { get; set; } = () => { };

    private static bool actionLockedByCountdown = false;

    void Awake()
    {
        coverImage.material = new Material(coverImage.material);
        coverImage.material.SetFloat("_Fade", 1);
    }

    public void OnCoverButtonClicked()
    {
        if (actionLockedByCountdown)
        {
            return;
        }
        //coverImage.gameObject.SetActive(false);
        //OnMemoElementRevealed?.Invoke();
        StartCoroutine(FadeOutCoroutine());
    }

    public IEnumerator CountdownAndFadeInCoroutine()
    {
        actionLockedByCountdown = true;
        yield return new WaitForSeconds(timeToHide - fadeTime);
        if (!isGuessed)
        {
            //CoverImage.gameObject.SetActive(true);
            Material coverImageMat = coverImage.material;

            coverImageMat.SetFloat("_Fade", 1);
        }

        //OnMemoElementHidden?.Invoke();
        //actionLockedByCountdown = false;
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
}
