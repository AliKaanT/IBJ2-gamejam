using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private bool isMoving = true;

    private bool isComingBack = false;
    public Vector3 movingDirection;

    public Animator shurkienAnimator;

    private GameObject gunTransform;

    // Update is called once per frame
    void Update()
    {
        if (isMoving) Move();
        if (isComingBack) ComeBack();
    }

    public void Move()
    {
        transform.Translate(Time.deltaTime * 10 * movingDirection);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            isMoving = false;
            shurkienAnimator.enabled = false;
            isComingBack = false;
            Debug.Log("Hit wall");
        }
        else if (other.gameObject.tag == "BounceWall")
        {
            // bounce it
            Vector3 normal = other.contacts[0].normal;

            movingDirection = movingDirection - 2 * (Vector3.Dot(movingDirection, normal)) * normal;
        }
    }

    public void StartComingBack(GameObject gunTransform)
    {
        if (isComingBack == true) return;

        isComingBack = true;
        isMoving = true;
        shurkienAnimator.enabled = true;
        this.gunTransform = gunTransform;
    }

    public void ComeBack()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = gunTransform.transform.position;

        // calculate desired direction vector
        Vector3 directionOfTravel = targetPosition - currentPosition;

        if (directionOfTravel.magnitude < .5f)
        {
            isComingBack = false;
            isMoving = false;
            shurkienAnimator.enabled = false;
            Destroy(gameObject);
        }

        // normalize vector to get only direction

        Vector3 directionOfTravelNormalized = directionOfTravel.normalized;

        // scale to desired length
        movingDirection = directionOfTravelNormalized * 1;
    }
}
