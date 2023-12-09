using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    [SerializeField] private GameObject slider;
    [SerializeField] private Transform clickedPosition;
    [SerializeField] private Transform nonClickedPosition;


    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit button");
            slider.transform.position = clickedPosition.position;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("exit button");
            slider.transform.position = nonClickedPosition.position;
        }

    }
}
