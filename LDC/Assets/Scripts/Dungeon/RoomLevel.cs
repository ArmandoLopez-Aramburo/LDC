using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLevel : MonoBehaviour
{
    [SerializeField] public List<GameObject> AvailableHallways = new List<GameObject>();
    private int rand;

    private DungeonPrefabs templates;
    private GameObject door;
    private LevelGeneration stats;
    private GameObject newHallway;

    public bool Spawned = false;

    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<DungeonPrefabs>();
        stats = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneration>();
    }

    // Needs to take into account the opening that it came from.
    public void SelectEntries(GameObject currentRoom)
    {
        int x;
        if(!stats.M_HiddenRooms)
        {
            for (int i = 0; i < 3; i++)
            {
                x = Random.Range(0, 4);
                if (stats.Direction[0] == 5)
                {
                    currentRoom.transform.GetChild(2).GetChild(x).gameObject.SetActive(true);
                    CheckListForDuplicates(AvailableHallways, currentRoom.transform.GetChild(2).GetChild(x).gameObject);
                }
                else
                {
                    if (x != stats.Direction[0])
                    {
                        currentRoom.transform.GetChild(2).GetChild(x).gameObject.SetActive(true);
                        CheckListForDuplicates(AvailableHallways, currentRoom.transform.GetChild(2).GetChild(x).gameObject);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 1; i++)
            {
                x = Random.Range(0, 4);
                if (x != stats.Direction[0])
                {
                    currentRoom.transform.GetChild(2).GetChild(x).gameObject.SetActive(true);
                    CheckListForDuplicates(AvailableHallways, currentRoom.transform.GetChild(2).GetChild(x).gameObject);
                }
            }
        }
        CurrentHallway();
    }

    // Function that's called to spawn hallways and talks with the LevelGeneration script to continue to the next room generation.
    public void CurrentHallway()
    {
        //rand = 0;
        if(AvailableHallways.Count > 0)
        {
            SpawnHallway(AvailableHallways[0], rand, false);
        }

        if (stats.RoomList.Count >= 1 && AvailableHallways.Count == 0)
        {
            //Debug.Log("SPAWNING NEXT ROOM");
            stats.RoomList.RemoveAt(0);
            stats.Direction.RemoveAt(0);
            if (stats.RoomList.Count == 0)
            {
                Debug.Log("SPAWNLIST COUNT: " + stats.RoomList.Count);
                Debug.Log("Finished Generating Dungeon.");
                stats.ClearRooms();
            }
            stats.Generate();
        }
    }

    // Function that spawns all the "open" hallways
    public void SpawnHallway(GameObject SpawnPoint, int rand, bool SecretRoom)
    {
        // If not a Secret Room spawn a Hallway
        if (!SecretRoom)
        {
            door = SpawnPoint.transform.GetChild(0).gameObject;
            if (SpawnPoint.name == "Top")
            {
                newHallway = Instantiate(templates.TD_Hallways[rand], SpawnPoint.transform.position + GetLocation(templates.TD_Hallways[rand], 0), Quaternion.identity, stats.Dungeon.transform);
                stats.Label(false, newHallway, 0, door, "room", this.gameObject);
                newHallway.GetComponent<HallwayLevel>().SelectRoom(newHallway, 0, this.gameObject);
            }
            if (SpawnPoint.name == "Right")
            {
                newHallway = Instantiate(templates.LR_Hallways[rand], SpawnPoint.transform.position + GetLocation(templates.LR_Hallways[rand], 1), Quaternion.identity, stats.Dungeon.transform);
                stats.Label(false, newHallway, 1, door, "room", this.gameObject);
                newHallway.GetComponent<HallwayLevel>().SelectRoom(newHallway, 1, this.gameObject);
            }
            if (SpawnPoint.name == "Bottom")
            {
                newHallway = Instantiate(templates.TD_Hallways[rand], SpawnPoint.transform.position + GetLocation(templates.TD_Hallways[rand], 2), Quaternion.identity, stats.Dungeon.transform);
                stats.Label(false, newHallway, 2, door, "room", this.gameObject);
                newHallway.GetComponent<HallwayLevel>().SelectRoom(newHallway, 2, this.gameObject);
            }
            if (SpawnPoint.name == "Left")
            {
                newHallway = Instantiate(templates.LR_Hallways[rand], SpawnPoint.transform.position + GetLocation(templates.LR_Hallways[rand], 3), Quaternion.identity, stats.Dungeon.transform);
                stats.Label(false, newHallway, 3, door, "room", this.gameObject);
                newHallway.GetComponent<HallwayLevel>().SelectRoom(newHallway, 3, this.gameObject);
            }
        }
    }

    // Function that get's the location of the spawner to be used to place the hallway in the correct spot
    private Vector3 GetLocation(GameObject temp, int direction)
    {
        // 0:Top, 1:Right, 2:Bottom, 3:Left
        Vector3 spawnLocation = new Vector3(0, 0, 0);
        if (direction == 0)
        {
            spawnLocation = -temp.transform.GetChild(2).GetChild(2).gameObject.transform.position;
        }
        if (direction == 1)
        {
            spawnLocation = -temp.transform.GetChild(2).GetChild(3).gameObject.transform.position;
        }
        if (direction == 2)
        {
            spawnLocation = -temp.transform.GetChild(2).GetChild(0).gameObject.transform.position;
        }
        if (direction == 3)
        {
            spawnLocation = -temp.transform.GetChild(2).GetChild(1).gameObject.transform.position;
        }
        return spawnLocation;
    }

    // Function used to check if a gameobject already exists in the list.
    private void CheckListForDuplicates(List<GameObject> list, GameObject temp)
    {
        if(!list.Contains(temp))
        {
            AvailableHallways.Add(temp);
        }
    }

    // Function to print out the hallway list
    private void PrintList()
    {
        foreach (var x in AvailableHallways)
        {
            Debug.Log(x.name);
        }
    }
}
