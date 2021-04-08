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

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

}