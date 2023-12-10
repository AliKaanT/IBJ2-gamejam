using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Time.deltaTime * 10 * Vector3.right);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
