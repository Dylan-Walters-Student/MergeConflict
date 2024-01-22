using BayatGames.SaveGameFree;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    private bool gamePaused;

    [SerializeField] Spawner spawner;
    [SerializeField] Material podiumIndicator;

    TMP_Text highscoreText;
    TMP_Text matchScoreText;
    TMP_Text timeText;
    float totalTime;
    float currentTime;

    [Header("Menus")]
    [SerializeField] GameObject PlayMenu;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ScoreboardMenu;
    [SerializeField] GameObject AboutMenu;
    [SerializeField] GameObject HandMenu;

    [Header("Back Ground Music")]
    [SerializeField] AudioSource audioSourceCamera;
    [SerializeField] List<AudioClip> musicList;

    private int highScore;
    private int matchScore;

    void Awake()
    {
        highScore = SaveGame.Load<int>("Highscore");
    }

    void Start()
    {
        MainMenu();
        podiumIndicator.color = new Color(150/255f, 52/255f, 52/255f, 61/255f);
        UpdateMenuScoreText();
        gamePaused = true;
    }

    void Update()
    {
        PlayMusic();
        Timer();
    }

    public void Play()
    {
        CloseMenu();
        currentTime = totalTime;
        gamePaused = false;
        spawner.DestroyPreviousBalls();
        podiumIndicator.color = new Color(113/255f, 113/255f, 113/255f, 61/255f);
    }

    public void Pause()
    {
        SetHighscore();
        UpdateMenuScoreText();
        podiumIndicator.color = new Color(150/255f, 52/255f, 52/255f, 61/255f);
        MainMenu();
        gamePaused = true;
    }

    public void Continue()
    {
        CloseMenu();
        podiumIndicator.color = new Color(113/255f, 113/255f, 113/255f, 61/255f);
        gamePaused = false;
    }

    public void WinLose()
    {
        gamePaused = true;
        podiumIndicator.color = new Color(150/255f, 52/255f, 52/255f, 61/255f);

        SetHighscore();
        UpdateMenuScoreText();
        MainMenu();

        currentTime = totalTime;
        matchScore = 0; // Needs to happen after setting match text.
    }

    public void CloseMenu()
    {
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        ScoreboardMenu.SetActive(false);
        AboutMenu.SetActive(false);
        HandMenu.SetActive(true);
    }

    public void MainMenu()
    {
        PlayMenu.SetActive(true);
        SettingsMenu.SetActive(false);
        ScoreboardMenu.SetActive(false);
        AboutMenu.SetActive(false);
        HandMenu.SetActive(false);
    }

    public void Settings()
    {
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        ScoreboardMenu.SetActive(false);
        AboutMenu.SetActive(false);
        HandMenu.SetActive(false);
    }
    public void Scoreboard()
    {
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        ScoreboardMenu.SetActive(true);
        AboutMenu.SetActive(false);
        HandMenu.SetActive(false);
    }
    public void About()
    {
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        ScoreboardMenu.SetActive(false);
        AboutMenu.SetActive(true);
        HandMenu.SetActive(false);
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
            SaveGame.Save<int>("Highscore", highScore);
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
