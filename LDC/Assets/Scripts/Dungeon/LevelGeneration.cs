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

    public List<GameObject> RoomList = new List<GameObject>();
    public List<int> Direction = new List<int>();
    private GameObject[] AllRooms;

    public void Start()
    {
        // Spawns the first room if it hasn't been spawned in.
        if (!startingRoomSpawned)
        {
            room = Instantiate(startRoom, this.gameObject.transform.position, Quaternion.identity, Dungeon.gameObject.transform);
            room.name = "Starting Room";
            startingRoomSpawned = true;
            RoomList.Add(room);
            Direction.Add(5);
            Generate();
        }
    }

    //  Function that performs the SelectHallways function to spawn in the rooms available hallways.
    public void Generate()
    {
        //PrintList();
        if(RoomList.Count > 0)
        {
            RoomList[0].GetComponent<RoomLevel>().SelectEntries(RoomList[0]);
        }
    }

    // Function that prints the Room List
    private void PrintList()
    {
        foreach (var x in RoomList)
        {
            Debug.Log(x.name);
        }
    }

    // Function that labels the room appropriately, depending on if it's a Hidden, Exit, Basic Room
    public void Label(bool SecretRoom, GameObject temp, int direction, GameObject door, string roomType, GameObject hallway)
    {
        if (roomType == "room")
        {
            temp.name = "Hallway";
        }
        else
        {
            if (roomType == "hallway" && !SecretRoom)
            {
                temp.name = "Basic Room";
                RoomList.Add(temp);
                Direction.Add(direction);
                MediumRoom--;
            }
            if (SecretRoom)
            {
                temp.name = "Hidden Room";
            }
        }
        door.gameObject.SetActive(true);
    }

    // Function that's called to clear the room's collider and makes the second to last room the Exit Room
    public void ClearRooms()
    {
        AllRooms = GameObject.FindGameObjectsWithTag("Rooms");

        for(int i = 0; i < AllRooms.Length; i++)
        {
            if(i == AllRooms.Length-2)
            {
                AllRooms[i].transform.parent.name = "EXITTTTT ROOOOOOM";
            }
            AllRooms[i].SetActive(false);
        }
    }
}
