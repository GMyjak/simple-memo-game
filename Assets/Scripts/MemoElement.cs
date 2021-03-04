using System.Collections;
using System.Collections.Generic;
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

    public int ImageId { get; set; } = -1;
}
