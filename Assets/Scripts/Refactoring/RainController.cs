using Assets.Scripts.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.House
{
    public class RainController : MonoBehaviour
    {
        [SerializeField] AudioSource sourceRain;
        [SerializeField] AudioSource sourceThunder;

        [SerializeField] AudioClip rain;
        [SerializeField] AudioClip[] thunder;

        float thunderTimer;
        [SerializeField] float thunderPeriodMin = 10f;
        [SerializeField] float thunderPeriodMax = 20f;

        private float currentThunderPeriodMin;
        private float currentThunderPeriodMax;

        float thunderBoltPeriod;
        float thunderBoltTimer;
        void Start()
        {
            thunderTimer = 0;
            sourceRain.clip = rain;
            sourceRain.loop = true;

            currentThunderPeriodMin = thunderPeriodMin;
            currentThunderPeriodMax = thunderPeriodMax;
        }
        void ThunderBolt()
        {
            if (thunderBoltTimer > thunderBoltPeriod)
            {
                thunderBoltTimer = 0;
                thunderBoltPeriod = Random.Range(0, 0.2f);
                RenderSettings.ambientMode = (RenderSettings.ambientMode.Equals(AmbientMode.Skybox)) ? AmbientMode.Trilight : AmbientMode.Skybox;
            }
            thunderBoltTimer += Time.deltaTime;
        }
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
                    sourceThunder.loop = false;
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

            if (sourceThunder.isPlaying && GameManager.Instance.State.Equals(GameState.Challenge))
            {
                ThunderBolt();
            }
            else if (!RenderSettings.ambientMode.Equals(AmbientMode.Trilight))
            {
                RenderSettings.ambientMode = AmbientMode.Trilight;
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {
                thunderPeriodMin = currentThunderPeriodMin / 2f;
                thunderPeriodMax = currentThunderPeriodMax / 2f;
            }
            else
            {
                thunderPeriodMin = currentThunderPeriodMin;
                thunderPeriodMax = currentThunderPeriodMax;
            }
        }
    }
}