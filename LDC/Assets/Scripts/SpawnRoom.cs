﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public int OpeningDirection;
    public string roomType;
    // 0 = Need Bottom Door
    // 1 = Need Left Door
    // 2 = Need Top Door
    // 3 = Need Right Door
        
    private DungeonPrefabs templates;

    private LevelGeneration stats;

    private GameObject temp;

    private int rand;
    public bool Spawned = false;
    public bool OpenHallway = true;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<DungeonPrefabs>();
        stats = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneration>();
        Invoke("Spawn", 2f);
    }

    private void Spawn()
    {
        if(stats.MediumRoom == 0)
        {
            stats.M_HiddenRooms = true;
        }
        if(!Spawned)
        {
            if (stats.MediumRoom >= 0 && !stats.M_HiddenRooms)
            {
                Debug.Log("Regular Room");
                if (roomType == "hallway")
                {
                    if (OpeningDirection == 0)
                    {
                        rand = Random.Range(0, templates.Rooms.Length);
                        RoomSpawner(0, rand, false);
                    }
                    if (OpeningDirection == 1)
                    {
                        rand = Random.Range(0, templates.Rooms.Length);
                        RoomSpawner(1, rand, false);
                    }
                    if (OpeningDirection == 2)
                    {
                        rand = Random.Range(0, templates.Rooms.Length);
                        RoomSpawner(2, rand, false);
                    }
                    if (OpeningDirection == 3)
                    {
                        rand = Random.Range(0, templates.Rooms.Length);
                        RoomSpawner(3, rand, false);
                    }
                }
                Spawned = true;
                OpenHallway = false;
            }
            
            if(stats.M_HiddenRooms)
            {
                Debug.Log("Hidden Room");
                if (roomType == "hallway")
                {
                    if (OpeningDirection == 0)
                    {
                        rand = Random.Range(0, templates.Rooms.Length);
                        RoomSpawner(0, rand, true);
                    }
                    if (OpeningDirection == 1)
                    {
                        rand = Random.Range(0, templates.Rooms.Length);
                        RoomSpawner(1, rand, true);
                    }
                    if (OpeningDirection == 2)
                    {
                        rand = Random.Range(0, templates.Rooms.Length);
                        RoomSpawner(2, rand, true);
                    }
                    if (OpeningDirection == 3)
                    {
                        rand = Random.Range(0, templates.Rooms.Length);
                        RoomSpawner(3, rand, true);
                    }
                }
                Spawned = true;
                OpenHallway = false;
            }

            if (roomType == "room")
            {

                if (OpeningDirection == 0)
                {
                    rand = Random.Range(0, templates.TD_Hallways.Length);
                    RoomSpawner(0, rand, false);
                }
                if (OpeningDirection == 1)
                {
                    rand = Random.Range(0, templates.LR_Hallways.Length);
                    Debug.Log(rand);
                    RoomSpawner(1, rand, false);
                }
                if (OpeningDirection == 2)
                {
                    rand = Random.Range(0, templates.TD_Hallways.Length);
                    RoomSpawner(2, rand, false);
                }
                if (OpeningDirection == 3)
                {
                    rand = Random.Range(0, templates.LR_Hallways.Length);
                    RoomSpawner(3, rand, false);
                }
            }
            Spawned = true;
        }
    }

    // Spawns the Room
    void RoomSpawner(int direction, int rand, bool SecretRoom)
    {
        // If room Type is "room" and not a Secret Room spawn a Hallway
        if (roomType == "room" && !SecretRoom)
        {
            if (direction == 0) Instantiate(templates.TD_Hallways[rand], this.transform.position + GetLocation(templates.TD_Hallways[rand], 0), Quaternion.identity);
            if (direction == 1) Instantiate(templates.LR_Hallways[rand], this.transform.position + GetLocation(templates.LR_Hallways[rand], 1), Quaternion.identity);
            if (direction == 2) Instantiate(templates.TD_Hallways[rand], this.transform.position + GetLocation(templates.TD_Hallways[rand], 2), Quaternion.identity);
            if (direction == 3) Instantiate(templates.LR_Hallways[rand], this.transform.position + GetLocation(templates.LR_Hallways[rand], 3), Quaternion.identity);
        }

        // If room Type is "hallway" spawn a room and if that room is not a Secret Room then create other spawn locations.
        else if (roomType == "hallway")
        {
            if (direction == 0)
            {
                temp = Instantiate(templates.Rooms[rand], this.transform.position + GetLocation(templates.Rooms[rand], 0), Quaternion.identity);
                if(!SecretRoom) ChooseHallways(2, temp);
            }
            if (direction == 1)
            {
                temp = Instantiate(templates.Rooms[rand], this.transform.position + GetLocation(templates.Rooms[rand], 1), Quaternion.identity);
                if (!SecretRoom) ChooseHallways(3, temp);
            }
            if (direction == 2)
            {
                temp = Instantiate(templates.Rooms[rand], this.transform.position + GetLocation(templates.Rooms[rand], 2), Quaternion.identity);
                if (!SecretRoom) ChooseHallways(0, temp);
            }
            if (direction == 3)
            {
                temp = Instantiate(templates.Rooms[rand], this.transform.position + GetLocation(templates.Rooms[rand], 3), Quaternion.identity);
                if (!SecretRoom) ChooseHallways(1, temp);
            }
            if(!SecretRoom) stats.MediumRoom--;
        }
    }

    // Randomly picks which room side to generate a hallway
    private void ChooseHallways(int direction, GameObject temp)
    {
        int x;
        for(int i = 0; i < 2; i++)
        {
            x = Random.Range(0, 4);
            Debug.Log(x);
            temp.transform.GetChild(2).GetChild(x).gameObject.SetActive(true);
        }
        temp.transform.GetChild(2).GetChild(direction).gameObject.SetActive(true);
    }

    // Get's the position of the spawner.
    private Vector3 GetLocation(GameObject temp, int direction)
    {
        // 0 Top
        // 1 Right
        // 2 Bottom
        // 3 Left
        Vector3 spawnLocation = new Vector3(0,0,0);
        if(direction == 0)
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

    // Destroys any other Spawn Location if it collides with another spawn location.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint") && collision.GetComponent<SpawnRoom>().Spawned == true)
        {
            Destroy(this.gameObject);
        }
        if (collision.CompareTag("SpawnPoint") && this.Spawned == true)
        {
            Destroy(collision.gameObject);
        }
    }

    // Need a Room Check to see if there's a room there already, might need to make the size bigger and check it before placing down a room.
}
