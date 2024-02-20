using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBrick : MonoBehaviour
{
    // Start is called before the first frame update

    public LayerMask PlayerLayer;
    public float DistanceFromPlayer = .01f;
    public Animator BrickAnimation;
    bool BrickAlreadyHit = false;
    public GameObject FruitPrefab;
    public Transform CheckPlayerBelow;
    bool Spawned = false;
    [SerializeField] AudioSource ShakeBrickSFX;
    public float Offset = 1f;

    private void Update()
    {
        if (BrickAlreadyHit == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(CheckPlayerBelow.position, Vector2.down, DistanceFromPlayer, PlayerLayer);
            if (hit.collider != null)
            {
                StartCoroutine(WaitAnimFinish("ShakeBrick", true));
                if (FruitPrefab != null)
                {
                    StartCoroutine(SpawnFruit());
                }
            }

        }
        else
        {
            BrickAnimation.SetBool("ShakeBrick", false);
        }
        ;

    
    }

    IEnumerator WaitAnimFinish(string str, bool implication)
    {
        if (!ShakeBrickSFX.isPlaying)
        {
            ShakeBrickSFX.Play();
        }
        BrickAnimation.SetBool(str, implication);
        yield return new WaitForSeconds(.5f);
    }


    IEnumerator SpawnFruit()
    {
        yield return new WaitForSeconds(.15f);

        if (Spawned == false)
        {
            Instantiate(FruitPrefab, new Vector3(transform.position.x, transform.position.y + Offset, 0), transform.rotation);
            Spawned = true;
        }
        if (Spawned == true)
        {
            BrickAlreadyHit = true;
        }

    }
}
