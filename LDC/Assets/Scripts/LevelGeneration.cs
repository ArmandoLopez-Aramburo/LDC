using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [Header("Starting Positions")]
    public Transform[] startingPositions;

    [Header("Room Prefabs")]
    public GameObject[] TopRooms;
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;

    [SerializeField] GameObject Player;

    private int direction;
    private int roomVariant;
    public float moveAmount;

    private float TimeBtwnRoom;
    public float startTimeBtwnRoom = 0.25f;

    public GameObject Dungeon;

    [Header("Dungeon Area")]
    public float minX;
    public float maxX;
    public float minY;

    private bool StopGenerating;

    private void Awake()
    {
        if(GameData.currentDungeon != null)
        {
            GameObject temp = GameObject.Find("Dungeon");
            temp = GameData.currentDungeon;
        }
    }
    // Update is called to spawn the rooms on a delay.
    private void Update()
    {
        if(GameData.GenerateDungeon == true && GameData.doOnce)
        {
            int randStartingPos = Random.Range(0, startingPositions.Length);
            transform.position = startingPositions[randStartingPos].position;
            GameObject room = Instantiate(TopRooms[0], transform.position, Quaternion.identity);
            room.transform.SetParent(Dungeon.transform);
            if (Player != null) Player.transform.position = startingPositions[randStartingPos].position;

            direction = Random.Range(1, 6);

            GameData.doOnce = false;
        }
        if(TimeBtwnRoom <= 0 && !StopGenerating)
        {
            if(GameData.GenerateDungeon) Move();
            TimeBtwnRoom = startTimeBtwnRoom;
        }
        else
        {
            TimeBtwnRoom -= Time.deltaTime;
        }
    }

    // Move Function to spawn the Procedurally Generated Dungeon.
    private void Move()
    {
        if (direction == 1 || direction == 2) // Move Right
        {
            if (transform.position.x < maxX)
            {
                print("Moving Right");
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = PlaceRoom(newPos, "right");

                direction = Random.Range(1, 6);
                if (direction == 3 || direction == 4)
                {
                    if(transform.position.x < maxX) direction = 1;
                    else direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 3 || direction == 4) // Move Left
        {
            Debug.Log("Move Left");
            if(transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = PlaceRoom(newPos, "left");

                direction = Random.Range(1, 6);
                if (direction == 1 || direction == 2)
                {
                    if (transform.position.x > minX) direction = 3;
                    else direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        if (direction == 5) // Move Down
        {
            if(transform.position.y > minY)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = PlaceRoom(newPos, "top");

                direction = Random.Range(1, 6);
            }
            else
            {
                Debug.Log("Stop Generating!!!");
                //Stop Level Generation
                StopGenerating = true;
                GameData.GenerateDungeon = false;
            }
        }
        print("Direction:" + direction);
    }

    // Place Room function that instantiates the correct room type.
    private Vector2 PlaceRoom(Vector2 temp, string roomType)
    {
        roomVariant = Random.Range(0, RightRooms.Length);
        Debug.Log(roomVariant);
        if(roomType == "right") (Instantiate(RightRooms[roomVariant], temp, Quaternion.identity)).transform.SetParent(Dungeon.transform);
        if (roomType == "left") (Instantiate(LeftRooms[roomVariant], temp, Quaternion.identity)).transform.SetParent(Dungeon.transform);
        if (roomType == "top") (Instantiate(TopRooms[roomVariant], temp, Quaternion.identity)).transform.SetParent(Dungeon.transform);
        return temp;
    }
}
