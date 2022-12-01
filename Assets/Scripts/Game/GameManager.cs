using Assets.Scripts.Helpers;
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

        [SerializeField] CinemachineFreeLook cinemachine;
        // [SerializeField] GameObject menuView; // TODO
        [SerializeField] GameObject protagonistView;
        [SerializeField] GameObject tvAwakeView;

        [SerializeField] Material whoTrunedOutTheLights;
        // [SerializeField] GameObject gameCompletedView; // TODO
        bool started;
        private GameState state;
        public GameState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    Debug.Log($"state updated to {state}");
                    SoundManager.Instance.UpdateSound();
                }
            }
        } 
        void Start()
        {
            State = GameState.Menu;
            debugLightSource.SetActive(false);
            RenderSettings.ambientIntensity = 0.5f;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
        }
        void Update()
        {
            #region Debug
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    State = GameState.GameCompleted;
            //}

            //if (Input.GetKeyDown(KeyCode.G))
            //{
            //    State = GameState.GameOver;
            //}


            if (Input.GetKeyDown(KeyCode.C))
            {
                State = GameState.Challenge;
            }
            #endregion
            if (GameManager.Instance.State.Equals(GameState.Wait))
            {
                cinemachine.LookAt = tvAwakeView.transform;
                cinemachine.Follow = tvAwakeView.transform;
            }
            if (!State.Equals(GameState.Menu))
            {
                protagonist.SetActive(true);
                dummy.SetActive(false);
                if (!Camera.main.GetComponent<CinemachineBrain>().enabled)
                {
                    Camera.main.GetComponent<CinemachineBrain>().enabled = true;
                }
            }
            if (State.Equals(GameState.Menu)) // || State.Equals(GameState.Motivation))
            {
                protagonist.SetActive(false);
                dummy.SetActive(true);
                Camera.main.GetComponent<CinemachineBrain>().enabled = false;
            }
        }
    }
}