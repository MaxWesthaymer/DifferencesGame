using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    #region InspectorFields
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private float offsetBetweenImages;
    [SerializeField] private float timeToNextLevel;
    #endregion
    
    #region Events
    public event Action onDataChange;
    #endregion
    
    #region Propierties
    public int DoneScore { get; private set; }
    public int WrongScore { get; private set; }
    public int CurrentLevel { get; private set; }
    #endregion
    
    #region PrivateFields
    private const int IMAGES_COUNT = 2;
    private GameObject[] imagesObjects;
    private float imagesHight;
    private float imagesWidth;
    private int objectsCount;
    private enum ImagePlace { TOP = 0, BOTTOM = 1 }
    private List<List<ClickableObject>> clicableObjects = new List<List<ClickableObject>>();
    #endregion

    #region UnityMethods
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        CreateImages();
        FillLevel();
        SetCamera();
        onDataChange?.Invoke();
    }
    #endregion
    

    #region PublicMethods
    public void ClickOnCorrectObject(int index)
    {
        clicableObjects[(int)ImagePlace.TOP][index].RunAnimation();
        clicableObjects[(int)ImagePlace.BOTTOM][index].RunAnimation();
        AddScore();
    }
    
    public void ClickOnWrongObject()
    {
        WrongScore++;
        onDataChange?.Invoke();
    }
    #endregion

    #region PrivateMethods
    private void AddScore()
    {
        DoneScore++;
        onDataChange?.Invoke();

        if (DoneScore == objectsCount)
        {
            Invoke(nameof(ChangeLevel), timeToNextLevel);
        }
    }

    private void ChangeLevel()
    {
        DoneScore = 0;
        WrongScore = 0;

        if (CurrentLevel < gameConfig.Levels.Length - 1)
        {
            CurrentLevel++;
        }
        else
        {
            CurrentLevel = 0;
        }
        FillLevel();
        SetCamera();
        onDataChange?.Invoke();
    }

    private void SetupImage(SpriteRenderer spriteRenderer, Sprite image, ImagePlace place)
    {
        spriteRenderer.sprite = image;
        var yPosition = spriteRenderer.size.y / 2 + offsetBetweenImages / 2;
        yPosition = place == ImagePlace.BOTTOM ? yPosition * -1 : yPosition;
        imagesHight += yPosition;
        imagesObjects[(int)place].transform.position = new Vector3(0, yPosition, 0);
        imagesWidth = spriteRenderer.size.x > imagesWidth ? spriteRenderer.size.x : imagesWidth;
    }

    private void CreateImages()
    {
        imagesObjects = new GameObject[IMAGES_COUNT];
        for (int i = 0; i < IMAGES_COUNT; i++)
        {
            imagesObjects[i] = new GameObject($"Image_{i}", typeof(SpriteRenderer));
        }
    }

    private void SetCamera()
    {
        Camera cam = Camera.main;
        if (Screen.height < Screen.width)
        {
            cam.orthographicSize = imagesHight / 2;
        }
        else
        {
            cam.orthographicSize = imagesWidth  * Screen.height / Screen.width * 0.5f;
        }
    }

    private void FillLevel()
    {
        var level = gameConfig.Levels[CurrentLevel];
        clicableObjects.Clear();
        for (int i = 0; i < IMAGES_COUNT; i++)
        {
            SetupImage(imagesObjects[i].GetComponent<SpriteRenderer>(), level.Images[i], (ImagePlace)i);
            
            imagesObjects[i].transform.Clear();

            var zones = Instantiate(level.ClickZonesPrefab, imagesObjects[i].transform.position, Quaternion.identity, imagesObjects[i].transform);
            clicableObjects.Add(new List<ClickableObject>());

            foreach (Transform it in zones.transform)
            {
                if (it.GetComponent<ClickableObject>() != null)
                {
                    clicableObjects[i].Add(it.GetComponent<ClickableObject>());
                    it.GetComponent<ClickableObject>()?.SetIndex(clicableObjects[i].Count - 1);
                }
            }
            objectsCount = clicableObjects[i].Count;
        }
    }
    #endregion
}
