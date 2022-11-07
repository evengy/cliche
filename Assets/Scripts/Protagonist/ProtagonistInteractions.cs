using Assets.Scripts.Interactive;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Protagonist
{
    public class ProtagonistInteractions : MonoBehaviour
    {
        public PickableObject Pick => pick;
        private PickableObject pick;
        private bool interactive;
        public bool Interactive => interactive;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            interactive = true;
            Debug.Log($"{other.tag.Equals("PickableObject")}");
            if (other.tag.Equals("PickableObject"))
            {
                pick = other.gameObject.GetComponent<PickableObject>();
            }
            // TODO request MENU
        }

        private void OnTriggerExit(Collider other)
        {
            interactive = false;
        }
    }
}