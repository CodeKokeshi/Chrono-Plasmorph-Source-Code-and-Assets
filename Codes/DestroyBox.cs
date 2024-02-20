using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    [SerializeField] Animator Box;
    [SerializeField] AudioSource BoxBreak;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacles"))
        {
            if (Box != null)
            {
                StartCoroutine(DestroyBoxAnim());
            }
        }
    }

    IEnumerator DestroyBoxAnim()
    {
        Box.SetBool("Break", true);
        if (BoxBreak != null) { 
            BoxBreak.Play();
        }
        yield return new WaitForSeconds(0.19f);
        Destroy(gameObject);
    }
}
