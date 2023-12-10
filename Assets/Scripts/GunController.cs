using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private GameObject bulletInstance;

    // Update is called once per frame
    void Update()
    {
        RotateGunWithMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (bulletInstance == null) return;
            bulletInstance.GetComponent<BulletController>().StartComingBack(gameObject);
        }

    }

    void Shoot()
    {
        if (bulletInstance != null) return;

        Vector3 position = gameObject.transform.position;

        bulletInstance = Instantiate(bulletPrefab, position, Quaternion.identity);

        bulletInstance.GetComponent<BulletController>().movingDirection = gameObject.transform.right;

    }

    private void RotateGunWithMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.right = direction;
    }
}
