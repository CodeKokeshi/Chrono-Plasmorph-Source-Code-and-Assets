using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBrickRNG : MonoBehaviour
{
    public LayerMask PlayerLayer;
    public float DistanceFromPlayer = .01f;
    public Animator BrickAnimation;
    bool BrickAlreadyHit = false;
    public Transform CheckPlayerBelow;
    bool Spawned = false;
    [SerializeField] AudioSource ShakeBrickSFX;
    public float Offset = 1f;

    [Header("RNG Components")]
    public GameObject MainPrefab;
    public GameObject Traps1;
    public GameObject Traps2;
    public GameObject Fruit1;
    public GameObject Fruit2;
    public GameObject Fruit3;

    private void Update()
    {
        if (BrickAlreadyHit == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(CheckPlayerBelow.position, Vector2.down, DistanceFromPlayer, PlayerLayer);
            if (hit.collider != null)
            {
                StartCoroutine(WaitAnimFinish("ShakeBrick", true));

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
        StartCoroutine(SpawnFruit());
        yield return new WaitForSeconds(.5f);


    }


    IEnumerator SpawnFruit()
    {
        yield return new WaitForSeconds(.15f);

        if (Spawned == false)
        {
            int randomIndex = Random.Range(1, 7);
            GameObject selectedPrefab = null;

            switch (randomIndex)
            {
                case 1:
                    selectedPrefab = MainPrefab;
                    break;
                case 2:
                    selectedPrefab = Traps1;
                    Offset = -0.5f;
                    break;
                case 3:
                    selectedPrefab = Traps2;
                    Offset = - 0.5f;
                    break;
                case 4:
                    selectedPrefab = Fruit1;
                    break;
                case 5:
                    selectedPrefab = Fruit2;
                    break;
                case 6:
                    selectedPrefab = Fruit3;
                    break;
            }
            if (selectedPrefab != null)
            {
                Instantiate(selectedPrefab, new Vector3(transform.position.x, transform.position.y + Offset, 0), transform.rotation);
                Spawned = true;
            }
        }
        if (Spawned == true)
        {
            BrickAlreadyHit = true;

        }

    }
}

