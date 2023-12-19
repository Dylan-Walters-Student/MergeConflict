using System;
using UnityEngine;

namespace Merger
{
    public class Merger : MonoBehaviour
    {
        public bool doNothing;

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

        private bool mergable;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "MergeZone")
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
                        Instantiate(ballTwo, lastPosition, lastRotation);
                        break;
                    case "BallTwo":
                        Instantiate(ballThree, lastPosition, lastRotation);
                        break;
                    case "BallThree":
                        Instantiate(ballFour, lastPosition, lastRotation);
                        break;
                    case "BallFour":
                        Instantiate(ballFive, lastPosition, lastRotation);
                        break;
                    case "BallFive":
                        Instantiate(ballSix, lastPosition, lastRotation);
                        break;
                    case "BallSix":
                        Instantiate(ballSeven, lastPosition, lastRotation);
                        break;
                    case "BallSeven":
                        Debug.Log("Win");
                        break;
                }

                Destroy(gameObject);
                mergable = false;
            }
        }
    }
}

