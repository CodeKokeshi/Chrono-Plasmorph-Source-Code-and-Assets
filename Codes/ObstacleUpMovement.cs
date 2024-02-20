using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovementUp : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveSpeed = 5f;
    public Vector3 Direction = Vector3.up;

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
