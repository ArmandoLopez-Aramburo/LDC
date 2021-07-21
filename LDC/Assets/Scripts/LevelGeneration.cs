using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] public GameObject Player;

    public GameObject Dungeon;

    [Header("Dungeon Size")]
    public float MediumRoom;
    public float SmallRoom;

    public bool M_HiddenRooms = false;

    private bool StopGenerating;

    List<GameObject> SpawnList = new List<GameObject>();
}
