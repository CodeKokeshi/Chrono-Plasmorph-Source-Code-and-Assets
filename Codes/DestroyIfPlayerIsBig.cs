using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DestroyIfPlayerIsBig : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public Animator Brick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] AudioSource BreakBrickSFX;
    private ShadowCaster2D shadowCaster;

    private void Start()
    {
        shadowCaster = GetComponent<ShadowCaster2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MyTagPlayerTag"))
        {
            if (Player != null)
            {
                if (Player.GetComponent<BulkedOrNot>().Bulked == true)
                {
                    if (!BreakBrickSFX.isPlaying)
                    {
                        BreakBrickSFX.Play();
                    }
                    rb.constraints = RigidbodyConstraints2D.None;
                    StartCoroutine(BreakBrick());
                    IEnumerator BreakBrick()
                    {

                        Brick.SetBool("Break", true);
                        if (shadowCaster != null)
                        {
                            shadowCaster.enabled = false;
                        }

                        yield return new WaitForSeconds(.33f);

                        Destroy(gameObject);

                    }
                }
            }
        }
    }
}
