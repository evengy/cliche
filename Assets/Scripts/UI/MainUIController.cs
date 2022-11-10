using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainUIController : Singleton<MainUIController>
    {
        public UIOptionState State { get; private set; }

        Material FirstOptionMessage => option1.GetComponent<Image>().material;
        Material SecondOptionMessage => option2.GetComponent<Image>().material;

        [SerializeField] Button option1; // start by default
        [SerializeField] Button option2; // TODO

        [SerializeField] Material firstMotivationMessage;
        [SerializeField] Material secondMotivationMessage;
        [SerializeField] Material motivationResponse;
        // Start is called before the first frame update
        void Start()
        {
            Button btn1 = option1.GetComponent<Button>();
            Button btn2 = option2.GetComponent<Button>();
            btn1.onClick.AddListener(FirstOption);
            btn2.onClick.AddListener(SecondOption);
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
                option2.gameObject.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Start))
            {
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.Motivation))
            {
                option1.GetComponent<Image>().material = firstMotivationMessage;
                option2.GetComponent<Image>().material = secondMotivationMessage;

                option1.gameObject.SetActive(true);
                option2.gameObject.SetActive(true);
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
            }
            if (GameManager.Instance.State.Equals(GameState.End))
            {
                option1.gameObject.SetActive(false);
                option2.gameObject.SetActive(false);
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

        void FirstMotivation()
        {

        }

        void SecondMotivation()
        {
            State = UIOptionState.Option2;
            Debug.Log($"{State}");
            ProtagonistUIController.Instance.AddToChat(SecondOptionMessage);
            GameManager.Instance.State = GameState.Challenge;
        }
    }
}