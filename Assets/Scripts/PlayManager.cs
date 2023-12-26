using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    [SerializeField] TMP_Text playText;
    
    public void Play(){
        if(spawner.getGameStatus()){
            spawner.setGamePaused();
            playText.text = "Play";
        } else {
            spawner.setGameActive();
            playText.text = "Stop!";
        }
    }

    public void CalculateScore() {
        //get all balls that have merge enabled
        //score each ball based on their scoring value
        //instantiate menu item popup with a score
        //if its a new highscore put it on the main menu otherwise forget about it
    }
}
