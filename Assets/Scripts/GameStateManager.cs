using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

/// <summary>
/// This component controls game state
/// It is also responsible for game initialization
/// </summary>
public class GameStateManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject memoObjectPrefab;

    [SerializeField] 
    private GridLayoutGroup gridLayout;

    [SerializeField]
    private MemoImagesSource imagesSource;

    // Private game state
    private List<MemoElement> memoElements;
    private MemoElement currentlyRevealedElement;
    private int xSize;
    private int ySize;

    // Public callbacks
    public Action OnGameReset = () => { };
    public Action OnElementsGuessed = () => { };
    public Action OnElementGuessedWrong = () => { };
    public Action OnGameFinished = () => { };

    private void Awake()
    {
        memoElements = new List<MemoElement>();
    }

    public void ResetGameState()
    {
        // Destroy all instantiated prefabs
        foreach (var elem in memoElements)
        {
            Destroy(elem.gameObject);
        }

        memoElements.Clear();
        xSize = 0;
        ySize = 0;

        OnGameReset?.Invoke();
    }

    public void InitializeGameState(int xSize, int ySize)
    {
        int numberOfElements = xSize * ySize;

        // Theoretically this method allows to create boards larger than 4x4
        // This exception will be thrown if number of elements is odd (cannot create pairs)
        if (xSize % 2 != 0 && ySize % 2 != 0)
        {
            throw new Exception($"Number of elements has to be even (current: {numberOfElements})");
        }

        ResetGameState();

        this.xSize = xSize;
        this.ySize = ySize;

        // This will set dimensions of grid layout
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
                    if (memoElements.All(element => element.IsGuessed))
                    {
                        OnGameFinished?.Invoke();
                    }
                }
                else
                {
                    StartCoroutine(currentlyRevealedElement.CountdownAndFadeInCoroutine());
                    StartCoroutine(memoElement.CountdownAndFadeInCoroutine());
                    OnElementGuessedWrong?.Invoke();
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
        // If game size is 16, we need only 8 sprites
        int numberOfElementsToTake = xSize * ySize / 2;

        // It is possible to initialize boards of very large sizes (InitializeGameState method)
        // There can be not enough Sprites configured to provide graphics for all elements
        if (imagesSource.Images.Count < numberOfElementsToTake)
        {
            throw new Exception($"Not enough images configured " +
                                $"({imagesSource.Images.Count} but {numberOfElementsToTake} are needed)");
        }

        var rnd = new Random();
        
        // Copy source sprites list, randomize their order and take amount needed
        var chosenImages = new List<Sprite>(imagesSource.Images)
            .OrderBy(_ => rnd.Next())
            .Take(numberOfElementsToTake)
            .ToList();

        // Copy all instantiated memo elements and arrange them randomly
        var memoElementsTemp = new List<MemoElement>(memoElements)
            .OrderBy(_ => rnd.Next())
            .ToList();

        // Since both sprite and memo element lists have been randomized, I can just assign sprite for next two memo elements 
        for (int i = 0; i < numberOfElementsToTake; i++)
        {
            memoElementsTemp[2 * i].ContentImage.sprite = chosenImages[i];
            memoElementsTemp[2 * i + 1].ContentImage.sprite = chosenImages[i];
            memoElementsTemp[2 * i].ImageId = i;
            memoElementsTemp[2 * i + 1].ImageId = i;
        }
    }

    #region Wrappers

    public GameObject MemoObjectPrefab
    {
        get => memoObjectPrefab;
        set => memoObjectPrefab = value;
    }

    public GridLayoutGroup GridLayout
    {
        get => gridLayout;
        set => gridLayout = value;
    }

    public MemoImagesSource ImagesSource
    {
        get => imagesSource;
        set => imagesSource = value;
    }

    #endregion
}
