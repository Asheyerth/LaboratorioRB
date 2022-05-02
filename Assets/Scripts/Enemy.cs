using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public float speed;
    Rigidbody2D rb;
    public GameObject player;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (rb.velocity == Vector2.zero)
        {
            float step = speed * Time.deltaTime;
            Debug.Log("Posicion en X player");
            Debug.Log(player.transform.position.x);
            Debug.Log("Posicion en Y player");
            Debug.Log(player.transform.position.y);
            Vector2 next = BoardManager.Instance.nextMovement((int)transform.position.x, (int)transform.position.y, (int)player.transform.position.x, (int)player.transform.position.y);

            float dirx = next.x - transform.position.x;
            float diry = next.y - transform.position.y;
            if (Mathf.Abs(dirx) < Mathf.Abs(diry) && Mathf.Abs(dirx) > Mathf.Epsilon)
            {
                next.y = transform.position.y;
            }
            else if (Mathf.Abs(diry) > Mathf.Epsilon)
            {
                next.x = transform.position.x;
            }


            transform.position = Vector2.MoveTowards(transform.position, next, step);
            Debug.Log(next);
        }
    }
}

