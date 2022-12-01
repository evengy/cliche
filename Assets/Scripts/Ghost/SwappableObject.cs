using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class SwappableObject : MonoBehaviour
    {
        public bool CanBeSwapped { get; private set; }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("GhostView"))
            {
                CanBeSwapped = false;
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals("GhostView"))
            {
                CanBeSwapped = true;
            }
        }
    }
}