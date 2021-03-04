using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
