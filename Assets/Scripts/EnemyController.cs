using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject enemyBulletPrefab;

    // Oyuncunun konumunu tutmak için bir değişken
    public Transform playerTransform;
    public float speed = 1f;

    private Rigidbody2D rb;
    private bool isTriggered = false;

    private GameObject bulletInstance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleVision();
        TriggerToPlayer();
        HandleAttack();
    }

    private void TriggerToPlayer()
    {
        if (isTriggered)
        {
            float horizontal = playerTransform.position.x - transform.position.x;
            float vertical = playerTransform.position.y - transform.position.y;

            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
    }

    private void HandleVision()
    {
        if (!isTriggered)
        {
            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = playerTransform.position;
            RaycastHit2D hit = Physics2D.Linecast(enemyPosition, playerPosition, LayerMask.GetMask("Wall"));
            if (hit.collider == null)
                isTriggered = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Shuriken")
        {
            Destroy(this.gameObject);
        }
    }

    private void HandleAttack()
    {
        if (isTriggered)
        {

            if (bulletInstance == null)
            {
                Vector2 enemyPosition = transform.position;
                Vector2 playerPosition = playerTransform.position;
                Vector2 direction = playerPosition - enemyPosition;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bulletInstance = Instantiate(enemyBulletPrefab, enemyPosition, rotation);



            }

        }
    }
}
