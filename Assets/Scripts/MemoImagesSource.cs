using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component provides way to configure Sprites used in MemoElements
/// </summary>
public class MemoImagesSource : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> images;

    public List<Sprite> Images
    {
        get => images;
        set => images = value;
    }
}
