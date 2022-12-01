using Assets.Scripts.Interactive;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TV
{
    public class TVInteractions : MonoBehaviour
    {
        private bool isInteractive;
        public bool IsInteractive => isInteractive;

        void Start()
        {
            isInteractive = false;
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals("Protagonist"))
            {
                isInteractive = other.gameObject.GetComponent<ProtagonistController>().TVSwitchFound;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            isInteractive = false;
        }
    }
}