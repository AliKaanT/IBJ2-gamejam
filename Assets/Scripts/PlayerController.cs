using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 3.0f;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovementHandler();
    }

    public void MovementHandler()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            GameManager.instance.GameOver();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            GameManager.instance.GameOver();
        }
    }
}
