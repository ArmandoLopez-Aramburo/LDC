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

    [Header("Starting Room")]
    public GameObject startRoom;
    private GameObject room;
    [Space]

    public bool M_HiddenRooms = false;
    private bool startingRoomSpawned = false;

    public void Start()
    {
        // Spawns the first room if it hasn't already.
        if(!startingRoomSpawned)
        {
            room = Instantiate(startRoom, this.gameObject.transform.position, Quaternion.identity, Dungeon.gameObject.transform);
            room.name = "Starting Room";
        }
        ChooseHallways(room);
        startingRoomSpawned = true;
    }

    // Function that chooses which hallway is "open" to be used by the dungeon generation.
    private void ChooseHallways(GameObject temp)
    {
        int x;
        for (int i = 0; i < 2; i++)
        {
            x = Random.Range(0, 4);
            temp.transform.GetChild(2).GetChild(x).gameObject.SetActive(true);
        }
    }

    private bool StopGenerating;

    List<GameObject> SpawnList = new List<GameObject>();
}
