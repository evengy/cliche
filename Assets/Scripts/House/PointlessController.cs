using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
using Assets.Scripts.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.House
{
    public class PointlessController : MonoBehaviour
    {

        [SerializeField] Material dialog;
        [SerializeField] bool clickable;
        UIInteractions interactions;
        void Start()
        {
            interactions = GetComponent<UIInteractions>();
        }
        void Update()
        {
            if (clickable)
            {
                if (interactions.IsInteractive && Input.GetMouseButtonDown((int)MouseButton.Left))
                {
                    ProtagonistUIController.Instance.AddToChat(dialog, PositionState.Left);
                }
            }
            else
            {
                if (interactions.IsInteractive && Input.GetKeyDown(KeyCode.E))
                {
                    ProtagonistUIController.Instance.AddToChat(dialog, PositionState.Left);
                }
            }
        }
    }
}