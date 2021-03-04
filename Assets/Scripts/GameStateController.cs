using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameStateController : MonoBehaviour
{
    [SerializeField] 
    private GameObject memoObjectPrefab;

    public GameObject MemoObjectPrefab
    {
        get => memoObjectPrefab;
        set => memoObjectPrefab = value;
    }

    [SerializeField] 
    private GridLayoutGroup gridLayout;

    public GridLayoutGroup GridLayout
    {
        get => gridLayout;
        set => gridLayout = value;
    }

    [SerializeField]
    private MemoImagesSource imagesSource;

    public MemoImagesSource ImagesSource
    {
        get => imagesSource;
        set => imagesSource = value;
    }

    private List<MemoElement> memoElements;
    private int xSize;
    private int ySize;

    private MemoElement currentlyRevealedElement;

    public Action OnGameReset = () => { };
    public Action OnElementsGuessed = () => { };

    private void Awake()
    {
        memoElements = new List<MemoElement>();
    }

    public void ResetGameState()
    {
        foreach (var elem in memoElements)
        {
            Destroy(elem.gameObject);
        }

        memoElements.Clear();
        xSize = 0;
        ySize = 0;
    }

    public void InitializeGameState(int xSize, int ySize)
    {
        int numberOfElements = xSize * ySize;

        if (xSize % 2 != 0 && ySize % 2 != 0)
        {
            throw new Exception($"Number of elements has to be even (current: {numberOfElements})");
        }

        ResetGameState();

        this.xSize = xSize;
        this.ySize = ySize;

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = ySize;

        for (int i = 0; i < numberOfElements; i++)
        {
            var newObject = Instantiate(MemoObjectPrefab, GridLayout.transform);
            var memoElement = newObject.GetComponent<MemoElement>();
            memoElements.Add(memoElement);

            memoElement.OnMemoElementRevealed += () =>
            {
                if (currentlyRevealedElement == null)
                {
                    currentlyRevealedElement = memoElement;
                }
                else if (currentlyRevealedElement.ImageId == memoElement.ImageId)
                {
                    currentlyRevealedElement.IsGuessed = true;
                    memoElement.IsGuessed = true;
                    currentlyRevealedElement = null;
                    OnElementsGuessed?.Invoke();
                }
                else
                {
                    StartCoroutine(currentlyRevealedElement.CountdownAndHideCoroutine());
                    StartCoroutine(memoElement.CountdownAndHideCoroutine());
                }
            };

            memoElement.OnMemoElementHidden += () =>
            {
                currentlyRevealedElement = null;
            };
        }

        RandomizeMemoElementSprites();
    }

    private void RandomizeMemoElementSprites()
    {
        int numberOfElementsToTake = xSize * ySize / 2;

        if (imagesSource.Images.Count < numberOfElementsToTake)
        {
            throw new Exception($"Not enough images configured " +
                                $"({imagesSource.Images.Count} but {numberOfElementsToTake} are needed)");
        }

        var rnd = new Random();
        var chosenImages = new List<Sprite>(imagesSource.Images)
            .OrderBy(img => rnd.Next())
            .Take(numberOfElementsToTake)
            .ToList();

        var memoElementsTemp = new List<MemoElement>(memoElements)
            .OrderBy(elem => rnd.Next())
            .ToList();

        for (int i = 0; i < numberOfElementsToTake; i++)
        {
            memoElementsTemp[2 * i].ContentImage.sprite = chosenImages[i];
            memoElementsTemp[2 * i + 1].ContentImage.sprite = chosenImages[i];
            memoElementsTemp[2 * i].ImageId = i;
            memoElementsTemp[2 * i + 1].ImageId = i;
        }
    }
}
