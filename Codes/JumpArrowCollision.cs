using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpArrowCollision : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    public string ArrowTag;
    public float BoostedJumpPower = 20f;
    [SerializeField] AudioSource BouncySFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ArrowTag))
        {
            if (BouncySFX != null)
            {
                if (!BouncySFX.isPlaying)
                {
                    BouncySFX.Play();

                }
            }
            rb.velocity = new Vector2(rb.velocity.x, BoostedJumpPower);
        }
    }
}
