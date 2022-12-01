using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
using Assets.Scripts.UI;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Obsolete
{
    public class LightSwitchController : MonoBehaviour
    {
        [SerializeField] Material dialog;
        UIInteractions interactions;
        void Start()
        {
            interactions= GetComponent<UIInteractions>();   
        }
        void Update()
        {
            if (interactions.IsInteractive && Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                ProtagonistUIController.Instance.AddToChat(dialog, PositionState.Left);
            }
        }
    }
}