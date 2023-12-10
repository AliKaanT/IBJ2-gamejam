// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DG.Tweening;

// public class PlayerController : MonoBehaviour
// {
//     Rigidbody2D body;

//     public float runSpeed = 3.0f;

//     private Color newColor = Color.magenta; // Yeni renk
//     public float maxDistance = 3f; // Maksimum uzaklık

//     private void Start()
//     {
//         body = GetComponent<Rigidbody2D>();
//     }
//     // Update is called once per frame
//     void Update()
//     {
//         MovementHandler();
//         ColorWallsAround();
//     }

//     public void MovementHandler()
//     {
//         float horizontal = Input.GetAxisRaw("Horizontal");
//         float vertical = Input.GetAxisRaw("Vertical");

//         body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
//     }

//     private void OnCollisionEnter2D(Collision2D other)
//     {
//         if (other.gameObject.tag == "EnemyBullet")
//         {
//             Destroy(this.gameObject);
//         }
//         else if (other.gameObject.tag == "Enemy")
//         {
//             Destroy(this.gameObject);
//         }
//     }

//     private void ColorWallsAround()
//     {
//         // Karakterin konumunu al
//         Vector2 characterPosition = transform.position;

//         // Duvarların olduğu layer'daki tüm colliderları al
//         Collider2D[] colliders = Physics2D.OverlapCircleAll(characterPosition, maxDistance, LayerMask.GetMask("Wall"));

//         foreach (Collider2D collider in colliders)
//         {
//             // Duvarların rengini değiştir
//             collider.GetComponent<SpriteRenderer>().DOColor(newColor, 1f);
//         }
//     }

//     void OnDrawGizmosSelected()
//     {
//         // Gizmos ile karakterin belirli uzaklık içinde olduğunu göster
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, maxDistance);
//     }
// }
