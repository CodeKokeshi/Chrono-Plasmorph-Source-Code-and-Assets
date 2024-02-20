using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineCollision : MonoBehaviour
{
    public string FinishLine;
    [SerializeField] Rigidbody2D rb;
    bool ActivateFreeze = false;

    private void Start()
    {
        ActivateFreeze = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(FinishLine))
        {
            StartCoroutine(FreezePosition());
            IEnumerator FreezePosition()
            {
                yield return new WaitForSeconds(.5f);
                ActivateFreeze = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(FinishLine))
        {
            StartCoroutine(FreezePosition());
            IEnumerator FreezePosition()
            {
                yield return new WaitForSeconds(.5f);
                ActivateFreeze = true;
            }
        }
    }

    private void Update()
    {
        if (ActivateFreeze == true) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.isKinematic = true;
        }
    }
}
