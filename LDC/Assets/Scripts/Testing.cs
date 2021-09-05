using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Rooms"))
        {
            Debug.Log("Collided with a pre-existing room");
        }
    }
}
