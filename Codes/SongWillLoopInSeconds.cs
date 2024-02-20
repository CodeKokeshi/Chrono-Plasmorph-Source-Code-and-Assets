using UnityEngine;
using UnityEngine.UI;

public class SongWillLoopInSeconds : MonoBehaviour
{
    [SerializeField]
    private float initialTimerDuration = 10f; // Initial duration of the timer in seconds

    private float currentTimer;

    public Text timerText;

    void Start()
    {
        currentTimer = initialTimerDuration;
        UpdateTimerText();
    }

    void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            // Timer has run out, restart it
            currentTimer = initialTimerDuration;
        }
    }

    void UpdateTimerText()
    {
        // Display the timer on the UI text component
        timerText.text = $"Song will loop in: {Mathf.CeilToInt(currentTimer)}s";
    }
}
