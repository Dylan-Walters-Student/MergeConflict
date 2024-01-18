using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayManager : MonoBehaviour
{
    private bool gamePaused;

    [SerializeField] Spawner spawner;
    [SerializeField] Material podiumIndicator;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject stopButton;
    [SerializeField] TMP_Text highscoreText;
    [SerializeField] TMP_Text matchScoreText;

    [SerializeField] TMP_Text timeText;
    [SerializeField] float totalTime;
    [SerializeField] float currentTime;

    [Header("Menus")]
    [SerializeField] GameObject PlayMenu;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ScoreboardMenu;
    [SerializeField] GameObject AboutMenu;

    [Header("Back Ground Music")]
    [SerializeField] AudioSource audioSourceCamera;
    [SerializeField] List<AudioClip> musicList;

    [Header("Game Function Testers")]
    // I have this to test without a headset on
    [SerializeField] bool TestPlay;
    [SerializeField] bool TestPause;
    [SerializeField] bool TestContinue;
    [SerializeField] bool TestWinLose;

    private int highScore;
    private int matchScore;

    void Start()
    {
        MainMenu();
        pauseButton.SetActive(false);
        stopButton.SetActive(false);
        podiumIndicator.color = new Color(150/255f, 52/255f, 52/255f, 61/255f);
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

        if (TestWinLose)
        {
            WinLose();
        }
    }

    public void Play()
    {
        pauseButton.SetActive(true);
        stopButton.SetActive(true);
        currentTime = totalTime;
        PlayMenu.SetActive(false);
        gamePaused = false;
        spawner.DestroyPreviousBalls();
        podiumIndicator.color = new Color(113/255f, 113/255f, 113/255f, 61/255f);
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        stopButton.SetActive(false);
        SetHighscore();
        UpdateMenuScoreText();
        // change menu button to use continue image
        podiumIndicator.color = new Color(150/255f, 52/255f, 52/255f, 61/255f);
        PlayMenu.SetActive(true);
        gamePaused = true;
    }

    public void Continue()
    {
        pauseButton.SetActive(true);
        stopButton.SetActive(true);
        podiumIndicator.color = new Color(113/255f, 113/255f, 113/255f, 61/255f);
        PlayMenu.SetActive(false);
        //set menu button to use play image
        gamePaused = false;
    }

    //Winning and loosing is the same thing atm
    //Might seperate later if different conditions apply
    public void WinLose()
    {
        pauseButton.SetActive(false);
        stopButton.SetActive(false);

        gamePaused = true;
        podiumIndicator.color = new Color(150/255f, 52/255f, 52/255f, 61/255f);

        SetHighscore();
        UpdateMenuScoreText();

        PlayMenu.SetActive(true);
        currentTime = totalTime;
        matchScore = 0; // Needs to happen after setting match text.
    }

    public void MainMenu()
    {
        PlayMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        ScoreboardMenu.SetActive(false);
        AboutMenu.SetActive(false);
    }

    public void Settings()
    {
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        ScoreboardMenu.SetActive(false);
        AboutMenu.SetActive(false);
    }
    public void Scoreboard()
    {
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        ScoreboardMenu.SetActive(true);
        AboutMenu.SetActive(false);
    }
    public void About()
    {
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        ScoreboardMenu.SetActive(false);
        AboutMenu.SetActive(true);
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
        matchScoreText.text = matchScore.ToString();
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
