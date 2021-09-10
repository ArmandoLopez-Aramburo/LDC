using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DoorWay : MonoBehaviour
{
    private void Awake()
    {
        // Set's the constraints and locks them in such that the colliders don't move.
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Detects the Walls for the Doorway and deletes them
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(collision.gameObject);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    // Detects the floors and deletes them, OnTrigger had to be set so that the player can walk on it without colliding.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(collision.gameObject);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
