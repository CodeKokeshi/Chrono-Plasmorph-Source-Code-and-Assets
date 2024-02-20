using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleCollision : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator PlayerAnimation;
    public CinemachineVirtualCamera PlayerCamera;
    [SerializeField] Rigidbody2D rb;

    [Header ("SFX")]
    [SerializeField] AudioSource HitWhileBulked;
    [SerializeField] AudioSource Death;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacles"))
        {
            if (GetComponent<BulkedOrNot>().Bulked == true)
            {
                StartCoroutine(HitAnim());

                IEnumerator HitAnim()
                {
                    if (HitWhileBulked != null)
                    {
                        if (!HitWhileBulked.isPlaying)
                        {
                            HitWhileBulked.Play();

                        }
                    }
                    PlayerAnimation.SetBool("Hit", true);
                    yield return new WaitForSeconds(.33f);
                    PlayerAnimation.SetBool("Hit", false);

                }

                StartCoroutine(NormalizeSize());
                IEnumerator NormalizeSize()
                {
                    Vector3 startScale = transform.localScale;
                    Vector3 targetScale = new Vector3(1, 1, 1);
                    float initialSize = PlayerCamera.m_Lens.OrthographicSize;
                    float targetSize = PlayerCamera.m_Lens.OrthographicSize / GetComponent<EatFruit>().CameraZoomOut;

                    float timeElapsed = 0f;

                    while (timeElapsed < GetComponent<EatFruit>().TransformationDuration)
                    {
                        transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / GetComponent<EatFruit>().TransformationDuration);
                        PlayerCamera.m_Lens.OrthographicSize = Mathf.Lerp(initialSize, targetSize, timeElapsed / GetComponent<EatFruit>().CameraZoomDuration);
                        timeElapsed += Time.deltaTime;
                        yield return null;
                    }

                    transform.localScale = targetScale;
                    GetComponent<BulkedOrNot>().Bulked = false;
                }

            }
            else if (GetComponent<BulkedOrNot>().Bulked == false)
            {
                //GameOverLogic Here But for now restart.
                //
                StartCoroutine(PlayerDeathAnim());
                IEnumerator PlayerDeathAnim()
                {
                    if (Death != null)
                    {
                        if (!Death.isPlaying)
                        {
                            Death.Play();

                        }
                    }
                    PlayerAnimation.SetBool("Death", true);
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    rb.isKinematic = true;
                    PlayerCamera.Follow = null;
                    GetComponent<PlayerMovement>().enabled = false;
                    yield return new WaitForSeconds(.433f);
                    GetComponent<SpriteRenderer>().enabled = false;
                    yield return new WaitForSeconds(.5f);
                    //Fade Effect Here then wait 1 seconds then restart scene
                    yield return new WaitForSeconds(.5f);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) //In case we got stuck and Enter2d doesn't work.
    {
        if (collision.collider.CompareTag("Obstacles"))
        {
            if (GetComponent<BulkedOrNot>().Bulked == true)
            {
                StartCoroutine(HitAnim());

                IEnumerator HitAnim()
                {
                    if (HitWhileBulked != null)
                    {
                        if (!HitWhileBulked.isPlaying)
                        {
                            HitWhileBulked.Play();

                        }
                    }
                    PlayerAnimation.SetBool("Hit", true);
                    yield return new WaitForSeconds(.33f);
                    PlayerAnimation.SetBool("Hit", false);
                }
            }

            else if (GetComponent<BulkedOrNot>().Bulked == false)
            {
                StartCoroutine(PlayerDeathAnim());
                IEnumerator PlayerDeathAnim()
                {
                    if (Death != null)
                    {
                        if (!Death.isPlaying)
                        {
                            Death.Play();

                        }
                    }
                    PlayerAnimation.SetBool("Death", true);
                    rb.isKinematic = true;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    PlayerCamera.Follow = null;
                    GetComponent<PlayerMovement>().enabled = false;
                    yield return new WaitForSeconds(.433f);
                    GetComponent<SpriteRenderer>().enabled = false;
                    yield return new WaitForSeconds(.5f);
                    //Fade Effect Here then wait 1 seconds then restart scene
                    yield return new WaitForSeconds(.5f);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

    }
}
