using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] TopRooms;
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;

    private int direction;
    private int roomVariant;
    public float moveAmount;

    private float TimeBtwnRoom;
    public float startTimeBtwnRoom = 0.25f;

    public float minX;
    public float maxX;
    public float minY;

    private bool StopGenerating;

    // Start is called before the first frame update
    void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[3].position;
        Instantiate(TopRooms[0], transform.position, Quaternion.identity);

        //direction = Random.Range(1, 6);
        direction = 2;
    }

    private void Update()
    {
        if(TimeBtwnRoom <= 0 && !StopGenerating)
        {
            Move();
            TimeBtwnRoom = startTimeBtwnRoom;
        }
        else
        {
            TimeBtwnRoom -= Time.deltaTime;
        }
    }

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
            }
        }
        print("Direction:" + direction);
    }

    private Vector2 PlaceRoom(Vector2 temp, string roomType)
    {
        roomVariant = Random.Range(0, RightRooms.Length);
        Debug.Log(roomVariant);
        if(roomType == "right") Instantiate(RightRooms[roomVariant], temp, Quaternion.identity);
        if (roomType == "left") Instantiate(LeftRooms[roomVariant], temp, Quaternion.identity);
        if (roomType == "top") Instantiate(TopRooms[roomVariant], temp, Quaternion.identity);
        return temp;
    }
}
