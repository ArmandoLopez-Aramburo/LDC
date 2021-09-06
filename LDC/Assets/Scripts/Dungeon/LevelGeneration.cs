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

    [SerializeField] public List<GameObject> SpawnList = new List<GameObject>();

    public void Start()
    {
        // Spawns the first room if it hasn't already.
        if (!startingRoomSpawned)
        {
            room = Instantiate(startRoom, this.gameObject.transform.position, Quaternion.identity, Dungeon.gameObject.transform);
            room.name = "Starting Room";
        }
        startingRoomSpawned = true;

        SpawnList.Add(room);
        Generate();
    }

    public void ExitRoomObjects(GameObject temp)
    {
        temp.name = "NEWWWWW EXIT";
    }

    [SerializeField] public void Generate()
    {
        //PrintList();
        //Debug.Log(SpawnList[0].name);
        while(SpawnList.Count > 0)
        {
            SpawnList[0].GetComponent<RoomLevel>().ChooseHallways(SpawnList[0]);
        }
        Debug.Log("Finished Generating");
    }

    private void PrintList()
    {
        foreach (var x in SpawnList)
        {
            Debug.Log(x.name);
        }
    }

    // Function that labels the room appropriately depending on if it's a Hidden, Exit, Random Room.
    public void Label(bool SecretRoom, GameObject temp, int direction, GameObject door, string roomType)
    {
        if (roomType == "room")
        {
            temp.name = "Hallway";
        }
        else
        {
            if (roomType == "hallway") temp.name = "Basic Room";
            if (MediumRoom == 1)
            {
                temp.name = "Exit Room";
                ExitRoomObjects(temp);
            }
            if (SecretRoom) temp.name = "Hidden Room";
            //else ChooseHallways(direction, temp);
        }
        door.gameObject.SetActive(true);
    }
}
