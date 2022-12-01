using Assets.Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class TriggerOnProtagonist : MonoBehaviour
    {
        public bool Triggered => trigger;
        bool trigger;
       
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