using Assets.Scripts.Helpers;
using Assets.Scripts.UI;
using Cinemachine;
using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] Material motivationMessage;
        public GameState State { get; set; }
        // Use this for initialization
        void Start()
        {
            State = GameState.Menu;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M)) // sound from the room -> chatter -> creates motivation
            {
                ProtagonistUIController.Instance.AddToChat(motivationMessage); // material image
                GameManager.Instance.State = GameState.Motivation;
            }

            if (State.Equals(GameState.Menu) || State.Equals(GameState.Motivation))
            {
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            }
            else if (!Camera.main.GetComponent<CinemachineBrain>().enabled)
            {
                Camera.main.GetComponent<CinemachineBrain>().enabled = true;
            }
        }
    }
}