using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayManager : MonoBehaviour
{
    private bool gamePaused;

    [SerializeField] Spawner spawner;
    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text menuPlayText;

    [SerializeField] TMP_Text highscoreText;
    [SerializeField] TMP_Text matchScoreText;

    [SerializeField] TMP_Text timeText;
    [SerializeField] float totalTime;
    [SerializeField] float currentTime;

    [Header("Back Ground Music")]
    [SerializeField] AudioSource audioSourceCamera;
    [SerializeField] List<AudioClip> musicList;

    [Header("Game Function Testers")]
    // I have this to test without a headset on
    [SerializeField] bool TestPlay;
    [SerializeField] bool TestPause;
    [SerializeField] bool TestContinue;

    private int highScore;
    private int matchScore;

    void Start()
    {
        menuPlayText.text = "Play!";
        UpdateMenuScoreText();
        gamePaused = true;
    }

    void Update()
    {
        PlayMusic();
        Timer();

        // for testing purposes
        if (TestPlay)
        {
            Play();
        }

        if (TestPause)
        {
            Pause();
        }

        if (TestContinue)
        {
            Continue();
        }
    }

    public void Play()
    {
        currentTime = totalTime;
        menu.SetActive(false);
        gamePaused = false;
        spawner.DestroyPreviousBalls();
    }

    public void Pause()
    {
        SetHighscore();
        UpdateMenuScoreText();
        menuPlayText.text = "Continue!";
        // change menu button to use continue
        menu.SetActive(true);
        gamePaused = true;
    }

    // not implimented yet
    public void Continue()
    {
        menu.SetActive(false);
        menuPlayText.text = "Play!";
        //set menu button to use play
        gamePaused = false;
    }

    //Winning and loosing is the same thing atm
    //Might seperate later if different conditions apply
    public void WinLose()
    {
        gamePaused = true;

        SetHighscore();
        UpdateMenuScoreText();

        menu.SetActive(true);
        currentTime = totalTime;
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
                // Showing a popup to say the timer ran out would be nice
                WinLose();
                gamePaused = true;
            }
        }
    }

    private void SetHighscore()
    {
        if (matchScore > highScore)
        {
            highScore = matchScore;
        }
    }

    private void UpdateMenuScoreText()
    {
        highscoreText.text = highScore.ToString();
        matchScoreText.text = highScore.ToString();
    }

    public void AddToMatchScore(int points)
    {
        matchScore += points;
    }

    public void SetGameTime(float newGameTime)
    {
        totalTime = newGameTime;
    }

    /// <summary>
    /// Gets the game 'paused' state indicating that the game is either running or not.
    /// </summary>
    /// <returns>True if paused, falsed if the game is running</returns>
    public bool GetGameStatus()
    {
        return gamePaused;
    }

    private void PlayMusic()
    {
        if (!audioSourceCamera.isPlaying && musicList != null)
        {
            int randomPosition = Random.Range(0, musicList.Count - 1);

            audioSourceCamera.clip = musicList[randomPosition];
            audioSourceCamera.Play();
        }
    }
}
