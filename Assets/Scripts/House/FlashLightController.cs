using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.House
{
    public class FlashLightController : MonoBehaviour
    {
        [SerializeField] Material dialog;
        UIInteractions interactions;
        void Start()
        {
            interactions = GetComponent<UIInteractions>();
        }
        void Update()
        {
            if (interactions.IsInteractive && Input.GetKeyDown(KeyCode.E))
            {
                ProtagonistUIController.Instance.AddToChat(dialog, PositionState.Left);
            }
        }
    }
}