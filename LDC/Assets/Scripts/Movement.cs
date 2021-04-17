using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float MOVE_SPEED = 10.0f;
    private Rigidbody2D rb;
    private Vector3 moveDir;
    // Start is called before the first frame update

    [SerializeField] private Sprite[] Character = null;
    [SerializeField] private GameObject CharacterSprite = null;
    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = 0.0f;
        float moveY = 0.0f;
        if(Input.GetKey(KeyCode.W))
        {
            CharacterSprite.GetComponent<SpriteRenderer>().sprite = Character[1];
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            CharacterSprite.GetComponent<SpriteRenderer>().sprite = Character[0];
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            CharacterSprite.GetComponent<SpriteRenderer>().flipX = true;
            CharacterSprite.GetComponent<SpriteRenderer>().sprite = Character[2];
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            CharacterSprite.GetComponent<SpriteRenderer>().flipX = false;
            CharacterSprite.GetComponent<SpriteRenderer>().sprite = Character[3];
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * MOVE_SPEED;
    }
}
