using System;
using System.Collections.Generic;
using UnityEngine;

namespace Merger
{
    public class Merger : MonoBehaviour
    {
        private bool doNothing;

        private bool mergable;

        private Spawner spawner;

        private PlayManager playManager;

        private AudioSource audioSourceBall;

        [SerializeField] 
        private AudioClip mergeSound;

        [SerializeField]
        private GameObject ballTwo;

        [SerializeField]
        private GameObject ballThree;

        [SerializeField]
        private GameObject ballFour;

        [SerializeField]
        private GameObject ballFive;

        [SerializeField]
        private GameObject ballSix;

        [SerializeField]
        private GameObject ballSeven;

        private void Start()
        {
            spawner = FindAnyObjectByType<Spawner>();
            playManager = FindAnyObjectByType<PlayManager>();
            audioSourceBall = gameObject.AddComponent<AudioSource>();

            audioSourceBall.clip = mergeSound;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "MergeZone" && !playManager.GetGameStatus())
            {
                mergable = true;
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == gameObject.tag && gameObject.tag != ballSeven.tag && mergable)
            {
                 if (doNothing) return;

                 collision.gameObject.GetComponent<Merger>().doNothing = true; // sets other game object to do nothing
                 Destroy(collision.gameObject); // destroys other game object

                Vector3 lastPosition = transform.position; //gets current position for instantiation
                Quaternion lastRotation = transform.rotation;
                switch (gameObject.tag)
                {
                    case "BallOne":
                        Instantiate(ballTwo, lastPosition, lastRotation, spawner.GetBallHolderTransform());
                        playManager.AddToMatchScore(1);
                        break;
                    case "BallTwo":
                        Instantiate(ballThree, lastPosition, lastRotation, spawner.GetBallHolderTransform());
                        playManager.AddToMatchScore(2);
                        break;
                    case "BallThree":
                        Instantiate(ballFour, lastPosition, lastRotation, spawner.GetBallHolderTransform());
                        playManager.AddToMatchScore(4);
                        break;
                    case "BallFour":
                        Instantiate(ballFive, lastPosition, lastRotation, spawner.GetBallHolderTransform());
                        playManager.AddToMatchScore(8);
                        break;
                    case "BallFive":
                        Instantiate(ballSix, lastPosition, lastRotation, spawner.GetBallHolderTransform());
                        playManager.AddToMatchScore(16);
                        break;
                    case "BallSix":
                        Instantiate(ballSeven, lastPosition, lastRotation, spawner.GetBallHolderTransform());
                        playManager.AddToMatchScore(64);
                        playManager.WinLose();
                        break;
                }

                audioSourceBall.Play();

                // destroy otherwise extra ball spawns
                Destroy(gameObject);
                mergable = false;
            }
        }
    }
}

