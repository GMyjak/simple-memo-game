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

    public int ImageId { get; set; } = -1;

    public Action OnMemoElementRevealed { get; set; } = () => { };
    public Action OnMemoElementHidden { get; set; } = () => { };

    private static bool actionLockedByCountdown = false;

    public void OnCoverButtonClicked()
    {
        if (actionLockedByCountdown)
        {
            return;
        }
        coverImage.gameObject.SetActive(false);
        OnMemoElementRevealed?.Invoke();
    }

    public IEnumerator CountdownAndHideCoroutine()
    {
        actionLockedByCountdown = true;
        yield return new WaitForSeconds(timeToHide);
        OnMemoElementHidden?.Invoke();
        if (!isGuessed)
        {
            CoverImage.gameObject.SetActive(true);
        }

        actionLockedByCountdown = false;
    }
}
