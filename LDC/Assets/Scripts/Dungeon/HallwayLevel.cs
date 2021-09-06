using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayLevel : MonoBehaviour
{
    private int rand;

    private DungeonPrefabs templates;
    private GameObject door;
    private LevelGeneration stats;
    private GameObject temp;

    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<DungeonPrefabs>();
        stats = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneration>();
    }


    // If room Type is "hallway" spawn a room and if that room is not a Secret Room then create other spawn locations.
    // Needs to check if there's a pre-existing room in that location and if so don't generate room.
    public void ChooseRoom(GameObject SpawnPoint, int direction)
    {
        // ADDED THIS.TRANSFORM.POSITION
        if (direction == 0)
        {
            // Have a separate gameobject on the Hallway prefabs to detect if it does hit a room if so return something.
            temp = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(0).position + GetLocation(templates.Rooms[rand], 0), Quaternion.identity, stats.Dungeon.transform);
            door = this.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
            stats.Label(false, temp, 0, door, "hallway");
        }
        if (direction == 1)
        {
            temp = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(1).position + GetLocation(templates.Rooms[rand], 1), Quaternion.identity, stats.Dungeon.transform);
            door = this.transform.GetChild(2).GetChild(1).GetChild(0).gameObject;
            stats.Label(false, temp, 1, door, "hallway");
        }
        if (direction == 2)
        {
            temp = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(2).position + GetLocation(templates.Rooms[rand], 2), Quaternion.identity, stats.Dungeon.transform);
            door = this.transform.GetChild(2).GetChild(2).GetChild(0).gameObject;
            stats.Label(false, temp, 2, door, "hallway");
        }
        if (direction == 3)
        {
            temp = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(3).position + GetLocation(templates.Rooms[rand], 3), Quaternion.identity, stats.Dungeon.transform);
            door = this.transform.GetChild(2).GetChild(3).GetChild(0).gameObject;
            stats.Label(false, temp, 3, door, "hallway");
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
}
