using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    public class CreditsManager : Singleton<CreditsManager>
    {
        [SerializeField] Material[] credits;
        int creditIndex;
        [SerializeField] GameObject titleLayout;
        [SerializeField] GameObject creditsLayout;
        // Use this for initialization
        [SerializeField] float creditTimer = 10f;
        float currentTime = 10;
        public bool isActive { get; set; }

        Animator titleAnimatior;
        void Start()
        {
            titleAnimatior = titleLayout.GetComponent<Animator>();

        }

        public void ShowCredits(int i)
        {
            creditsLayout.SetActive(true);
            titleLayout.GetComponent<Image>().material = credits[i];
            titleAnimatior.SetTrigger("Awake");
        }

        // Update is called once per frame
        void Update()
        {
            if (isActive)
            {
                currentTime += Time.deltaTime;

                if (currentTime >= creditTimer)
                {
                    ShowCredits(creditIndex);
                    currentTime = 0;
                    creditIndex++;
                    if (creditIndex > credits.Length)
                    {
                        creditIndex = 0;
                        isActive = false;
                    }
                }
            }
            if (currentTime >= 4f)
            {
                creditsLayout.SetActive(false);
            }
        }
    }
}