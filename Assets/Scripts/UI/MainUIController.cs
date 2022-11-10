using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
        [SerializeField] GameObject layout;


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
                //button2.gameObject.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Start))
            {
                options.SetActive(false);
                layout.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Motivation))
            {
                button1.GetComponent<Image>().material = firstMotivationMessage;
                button2.GetComponent<Image>().material = secondMotivationMessage;

                options.SetActive(true);
                layout.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {
                options.SetActive(false);
                layout.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.End))
            {
                options.SetActive(false);
                layout.SetActive(false);
            }
        }

        void FirstOption()
        {
            Debug.Log("option 1");
            if (GameManager.Instance.State.Equals(GameState.Menu))
            {
                GameManager.Instance.State = GameState.Start;
            }
            if (GameManager.Instance.State.Equals(GameState.Start))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.Motivation))
            {
                State = UIOptionState.Option1;
                Debug.Log($"{State}");
                ProtagonistUIController.Instance.AddToChat(FirstOptionMessage);
                ProtagonistUIController.Instance.AddToChat(motivationResponse);
                GameManager.Instance.State = GameState.Challenge;
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.End))
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
                ProtagonistUIController.Instance.AddToChat(SecondOptionMessage);
                ProtagonistUIController.Instance.AddToChat(motivationResponse);
                GameManager.Instance.State = GameState.Challenge;
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {

            }
            if (GameManager.Instance.State.Equals(GameState.End))
            {

            }
        }
    }
}