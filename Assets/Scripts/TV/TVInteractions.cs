using Assets.Scripts.Interactive;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TV
{
    public class TVInteractions : MonoBehaviour
    {
        
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