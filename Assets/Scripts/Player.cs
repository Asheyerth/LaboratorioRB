using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D playerb;
    private bool cblock = false;
    public float speed = 10f;
    public bool _collisionblock = false;

    private square[,] frameArray;

    public Animator playeranim;
    private Vector3 moved;


    public int tam;
    public int obst;
    public Player player;


    // Start is called before the first frame update
    void Start()
    {
        playerb = this.GetComponent<BoxCollider2D>();
        frameArray = new square[1, 1];

    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moved = new Vector3(x, y, 0);
        

        if (moved.x > 0)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (moved.x < 0)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }

        transform.Translate(moved * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "box")
        {
            _collisionblock = true;
        }

    }
}
