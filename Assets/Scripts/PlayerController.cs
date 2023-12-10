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

    private float DashCooldown = 0;

    public float runSpeed = 3.0f;

    public Animator animator;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        StartCoroutine(CoolDownCalculator());
    }

    void Update()
    {
        MovementHandler();
        DashHandler();

        float oldZ = Camera.main.transform.position.z;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, oldZ);
    }

    public void MovementHandler()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);




        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("isWalking", true);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(horizontal, vertical, 0));
            transform.Rotate(0, 0, 0);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    public void DashHandler()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (DashCooldown <= 0)
            {

                animator.SetTrigger("onDash");
                DashCooldown = 2.5f;
                AudioManager.instance.PlayDashSound();
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
