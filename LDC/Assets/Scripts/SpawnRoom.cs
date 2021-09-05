using System.Collections;
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
    private DoorWay door;
    private LevelGeneration stats;
    private GameObject temp;

    private int rand;
    public bool Spawned = false;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Templates").GetComponent<DungeonPrefabs>();
        stats = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneration>();
        door = this.transform.GetChild(0).GetComponentInChildren<DoorWay>();
        Invoke("Spawn", 0.1f);
    }

    // Function Spawner
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
            }
            
            if(stats.M_HiddenRooms)
            {
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
        // Needs to check if there's a pre-existing room in that location and if so don't generate room.
        if (roomType == "room" && !SecretRoom)
        {
            if (direction == 0)
            {
                temp = Instantiate(templates.TD_Hallways[rand], this.transform.position + GetLocation(templates.TD_Hallways[rand], 0), Quaternion.identity, stats.Dungeon.transform);
                Label(SecretRoom, temp, 2, roomType);
                //door.gameObject.SetActive(true);
            }
            if (direction == 1)
            {
                temp = Instantiate(templates.LR_Hallways[rand], this.transform.position + GetLocation(templates.LR_Hallways[rand], 1), Quaternion.identity, stats.Dungeon.transform);
                Label(SecretRoom, temp, 3, roomType);
                //door.gameObject.SetActive(true);
            }
            if (direction == 2)
            {
                temp = Instantiate(templates.TD_Hallways[rand], this.transform.position + GetLocation(templates.TD_Hallways[rand], 2), Quaternion.identity, stats.Dungeon.transform);
                Label(SecretRoom, temp, 0, roomType);
                //door.gameObject.SetActive(true);
            }
            if (direction == 3)
            {
                temp = Instantiate(templates.LR_Hallways[rand], this.transform.position + GetLocation(templates.LR_Hallways[rand], 3), Quaternion.identity, stats.Dungeon.transform);
                Label(SecretRoom, temp, 1, roomType);
                //door.gameObject.SetActive(true);
            }
        }

        // If room Type is "hallway" spawn a room and if that room is not a Secret Room then create other spawn locations.
        // Needs to check if there's a pre-existing room in that location and if so don't generate room.
        else if (roomType == "hallway")
        {
            if (direction == 0)
            {
                temp = Instantiate(templates.Rooms[rand], this.transform.position + GetLocation(templates.Rooms[rand], 0), Quaternion.identity, stats.Dungeon.transform);
                Label(SecretRoom, temp, 2, roomType);
            }
            if (direction == 1)
            {
                temp = Instantiate(templates.Rooms[rand], this.transform.position + GetLocation(templates.Rooms[rand], 1), Quaternion.identity, stats.Dungeon.transform);
                Label(SecretRoom, temp, 3, roomType);
            }
            if (direction == 2)
            {
                temp = Instantiate(templates.Rooms[rand], this.transform.position + GetLocation(templates.Rooms[rand], 2), Quaternion.identity, stats.Dungeon.transform);
                Label(SecretRoom, temp, 0, roomType);
            }
            if (direction == 3)
            {
                temp = Instantiate(templates.Rooms[rand], this.transform.position + GetLocation(templates.Rooms[rand], 3), Quaternion.identity, stats.Dungeon.transform);
                Label(SecretRoom, temp, 1, roomType);
            }
            if(!SecretRoom) stats.MediumRoom--;
        }
    }

    // Function that labels the room appropriately depending on if it's a Hidden, Exit, Random Room.
    private void Label(bool SecretRoom,GameObject temp, int direction, string roomType)
    {
        if(roomType == "room")
        {
            temp.name = "Hallway";
        }
        else
        {
            if (roomType == "hallway") temp.name = "Basic Room";
            if (stats.MediumRoom == 1)
            {
                temp.name = "Exit Room";
                stats.ExitRoomObjects(temp);
            }
            if (SecretRoom) temp.name = "Hidden Room";
            else ChooseHallways(direction, temp);
        }
        door.gameObject.SetActive(true);
    }

    // Randomly picks a side to generate a hallway
    private void ChooseHallways(int direction, GameObject temp)
    {
        int x;
        for(int i = 0; i < 2; i++)
        {
            x = Random.Range(0, 4);
            if(x != direction)
            {
                temp.transform.GetChild(2).GetChild(x).gameObject.SetActive(true);
            }
        }
    }

    // Get's the position of the spawner.
    private Vector3 GetLocation(GameObject temp, int direction)
    {
        // 0:Top, 1:Right, 2:Bottom, 3:Left
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
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        // If statement so it won't spawn a room that's already been spawned.
        if (collision.CompareTag("SpawnPoint") && this.Spawned == true)
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
       
    private void RoomHasSpawned(GameObject temp)
    {
        Debug.Log(temp.name);
        temp.GetComponent<RoomInfo>().RoomExists = true;
    } 
}