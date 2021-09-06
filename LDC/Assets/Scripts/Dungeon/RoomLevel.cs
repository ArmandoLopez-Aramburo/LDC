using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLevel : MonoBehaviour
{
    [SerializeField] public List<GameObject> HallwaysAvailable = new List<GameObject>();
    private int rand;

    private DungeonPrefabs templates;
    private GameObject door;
    private LevelGeneration stats;
    private GameObject temp;

    public bool Spawned = false;

    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<DungeonPrefabs>();
        stats = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneration>();
        //door = this.transform.GetChild(0).GetComponentInChildren<DoorWay>();
    }

    // Function that chooses which hallway is "open" to be used by the dungeon generation.
    // Could be later on used for Room's that already have a set opening.
    public void ChooseHallways(GameObject temp)
    {
        int x;
        for (int i = 0; i < 3; i++)
        {
            x = Random.Range(0, 4);
            temp.transform.GetChild(2).GetChild(x).gameObject.SetActive(true);

            CheckListForDuplicates(HallwaysAvailable, temp.transform.GetChild(2).GetChild(x).gameObject);
        }

        SpawnHallways();
    }

    private void SpawnHallways()
    {
        //PrintList();
        while (HallwaysAvailable.Count > 0)
        {
            rand = 0;
            RoomSpawner(HallwaysAvailable[0], rand, false);
            HallwaysAvailable.RemoveAt(0);

        }
        stats.SpawnList.RemoveAt(0);
    }

    void RoomSpawner(GameObject SpawnPoint, int rand, bool SecretRoom)
    {
        
        // If room Type is "room" and not a Secret Room spawn a Hallway
        // Needs to check if there's a pre-existing room in that location and if so don't generate room.
        // TEMP IS THE HALLWAY
        if (!SecretRoom)
        {
            door = SpawnPoint.transform.GetChild(0).gameObject;
            if (SpawnPoint.name == "Top")
            {
                temp = Instantiate(templates.TD_Hallways[rand], SpawnPoint.transform.position + GetLocation(templates.TD_Hallways[rand], 0), Quaternion.identity, stats.Dungeon.transform);
                stats.Label(false, temp, 0, door, "room");
                temp.GetComponent<HallwayLevel>().ChooseRoom(temp, 0);
            }
            if (SpawnPoint.name == "Right")
            {
                temp = Instantiate(templates.LR_Hallways[rand], SpawnPoint.transform.position + GetLocation(templates.LR_Hallways[rand], 1), Quaternion.identity, stats.Dungeon.transform);
                stats.Label(false, temp, 1, door, "room");
                temp.GetComponent<HallwayLevel>().ChooseRoom(temp, 1);
            }
            if (SpawnPoint.name == "Bottom")
            {
                temp = Instantiate(templates.TD_Hallways[rand], SpawnPoint.transform.position + GetLocation(templates.TD_Hallways[rand], 2), Quaternion.identity, stats.Dungeon.transform);
                stats.Label(false, temp, 2, door, "room");
                temp.GetComponent<HallwayLevel>().ChooseRoom(temp, 2);
            }
            if (SpawnPoint.name == "Left")
            {
                temp = Instantiate(templates.LR_Hallways[rand], SpawnPoint.transform.position + GetLocation(templates.LR_Hallways[rand], 3), Quaternion.identity, stats.Dungeon.transform);
                stats.Label(false, temp, 3, door, "room");
                temp.GetComponent<HallwayLevel>().ChooseRoom(temp, 3);
            }
        }
    }

    // Get's the position of the spawner.
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

    private void CheckListForDuplicates(List<GameObject> list, GameObject temp)
    {
        if(!list.Contains(temp))
        {
            HallwaysAvailable.Add(temp);
        }
    }

    private void PrintList()
    {
        foreach (var x in HallwaysAvailable)
        {
            Debug.Log(x.name);
        }
    }
}
