using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainUIController : Singleton<MainUIController>
    {
        public UIOptionState State { get; private set; }

        Material FirstOptionMessage => button1.GetComponent<Image>().material;
        Material SecondOptionMessage => button2.GetComponent<Image>().material;

        Button button1; // start by default
        Button button2; // TODO

        [SerializeField] GameObject options;
        [SerializeField] GameObject mainLayout;
        [SerializeField] GameObject endgameLayout;

        [SerializeField] Material newGame;
        [SerializeField] Material tryAgain;
        [SerializeField] Material exit;
        [SerializeField] Material gameOver;
        [SerializeField] Material gameComplete;

        [SerializeField] GameObject endgameTitle;

        [SerializeField] Material firstMotivationMessage;
        [SerializeField] Material secondMotivationMessage;
        [SerializeField] Material motivationResponse;

        // Start is called before the first frame update
        void Start()
        {
            button1 = options.GetComponentsInChildren<Button>()[0];
            button2 = options.GetComponentsInChildren<Button>()[1];

            button1.onClick.AddListener(FirstOption);
            button2.onClick.AddListener(SecondOption);
        }

        // Update is called once per frame
        void Update()
        {
            ApplyUI();
        }

        void ApplyUI()
        {

            if (GameManager.Instance.State.Equals(GameState.Menu))
            {
                endgameLayout.SetActive(false);
                //button2.gameObject.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Start))
            {
                endgameLayout.SetActive(false);
                options.SetActive(false);
                mainLayout.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Motivation))
            {
                button1.GetComponent<Image>().material = firstMotivationMessage;
                button2.GetComponent<Image>().material = secondMotivationMessage;

                endgameLayout.SetActive(false);
                options.SetActive(true);
                mainLayout.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {
                endgameLayout.SetActive(false);
                options.SetActive(false);
                mainLayout.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Any))
            {
                endgameLayout.SetActive(false);
                options.SetActive(false);
                mainLayout.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.GameOver))
            {

                button1.GetComponent<Image>().material = tryAgain;
                button2.GetComponent<Image>().material = exit;
                endgameTitle.GetComponent<Image>().material = gameOver;

                options.SetActive(true);
                endgameLayout.SetActive(true);
                mainLayout.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.GameComplete))
            {
                button1.GetComponent<Image>().material = newGame;
                button2.GetComponent<Image>().material = exit; 
                endgameTitle.GetComponent<Image>().material = gameComplete;

                options.SetActive(true);
                endgameLayout.SetActive(true);
                mainLayout.SetActive(false);
            }
        }

        void FirstOption()
        {
            Debug.Log("option 1");
            if (GameManager.Instance.State.Equals(GameState.Menu))
            {
                GameManager.Instance.State = GameState.Any;
            }
            if (GameManager.Instance.State.Equals(GameState.Start))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.Motivation))
            {
                State = UIOptionState.Option1;
                Debug.Log($"{State}");
                //ProtagonistUIController.Instance.AddToChat(FirstOptionMessage, PositionState.Right);
                ProtagonistUIController.Instance.AddToChat(motivationResponse, PositionState.Left);
                GameManager.Instance.State = GameState.Any;
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.GameOver))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.GameComplete))
            {

            }
        }

        void SecondOption()
        {
            Debug.Log("option 2");
            if (GameManager.Instance.State.Equals(GameState.Menu))
            {
                //GameManager.Instance.State = GameState.Start;
            }
            if (GameManager.Instance.State.Equals(GameState.Start))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.Motivation))
            {
                State = UIOptionState.Option1;
                Debug.Log($"{State}");
                //ProtagonistUIController.Instance.AddToChat(SecondOptionMessage, PositionState.Right);
                ProtagonistUIController.Instance.AddToChat(motivationResponse, PositionState.Left);
                GameManager.Instance.State = GameState.Any;
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.GameOver))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.GameComplete))
            {

            }
        }
    }
}