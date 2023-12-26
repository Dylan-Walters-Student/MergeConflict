using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] bool gameActive;

    [SerializeField] GameObject ballOne;

    [SerializeField] GameObject balltwo;

    [SerializeField] GameObject ballThree;

    [SerializeField] float lowSpawnSpeed, HighSpawnSpeed;

    [SerializeField] float spawnRadius = 0.28f;

    GameObject ballHolder;

    float waitingTime;

    float decrementTime = 0.001f;

    float timer;


    void Start()
    {
        gameActive = false;
        ballHolder = new GameObject("GameBallHolder");
        waitingTime = Random.Range(lowSpawnSpeed, HighSpawnSpeed);
    }

    void Update()
    {
        if (gameActive){
            SpawnBalls();
        }
    }

    private void SpawnBalls(){
        timer += Time.deltaTime;

        if (lowSpawnSpeed < -0.2f && HighSpawnSpeed > 0.2f)
        {
            decrementTime *= 1.00005f;
            lowSpawnSpeed += decrementTime;
            HighSpawnSpeed -= decrementTime;
        }

        if (timer > waitingTime)
        {
            Vector3 position = transform.position;
            position.x += Random.Range(-spawnRadius, spawnRadius);
            position.y += Random.Range(-spawnRadius, spawnRadius);
            position.z += Random.Range(-spawnRadius, spawnRadius);
            Quaternion rotation = transform.rotation; //Rotation here is of the spawner this may be odd if using non-ball prefabs

            int randomBall = Random.Range(0, 100);
            if (randomBall < 50)
                Instantiate(ballOne, position, rotation, ballHolder.transform);
            else if (randomBall < 85)
                Instantiate(balltwo, position, rotation, ballHolder.transform);
            else
                Instantiate(ballThree, position, rotation, ballHolder.transform);

            waitingTime = Random.Range(lowSpawnSpeed, HighSpawnSpeed);
            timer = 0;
        }
    }

    public bool getGameStatus() {
        return gameActive;
    }

    public void setGameActive() {
        // remove previous balls here --
        Destroy(ballHolder);
        ballHolder = new GameObject("GameBallHolder");
        gameActive = true;
    }

    public void setGamePaused() {
        gameActive = false;
    }
}
