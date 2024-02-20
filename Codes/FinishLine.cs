using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    [Header("Score and Timer Components")]
    // Score Components
    [HideInInspector] public bool TimerRunning = false;
    [HideInInspector] public float ElapsedTime = 0f;
    [HideInInspector] public float Highscore;
    public Text timeText;
    public Text HighScoreText;
    public string PlayerPref = "Highscore";


    [Header ("Portal Components")]
    //Portal Components
    public Transform Player;
    public Animator PlayerAnim;
    bool NextSceneReady = false;
    bool GameEnded = false;

    [Header("Transitions")]
    public Animator Transition;
    public bool KillMusic = false;


    Vector3 FreezePosition;


    private void Start()
    {
        timeText.enabled = true;
        HighScoreText.enabled = true;
        GameEnded = false;
        TimerRunning = true;
        if (HighScoreText  != null) { 
            Highscore = PlayerPrefs.GetFloat(PlayerPref, 0f);
            HighScoreText.text = "Best Time: " + ((int)Highscore).ToString() + "s";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MyTagPlayerTag")) { 
            if (GameEnded == false)
            {
                SaveScore(ElapsedTime); //Save best time
                TimerRunning = false; //Stop time elapsed
                GameEnded = true;
                SaveScore(ElapsedTime); //Save best time
            }
        }
    }


    private void Update()
    {
        if (GameEnded == true) // Load next scene
        {
            SaveScore(ElapsedTime); //Save best time
            if (NextSceneReady == false)
            {
                StartCoroutine(PlayEnterPortal()); //Will player the enter portal animation and bring the player position at the center.
                IEnumerator PlayEnterPortal()
                {
                    float duration = .5f;
                    float durationStart = 0f;

                    Vector3 playerStartPosition = Player.transform.position; // Save the initial player position

                    while (durationStart < duration)
                    {
                        float t = durationStart / duration;
                        Vector3 lerpedPosition = Vector3.Lerp(playerStartPosition, transform.position, t);

                        Player.transform.position = new Vector3(lerpedPosition.x, lerpedPosition.y, Player.transform.position.z);

                        durationStart += Time.deltaTime;
                        yield return null;
                    }

                    FreezePosition = Player.transform.position;

                    float LerpDuration = .5f;
                    float LerpDurationStart = 0;
                    Vector3 startScale = transform.localScale;
                    Vector3 targetScale = new Vector3(.4f, .4f, 1);
                    while (LerpDurationStart < LerpDuration)
                    {
                        float t = LerpDurationStart / LerpDuration;

                        Player.transform.localScale = Vector3.Lerp(startScale, targetScale, t);

                        LerpDurationStart += Time.deltaTime;
                        
                        yield return null;
                    }

                    PlayerAnim.SetBool("Death", true); // disappear animation

                    yield return new WaitForSeconds(.4f);


                    NextSceneReady = true;


                }
            }

            if (NextSceneReady == true)
            {
                Player.transform.position = FreezePosition;
                StartCoroutine(LoadNextScene());
                IEnumerator LoadNextScene()
                {
                    if (Transition != null)
                    {
                        Transition.SetBool("Out", true);
                        if (KillMusic == true)
                        {
                            BGMLogic.instance.StopMusic();
                            yield return new WaitForSeconds(.6f);
                        }
                        yield return new WaitForSeconds(1);
                    }

                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }

            }
        }

        if (TimerRunning == true) // Time elapsed update
        {
            ElapsedTime += Time.deltaTime;
            UpdateTime();
        }
    }

    void UpdateTime() //Time elapsed
    {
        if (timeText != null)
        {
            timeText.text = Mathf.Round(ElapsedTime).ToString() + "s";
        }
    }

    void SaveScore(float Score) //Save score
    {
        float PrevHighScore = PlayerPrefs.GetFloat(PlayerPref, Mathf.Infinity);

        if (Score < PrevHighScore)
        {
            PlayerPrefs.SetFloat(PlayerPref, Score);
            PlayerPrefs.Save();
        }
    }
}
