using Assets.Scripts.Helpers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] AudioClip librarySound;
        [SerializeField] AudioClip hauntedTVSound;
        [SerializeField] AudioClip creditsSound; // game complete
        [SerializeField] AudioClip menuSound;
        [SerializeField] AudioClip gameOverSound;
        AudioSource audioSource1;
        AudioSource audioSource2;

        bool isSource1;

        [SerializeField] float fadeTime = 2f;
        // Use this for initialization
        void Start()
        {
            audioSource1 = gameObject.AddComponent<AudioSource>();
            audioSource2 = gameObject.AddComponent<AudioSource>();
            isSource1 = true;

            DirectInit(menuSound);
        }

        void Swap(AudioClip audio)
        {
            StopAllCoroutines();
            StartCoroutine(Fade(audio));

            isSource1 = !isSource1;
        }
        void DirectInit(AudioClip audio)
        {
            StopAllCoroutines();
            audioSource1.Stop();
            audioSource2.Stop();
            audioSource1.clip = audio;
            audioSource1.volume = 1f;
            audioSource1.loop = true;
            audioSource1.Play();
        }
        IEnumerator Fade(AudioClip audio)
        {
            float elapsed = 0;
            if (isSource1)
            {
                audioSource2.clip = audio;
                audioSource2.loop = true;

                audioSource2.Play();
                while (elapsed < fadeTime)
                {
                    audioSource1.volume = Mathf.Lerp(1, 0, elapsed / fadeTime);
                    audioSource2.volume = Mathf.Lerp(0, 1, elapsed / fadeTime);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                audioSource1.Stop();
            }
            else
            {
                audioSource1.clip = audio;
                audioSource1.loop = true;
                
                audioSource1.Play();
                while (elapsed < fadeTime)
                {
                    audioSource2.volume = Mathf.Lerp(1, 0, elapsed / fadeTime);
                    audioSource1.volume = Mathf.Lerp(0, 1, elapsed / fadeTime);
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                audioSource2.Stop();
            }
        }
        // Update is called once per frame
        public void UpdateSound()
        {
            switch (GameManager.Instance.State)
            {
                case GameState.Menu:
                    Swap(menuSound);
                    break;
                case GameState.Start:
                    Swap(librarySound);
                    break;
                case GameState.Motivation:
                    Swap(null);
                    break;
                case GameState.Challenge:
                    DirectInit(hauntedTVSound);
                    break;
                case GameState.GameOver:
                    Swap(gameOverSound);
                    break;
                case GameState.GameComplete:
                    Swap(creditsSound);
                    break;
                case GameState.Hold:
                    Swap(null);
                    break;
                default:
                    break;
            }
        }
    }
}