using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    [Header("Table")]
    [SerializeField] Slider tableHeightSlider;
    [SerializeField] GameObject table;

    [Header("Game")]
    PlayManager playManager;
    [SerializeField] Slider gameTimeSlider;
    [SerializeField] TMP_Text gameTimeText;

    private void Start()
    {
        playManager = GetComponent<PlayManager>();
        playManager.SetGameTime(gameTimeSlider.value);

        int minutes = Mathf.FloorToInt(gameTimeSlider.value / 60f);
        int seconds = Mathf.FloorToInt(gameTimeSlider.value % 60f);

        gameTimeText.text = "Game Time- " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void Update()
    {
        tableHeightSlider.onValueChanged.AddListener(UpdateTableHeight);
        gameTimeSlider.onValueChanged.AddListener(UpdateGameTime);
    }

    void UpdateTableHeight(float value)
    {
        table.transform.position = new Vector3(0, value, 0.522f);
    }

    void UpdateGameTime(float value)
    {
        playManager.SetGameTime(value);

        int minutes = Mathf.FloorToInt(gameTimeSlider.value / 60f);
        int seconds = Mathf.FloorToInt(gameTimeSlider.value % 60f);

        gameTimeText.text = "Game Time- " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
