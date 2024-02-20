using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBrickDeleteLater : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MyTagPlayerTag"))
        {
            Destroy(gameObject);
        }
    }
}
