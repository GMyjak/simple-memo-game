using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private List<MemoElement> memoElements;
    private int score = 0;
    private float playTime = 0;

    private void Awake()
    {
        memoElements = new List<MemoElement>();
    }

    public void ResetGameState()
    {
        foreach (var elem in memoElements)
        {
            Destroy(elem);
        }

        memoElements.Clear();
        score = 0;
        playTime = 0;
    }

    public void InitializeGameState(int xSize, int ySize)
    {
        int numberOfElements = xSize * ySize;

        if (xSize % 2 != 0 && ySize % 2 != 0)
        {
            throw new Exception($"Number of elements has to be even (current: {numberOfElements})");
        }

        ResetGameState();

        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = xSize;

        for (int i = 0; i < numberOfElements; i++)
        {
            var newObject = Instantiate(MemoObjectPrefab, GridLayout.transform);
            memoElements.Add(newObject.GetComponent<MemoElement>());
        }
    }
}
