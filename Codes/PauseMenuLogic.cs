using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseMenuLogic : MonoBehaviour
{
    public GameObject PausePanel;
    public RectTransform PausePanelRectTransform;

    [SerializeField] float topPosY, midPosY;
    [SerializeField] float tweenDuration;

    public Animator TransitionAnim;
    public GameObject Loading;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!PausePanel.activeSelf)
            {
                Pause();
            }

            else if (PausePanel.activeSelf)
            {
                Resume();
            }
        }


    }

    public void Pause()
    {
        if (PausePanel != null)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0f;
            PauseIntro();
        }

    }

    public async void Resume()
    {
        await PauseOutro();
        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1f;

        }
    }

    public async void Restart()
    {
        await PauseOutro();
        Time.timeScale = 1f;
        StartCoroutine(RestartLevel());
    }

    public async void Menu()
    {
        await PauseOutro();
        Time.timeScale = 1f;
        StartCoroutine(MainMenu());
    }

    void PauseIntro()
    {
        PausePanelRectTransform.DOAnchorPosY(midPosY, tweenDuration).SetUpdate(true);
    }

    async Task PauseOutro()
    {
        await PausePanelRectTransform.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();

    }

    IEnumerator RestartLevel()
    {
        Loading.SetActive(true);
        TransitionAnim.SetBool("Out", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }

    IEnumerator MainMenu()
    {
        Loading.SetActive(true);
        TransitionAnim.SetBool("Out", true);
        yield return new WaitForSeconds(1);
        BGMLogic.instance.StopMusic();
        SceneManager.LoadSceneAsync(0);

    }
}
