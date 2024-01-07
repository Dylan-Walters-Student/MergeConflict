using System;
using UnityEngine;

namespace Merger
{
    public class Merger : MonoBehaviour
    {
        public bool doNothing;

        private bool mergable;

        private PlayManager playManager;

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
            playManager = FindAnyObjectByType<PlayManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(playManager.GetGameStatus());
            if (other.tag == "MergeZone" && !playManager.GetGameStatus())
            {
                mergable = true;
            }
            Debug.Log(mergable);
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
                        Instantiate(ballTwo, lastPosition, lastRotation);
                        playManager.AddToMatchScore(1);
                        break;
                    case "BallTwo":
                        Instantiate(ballThree, lastPosition, lastRotation);
                        playManager.AddToMatchScore(2);
                        break;
                    case "BallThree":
                        Instantiate(ballFour, lastPosition, lastRotation);
                        playManager.AddToMatchScore(4);
                        break;
                    case "BallFour":
                        Instantiate(ballFive, lastPosition, lastRotation);
                        playManager.AddToMatchScore(8);
                        break;
                    case "BallFive":
                        Instantiate(ballSix, lastPosition, lastRotation);
                        playManager.AddToMatchScore(16);
                        break;
                    case "BallSix":
                        Instantiate(ballSeven, lastPosition, lastRotation);
                        playManager.AddToMatchScore(32);
                        break;
                    case "BallSeven":
                        playManager.AddToMatchScore(64);
                        playManager.WinLose();
                        break;
                }

                Destroy(gameObject);
                mergable = false;
            }
        }
    }
}

