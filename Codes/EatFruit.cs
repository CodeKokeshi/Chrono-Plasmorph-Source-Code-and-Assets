using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFruit : MonoBehaviour

{
    public float TransformationDuration = 0.7f;
    public float TargetSize = 1.25f;
    public CinemachineVirtualCamera PlayerCamera;
    public float CameraZoomOut = 2f;
    public float CameraZoomDuration = 1f;
    [SerializeField] AudioSource BulkSFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fruit")) {
            GetComponent<BulkedOrNot>().Bulked = true; //Identifies that he's now bulked.
            StartCoroutine(GetBulk());

            IEnumerator GetBulk()
            {
                if (BulkSFX != null)
                {
                    if (!BulkSFX.isPlaying)
                    {
                        BulkSFX.Play();
                    }
                }
                Vector3 startScale = transform.localScale;
                Vector3 targetScale = new Vector3(TargetSize, TargetSize, 1);
                float initialSize = PlayerCamera.m_Lens.OrthographicSize;
                float targetSize = PlayerCamera.m_Lens.OrthographicSize * CameraZoomOut;

                float timeElapsed = 0f;

                while (timeElapsed < TransformationDuration)
                {
                    transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / TransformationDuration);
                    PlayerCamera.m_Lens.OrthographicSize = Mathf.Lerp(initialSize, targetSize, timeElapsed / CameraZoomDuration);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                // Ensure the final scale is set

                transform.localScale = targetScale;


            }




        }

        if (collision.collider.CompareTag("Orange"))
        {
            GetComponent<BulkedOrNot>().OrangeEaten = true; 

        }
    }
}
