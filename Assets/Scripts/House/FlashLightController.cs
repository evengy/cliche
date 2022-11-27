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
        // Use this for initialization
        void Start()
        {
            interactions = GetComponent<UIInteractions>();
        }

        // Update is called once per frame
        void Update()
        {
            if (interactions.IsInteractive && Input.GetKeyDown(KeyCode.E))
            {
                ProtagonistUIController.Instance.AddToChat(dialog, PositionState.Left);
            }
        }
    }
}