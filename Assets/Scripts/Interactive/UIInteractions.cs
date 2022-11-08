using Assets.Scripts.Interactive;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class UIInteractions : MonoBehaviour
    {
        [SerializeField] GameObject UI;
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
            if (isInteractive)
            {
                UI.SetActive(true);
            }
            else
            {
                UI.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Protagonist"))
            {
                Debug.Log($"Protagonist can Interact with {this.gameObject.name}");
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals("Protagonist"))
            {
                isInteractive = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isInteractive = false;
        }
    }
}