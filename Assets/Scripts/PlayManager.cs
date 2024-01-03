using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayManager : MonoBehaviour
{
    // ui and spawner
    [SerializeField] Spawner spawner;
    [SerializeField] Canvas MainMenu;

    // timer
    float saveTime;
    [SerializeField] float timeRemaining;
    [SerializeField] TMP_Text timeText;

    public void Play(){
        if(spawner.getGameStatus()){
            spawner.setGameActive(); // starts game, resets field
            MainMenu.enabled = false;
            saveTime = timeRemaining;
            Timer(true);
        }
        else {
            spawner.setGamePaused(); //pauses game -- basically stops cant unpause only restart
            MainMenu.enabled = true;
            Timer(false);
            timeRemaining = saveTime;
            //calculate score
        }
    }

    void Timer(bool gameRunning)
    {
        if (!gameRunning)
        {
            timeRemaining -= Time.deltaTime;

            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);

            if (timeRemaining <= 0)
            {
                spawner.setGamePaused();
            }
        }
    }
}
