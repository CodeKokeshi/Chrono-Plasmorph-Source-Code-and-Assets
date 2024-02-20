using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public Vector3 Direction = Vector3.left;

    void Update()
    {
        transform.position = transform.position + (Direction * MoveSpeed) * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Deleter"))
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Deleter"))
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Deleter"))
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
