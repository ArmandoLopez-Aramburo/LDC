using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static Vector3 playerPos;

    public static float playerMaxHP = 100;
    public static float playerCurrentHP = 100;
    public static float playerCurrentXP = 0;

    public static bool outOfCombat = false;
    public static bool GenerateDungeon = true;
    public static bool doOnce = true;

    public static GameObject currentDungeon;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
}