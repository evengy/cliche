﻿using Assets.Scripts.Helpers;
using Assets.Scripts.UI;
using Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] GameObject protagonist;
        [SerializeField] GameObject dummy;
        [SerializeField] GameObject debugLightSource;
        private GameState state;
        public GameState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    SoundManager.Instance.UpdateSound();
                }
            }
        } // TODO private set and refactor
        // Use this for initialization
        void Start()
        {
            State = GameState.Menu;
            debugLightSource.SetActive(false);
            RenderSettings.ambientIntensity = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            //if (State.Equals(GameState.Motivation)) // Testing. should come from motivator
            //{
            //    ProtagonistUIController.Instance.AddToChat(motivationMessage, PositionState.Left);
            //    //State = GameState.Motivation;
            //}
            if (State.Equals(GameState.Menu))
            {
                protagonist.SetActive(false);
                dummy.SetActive(true);
            }
            if (!State.Equals(GameState.Menu))
            {
                protagonist.SetActive(true);
                dummy.SetActive(false);
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