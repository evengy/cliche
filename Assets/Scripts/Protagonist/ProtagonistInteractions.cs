using Assets.Scripts.Interactive;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Protagonist
{
    public class ProtagonistInteractions : MonoBehaviour
    {
        public PickableObject Pick => pick;
        private PickableObject pick;
        public bool CanJump { get; private set; }
        private bool isInteractive;
        public bool IsInteractive => isInteractive;
        // Use this for initialization
        void Start()
        {
            isInteractive = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("PickableObject"))
            {
                Debug.Log("PickableObject in reach");
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals("PickableObject"))
            {
                isInteractive = true;
                pick = other.gameObject.GetComponent<PickableObject>();
            }

            if (other.tag.Equals("JumpObject"))
            {
                CanJump = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isInteractive = false;
            if (other.tag.Equals("JumpObject"))
            {
                CanJump = false;
            }
        }
    }
}