using UnityEngine;

namespace Merger
{
    public class Merger : MonoBehaviour
    {
        public bool doNothing;

        [SerializeField]
        private GameObject BallOne;

        [SerializeField]
        private GameObject BallTwo;

        [SerializeField]
        private GameObject BallThree;

        [SerializeField]
        private GameObject BallFour;

        [SerializeField]
        private GameObject BallFive;

        [SerializeField]
        private GameObject BallSix;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == gameObject.tag && gameObject.tag != BallSix.tag)
            {
                 if (doNothing) return; // does nothing

                 collision.gameObject.GetComponent<Merger>().doNothing = true; // sets other game object to do nothing
                 Destroy(collision.gameObject); // destroys other game object

                Vector3 lastPosition = transform.position; //gets current position for instantiation
                Quaternion lastRotation = transform.rotation;
                switch (gameObject.tag)
                {
                    case "BallOne": // @DW would switching the strings to be BallOne.tag be better?
                        Instantiate(BallTwo, lastPosition, lastRotation);
                        break;
                    case "BallTwo":
                        Instantiate(BallThree, lastPosition, lastRotation);
                        break;
                    case "BallThree":
                        Instantiate(BallFour, lastPosition, lastRotation);
                        break;
                    case "BallFour":
                        Instantiate(BallFive, lastPosition, lastRotation);
                        break;
                    case "BallFive":
                        Instantiate(BallSix, lastPosition, lastRotation);
                        break;
                    case "BallSix":
                        //give points
                        //Win
                        break;
                }

                 Destroy(gameObject);
            }
        }
    }
}

