using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject enemyBulletPrefab;

    // Oyuncunun konumunu tutmak için bir değişken
    public Transform playerTransform;
    public float speed = 1f;

    private Rigidbody2D rb;
    private bool isTriggered = false;
    private int frameCount = 0;

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
    }

    private void TriggerToPlayer()
    {
        if (isTriggered)
        {
            if (frameCount == 10)
            {
                frameCount = 0;
                Transform targetTransform = DjkstraManager.Instance.Run(gameObject.transform, playerTransform);
                rb.velocity = (targetTransform.position - transform.position).normalized * speed;

                transform.rotation = Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position);
                transform.Rotate(0, 0, 90);
            }
            else frameCount++;

        }
    }

    private void HandleVision()
    {
        if (!isTriggered)
        {
            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = playerTransform.position;
            RaycastHit2D hit = Physics2D.Linecast(enemyPosition, playerPosition, LayerMask.GetMask("Wall"));

            float distance = Vector2.Distance(enemyPosition, playerPosition);

            if (hit.collider == null && distance < 10f)
            {
                Debug.Log(distance);
                StartCoroutine(ShootABullet());
                isTriggered = true;
                EnemyPathController epc = gameObject.GetComponent<EnemyPathController>();
                if (epc != null)
                    epc.StopPatrolling();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Shuriken")
        {
            GameManager.instance.EnemyKilled();
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ShootABullet()
    {
        while (true)
        {
            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = playerTransform.position;
            RaycastHit2D hit = Physics2D.Linecast(enemyPosition, playerPosition, LayerMask.GetMask("Wall"));
            if (hit.collider == null)
            {
                Vector2 direction = playerPosition - enemyPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bulletInstance = Instantiate(enemyBulletPrefab, enemyPosition, rotation);
                AudioManager.instance.PlayEnemyShootSound();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
