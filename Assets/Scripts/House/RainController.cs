using Assets.Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.House
{
    public class RainController : MonoBehaviour
    {
        [SerializeField] AudioSource sourceRain;
        [SerializeField] AudioSource sourceThunder;

        [SerializeField] AudioClip rain;
        [SerializeField] AudioClip[] thunder;

        float thunderTimer;
        [SerializeField] float thunderPeriodMin = 5f;
        [SerializeField] float thunderPeriodMax = 10f;
        // Use this for initialization
        void Start()
        {
            thunderTimer = 0;
            sourceRain.clip = rain;
            sourceRain.loop= true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.Instance.State.Equals(GameState.Menu))
            {
                if (!sourceRain.isPlaying 
                    && !GameManager.Instance.State.Equals(GameState.Challenge)
                    && !GameManager.Instance.State.Equals(GameState.Wait)) sourceRain.Play();
                if (thunderTimer > thunderPeriodMax)
                {
                    thunderTimer = 0;
                    sourceThunder.clip = thunder[Random.Range(0, thunder.Length)];
                    sourceThunder.loop= false;
                    if (!sourceThunder.isPlaying) sourceThunder.Play();
                    thunderPeriodMax = Random.Range(thunderPeriodMin, thunderPeriodMax);
                }
                thunderTimer += Time.deltaTime;
            }
            
            if (GameManager.Instance.State.Equals(GameState.Menu) 
                || sourceRain.isPlaying
                    && (GameManager.Instance.State.Equals(GameState.Challenge)
                    || GameManager.Instance.State.Equals(GameState.Wait)))
            {
                sourceRain.Stop();
            }
        }
    }
}