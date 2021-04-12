using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DoorWay : MonoBehaviour
{
    private void Awake()
    {
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
    }
}
