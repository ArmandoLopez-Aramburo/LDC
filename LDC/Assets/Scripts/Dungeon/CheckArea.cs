using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    private GameObject TempParent;

    public void IsAreaAvailable(GameObject parentHallway)
    {
        TempParent = parentHallway;
        this.gameObject.SetActive(true);
    }

    // OnCollision function that checks if a room is already in that spot
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rooms"))
        {
            Debug.Log("Collided with a room");
            TempParent.GetComponent<HallwayLevel>().DestroyRoom = true;
            Debug.Log("DestroyRoom Variable: " + TempParent.GetComponent<HallwayLevel>().DestroyRoom);
        }
    }
}
