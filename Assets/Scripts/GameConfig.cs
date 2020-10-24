using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public Level[] Levels;
}

[Serializable]
public class Level
{
    public Level(Sprite[] sprites, GameObject prefab)
    {
        Images = sprites;
        ClickZonesPrefab = prefab;
    }
    public Sprite[] Images;
    public GameObject ClickZonesPrefab;
}

