using BayatGames.SaveGameFree;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    private bool gamePaused;
    private int highScore;
    private int matchScore;
    private float totalTime;
    private float currentTime;

    [SerializeField] Spawner spawner;
    [SerializeField] Material podiumIndicator;

    [SerializeField] TMP_Text highscoreText;
    [SerializeField] TMP_Text matchScoreText;
    [SerializeField] TMP_Text timeText;

    [Header("Menus")]
    [SerializeField] GameObject PlayMenu;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ScoreboardMenu;
    [SerializeField] GameObject AboutMenu;

    [Header("Back Ground Music")]
    [SerializeField] AudioSource audioSourceCamera;
    [SerializeField] List<AudioClip> musicList;

    void Awake()
    {
        highScore = SaveGame.Load<int>("Highscore");
    }

    void Start()
    {
        MainMenu();
        UpdateMenuScoreText();
        currentTime = totalTime;
        podiumIndicator.color = new Color(150/255f, 52/255f, 52/255f, 61/255f);
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
        spawner.DestroyPreviousBalls();
        currentTime = totalTime;
        podiumIndicator.color = new Color(113/255f, 113/255f, 113/255f, 61/255f);
        gamePaused = false;
    }

    public void Pause()
    {
        SetHighscore();
        UpdateMenuScoreText();
        MainMenu();
        podiumIndicator.color = new Color(150/255f, 52/255f, 52/255f, 61/255f);
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
        SetHighscore();
        UpdateMenuScoreText();
        MainMenu();
        gamePaused = true;
        podiumIndicator.color = new Color(150 / 255f, 52 / 255f, 52 / 255f, 61 / 255f);
        currentTime = totalTime;
        matchScore = 0; // Needs to happen after setting match text.
    }

    public void CloseMenu()
    {
        PlayMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        ScoreboardMenu.SetActive(false);
        AboutMenu.SetActive(false);
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
