using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayManager : MonoBehaviour
{
    [SerializeField] bool gamePaused;

    [SerializeField] Spawner spawner;
    [SerializeField] Canvas MainMenu;

    [SerializeField] TMP_Text timeText;
    [SerializeField] float totalTime = 60f;
    [SerializeField] float currentTime;

    private int highScore;
    private int matchScore;

    void Start()
    {
        gamePaused = true;
    }

    void Update()
    {
        Timer();
    }

    public void Play()
    {
        MainMenu.enabled = false;
        gamePaused = false;
        spawner.DestroyPreviousBalls();
    }

    public void Pause()
    {
        MainMenu.enabled = true;
        gamePaused = true;
    }

    public void Continue()
    {
        MainMenu.enabled = false;
        gamePaused = false;
    }

    //Winning and loosing is the same thing atm
    //Might seperate later if different conditions apply
    public void WinLose()
    {
        gamePaused = true;
        MainMenu.enabled = true;
        currentTime = totalTime;

        // sets match score
        if (matchScore > highScore)
        {
            highScore = matchScore;
        }

        // set match score text
        // set highscore text if needed

        matchScore = 0; // Needs to happen after setting match text.
    }

    public void Timer()
    {
        if (!gamePaused)
        {
            currentTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (currentTime <= 0f)
            {
                Debug.Log("Lose");
                gamePaused = true;
            }
        }
    }

    public void AddToMatchScore(int points)
    {
        matchScore += points;
    }

    /// <summary>
    /// Gets the game 'paused' state indicating that the game is either running or not.
    /// </summary>
    /// <returns>True if paused, falsed if the game is running</returns>
    public bool GetGameStatus()
    {
        return gamePaused;
    }
}
