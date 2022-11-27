using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
using Assets.Scripts.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.House
{
    public class TelephoneController : MonoBehaviour
    {
        [SerializeField] Material dialog;
        UIInteractions interactions;
        // Use this for initialization
        void Start()
        {
            interactions = GetComponent<UIInteractions>();
        }

        // Update is called once per frame
        void Update()
        {
            if (interactions.IsInteractive && Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                ProtagonistUIController.Instance.AddToChat(dialog, PositionState.Left);
            }
        }
    }
}