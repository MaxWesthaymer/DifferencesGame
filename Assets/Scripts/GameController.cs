using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private float offsetBetweenInages;

    public event Action onDataChange;
    public int DoneScore { get; private set; }
    public int WrongScore { get; private set; }
    public int CurrentLevel { get; private set; }
    public static GameController Instance;
    private const int IMAGES_COUNT = 2;
    private GameObject[] imagesObjects;
    private float imagesHight;
    private float imagesWidth;
    private enum ImagePlace
    {
        TOP = 0,
        BOTTOM = 1
    }

    public List<List<GameObject>> clicableObjects = new List<List<GameObject>>();

    void Awake()
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
    void Start()
    {
        
        FillLevel();
        onDataChange?.Invoke();
    }

    public void ClinckOnCurrectObject(int index)
    {
        clicableObjects[0][index].SetActive(false);
        clicableObjects[1][index].SetActive(false);
        DoneScore++;
        onDataChange?.Invoke();
    }

    private void SetupImage(SpriteRenderer spriteRenderer, Sprite image, ImagePlace place)
    {
        spriteRenderer.sprite = image;
        var yPosition = spriteRenderer.size.y / 2 + offsetBetweenInages / 2;
        yPosition = place == ImagePlace.BOTTOM ? yPosition * -1 : yPosition;
        imagesHight += yPosition;
        imagesObjects[(int)place].transform.position = new Vector3(0, yPosition, 0);
        imagesWidth = spriteRenderer.size.x > imagesWidth ? spriteRenderer.size.x : imagesWidth;
    }


    private void FillLevel()
    {
        var level = gameConfig.Levels[CurrentLevel];
        imagesObjects = new GameObject[IMAGES_COUNT];
        for (int i = 0; i < IMAGES_COUNT; i++)
        {
            imagesObjects[i] = new GameObject($"Image_{i}", typeof(SpriteRenderer));
        }
        
        for (int i = 0; i < IMAGES_COUNT; i++)
        {
            SetupImage(imagesObjects[i].GetComponent<SpriteRenderer>(), level.Images[i], (ImagePlace)i);
        }
        
        for (int i = 0; i < IMAGES_COUNT; i++)
        {
            var zones = Instantiate(level.ClickZonesPrefab, imagesObjects[i].transform.position, Quaternion.identity, imagesObjects[i].transform);
            clicableObjects.Add(new List<GameObject>());
            foreach (Transform it in zones.transform)
            {
                clicableObjects[i].Add(it.gameObject);
                it.gameObject.AddComponent<ClickableObject>();
                it.GetComponent<ClickableObject>().Setup(clicableObjects[i].Count - 1);
            }
        }
        
        
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
}
