using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveCameraWhenFallingDown : MonoBehaviour
{
    public CinemachineVirtualCamera PlayerCamera;
    public float RemoveCameraAxis = -5.5f;
    [SerializeField] AudioSource FellSFX;

    void Update()
    {
        if (PlayerCamera != null)
        {
            if (transform.position.y <= RemoveCameraAxis)
            {
                if (FellSFX !=  null)
                {
                    if (!FellSFX.isPlaying) {
                    FellSFX.Play();}
                }
                PlayerCamera.Follow = null;
                if (transform.position.y <= RemoveCameraAxis * 4)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}
