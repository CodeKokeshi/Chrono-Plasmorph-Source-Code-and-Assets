using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eaten : MonoBehaviour
{
    Animator Disappear;
    [SerializeField] AudioSource FruitEatenSFX;

    private void Start()
    {

        Disappear = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MyTagPlayerTag") || collision.collider.CompareTag("Obstacles"))
        {
            if (collision.collider.CompareTag("MyTagPlayerTag")) { 
                if (!FruitEatenSFX.isPlaying && FruitEatenSFX != null)
                {
                    FruitEatenSFX.Play();
                }
            }
            StartCoroutine(DeleteSelf());
            IEnumerator DeleteSelf()
            {
                Disappear.SetBool("Eaten", true);
                yield return new WaitForSeconds(.350f);
                Destroy(gameObject);
            }
        }
    }
}
