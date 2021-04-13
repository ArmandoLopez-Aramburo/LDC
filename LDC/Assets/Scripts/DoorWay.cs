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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Destroy(collision.gameObject);
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        this.gameObject.SetActive(false);
    }
}
