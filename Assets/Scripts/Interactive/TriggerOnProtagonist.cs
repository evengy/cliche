using Assets.Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class TriggerOnProtagonist : MonoBehaviour
    {
        public bool Triggered => trigger;
        bool trigger;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Protagonist"))
            {
                trigger = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            trigger = false;
        }

    }
}