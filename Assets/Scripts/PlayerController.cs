using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float DashCooldown = 0;

    public float runSpeed = 3.0f;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(CoolDownCalculator());
    }

    void Update()
    {
        MovementHandler();
        DashHandler();
    }

    public void MovementHandler()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void DashHandler()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (DashCooldown <= 0)
            {
                DashCooldown = 1f;
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
                Vector2 direction = new(horizontal, vertical);

                body.DOMove(body.position + direction * 4, 0.3f);
            }
        }
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

    private IEnumerator CoolDownCalculator()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            DashCooldown -= 0.5f;
            if (DashCooldown < 0)
            {
                DashCooldown = 0;
            }
        }
    }
}
