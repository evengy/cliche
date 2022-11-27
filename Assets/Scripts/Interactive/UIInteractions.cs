using Assets.Scripts.Interactive;
using System.Collections;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class UIInteractions : MonoBehaviour
    {
        [SerializeField] GameObject UI;
        [SerializeField] GameObject highlight;
        [SerializeField] bool highlightable = true;
        int layerMask;

        private bool isInReach;
        private bool isHighlighted;
        public bool IsInteractive => isHighlighted || (isInReach && !highlightable);

        // Use this for initialization
        void Start()
        {
            isInReach = false;
            layerMask = 1 << 3;
        }

        void UIUpdate()
        {
            if (isHighlighted || (isInReach && !highlightable))
            {
                UI.SetActive(true);
            }
            else
            {
                UI.SetActive(false);
            }
        }
        private void HighlightUpdate()
        {
            isHighlighted = false;
            highlight.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (isInReach && Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.GetInstanceID().Equals(highlight.gameObject.transform.GetInstanceID()))
                {
                    Debug.Log(hit.transform.GetInstanceID());
                    Debug.Log("hit");
                    isHighlighted = true;
                    highlight.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (highlight != null && highlightable)
            {
                HighlightUpdate();
            }
            if (UI != null)
            {
                UIUpdate();
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
                isInReach = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            isInReach = false;
        }
    }
}