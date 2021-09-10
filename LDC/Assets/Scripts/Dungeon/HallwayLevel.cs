using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayLevel : MonoBehaviour
{
    private int rand;

    private DungeonPrefabs templates;
    private GameObject door;
    private LevelGeneration stats;
    private GameObject newRoom;

    public bool DestroyRoom;

    private IEnumerator routine;

    private void Awake()
    {
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<DungeonPrefabs>();
        stats = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneration>();
    }

    // Function to select a room template and if that room is not a Secret Room then create other spawn locations.
    public void SelectRoom(GameObject SpawnPoint, int direction, GameObject PreviousRoom)
    {
        if (stats.MediumRoom == 0)
        {
            stats.M_HiddenRooms = true;
        }
        if (stats.MediumRoom > 0)
        {
            if (direction == 0)
            {
                newRoom = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(0).position + GetLocation(templates.Rooms[rand], 0), Quaternion.identity, stats.Dungeon.transform);
                routine = SpawnRoom(false, newRoom, 0, 2, "hallway", PreviousRoom);
                StartCoroutine(routine);
            }
            if (direction == 1)
            {
                newRoom = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(1).position + GetLocation(templates.Rooms[rand], 1), Quaternion.identity, stats.Dungeon.transform);
                routine = SpawnRoom(false, newRoom, 1, 3, "hallway", PreviousRoom);
                StartCoroutine(routine);
            }
            if (direction == 2)
            {
                newRoom = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(2).position + GetLocation(templates.Rooms[rand], 2), Quaternion.identity, stats.Dungeon.transform);
                routine = SpawnRoom(false, newRoom, 2, 0, "hallway", PreviousRoom);
                StartCoroutine(routine);
            }
            if (direction == 3)
            {
                newRoom = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(3).position + GetLocation(templates.Rooms[rand], 3), Quaternion.identity, stats.Dungeon.transform);
                routine = SpawnRoom(false, newRoom, 3, 1, "hallway", PreviousRoom);
                StartCoroutine(routine);
            }
            DestroyRoom = false;
        }
        if(stats.M_HiddenRooms)
        {
            if (direction == 0)
            {
                newRoom = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(0).position + GetLocation(templates.Rooms[rand], 0), Quaternion.identity, stats.Dungeon.transform);
                routine = SpawnRoom(true, newRoom, 0, 2, "hallway", PreviousRoom);
                StartCoroutine(routine);
            }
            if (direction == 1)
            {
                newRoom = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(1).position + GetLocation(templates.Rooms[rand], 1), Quaternion.identity, stats.Dungeon.transform);
                routine = SpawnRoom(true, newRoom, 1, 3, "hallway", PreviousRoom);
                StartCoroutine(routine);
            }
            if (direction == 2)
            {
                newRoom = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(2).position + GetLocation(templates.Rooms[rand], 2), Quaternion.identity, stats.Dungeon.transform);
                routine = SpawnRoom(true, newRoom, 2, 0, "hallway", PreviousRoom);
                StartCoroutine(routine);
            }
            if (direction == 3)
            {
                newRoom = Instantiate(templates.Rooms[rand], this.transform.GetChild(2).GetChild(3).position + GetLocation(templates.Rooms[rand], 3), Quaternion.identity, stats.Dungeon.transform);
                routine = SpawnRoom(true, newRoom, 3, 1, "hallway", PreviousRoom);
                StartCoroutine(routine);
            }
            DestroyRoom = false;
        }
    }

    // Enumerator used to spawn in the rooms, check to see if a room is already spawned there and if so deletes it.
    IEnumerator SpawnRoom(bool isSecretRoom, GameObject newRoom, int FromDirection, int ToDirection, string Type, GameObject PreviousRoom)
    {
        newRoom.transform.GetChild(3).GetComponent<CheckArea>().IsAreaAvailable(this.gameObject);
        yield return new WaitForSeconds(0.1f);
        if (DestroyRoom)
        {
            Destroy(this.newRoom.gameObject);
            door = this.transform.GetChild(2).GetChild(FromDirection).GetChild(0).gameObject;
            door.gameObject.SetActive(true);
            if (PreviousRoom.GetComponent<RoomLevel>().AvailableHallways.Count >= 1)
            {
                PreviousRoom.GetComponent<RoomLevel>().AvailableHallways.RemoveAt(0);
                PreviousRoom.GetComponent<RoomLevel>().CurrentHallway();
            }
        }
        else
        {
            this.newRoom.GetComponent<RoomLevel>().Spawned = true;
            door = this.transform.GetChild(2).GetChild(FromDirection).GetChild(0).gameObject;
            stats.Label(isSecretRoom, this.newRoom, ToDirection, door, Type, this.gameObject);

            if (PreviousRoom.GetComponent<RoomLevel>().AvailableHallways.Count >= 1)
            {
                PreviousRoom.GetComponent<RoomLevel>().AvailableHallways.RemoveAt(0);
                PreviousRoom.GetComponent<RoomLevel>().CurrentHallway();
            }
        }
    }

    // Function that get's the location of the spawner to be used to place it in the correct spot
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
