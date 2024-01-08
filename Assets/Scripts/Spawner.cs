using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] PlayManager playManager;

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
        ballHolder = new GameObject("GameBallHolder");
        waitingTime = Random.Range(lowSpawnSpeed, HighSpawnSpeed);
    }

    void Update()
    {
        if (!playManager.GetGameStatus())
        {
            SpawnBalls();
        }
    }

    private void SpawnBalls(){
        timer += Time.deltaTime;

        if (lowSpawnSpeed < -0.2f && HighSpawnSpeed > 0.2f)
        {
            // the idea is to increase spawn rate as the game goes on. Fix?
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
            if (randomBall < 70)
                Instantiate(ballOne, position, rotation, GetBallHolderTransform());
            else if (randomBall < 90)
                Instantiate(balltwo, position, rotation, GetBallHolderTransform());
            else
                Instantiate(ballThree, position, rotation, GetBallHolderTransform());

            waitingTime = Random.Range(lowSpawnSpeed, HighSpawnSpeed);
            timer = 0;
        }
    }

    public void DestroyPreviousBalls() {
        Destroy(ballHolder);
        ballHolder = new GameObject("GameBallHolder");
    }

    public Transform GetBallHolderTransform()
    {
        return ballHolder.transform;
    }
}
