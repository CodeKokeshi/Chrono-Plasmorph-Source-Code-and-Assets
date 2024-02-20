using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePortal : MonoBehaviour
{
    public GameObject portal;
    bool WasAlreadyActivated = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (WasAlreadyActivated == false)
        {
            if (collision.CompareTag("MyTagPlayerTag"))
            {
                portal.SetActive(true);
                WasAlreadyActivated = true;
            }
        }

    }
}
