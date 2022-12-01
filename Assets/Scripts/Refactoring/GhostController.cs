using Assets.Scripts.Game;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostController : MonoBehaviour
    {
        // TODO add sounds
        [SerializeField] SwappableObject[] swappables;
        [SerializeField] Light lampLight;
        [SerializeField] AudioClip[] swapSounds;
        float swapTimer;
        float ghostPeriod = 5f;
        float blinkTimer;
        float blinkPeriod = 0.4f;
        float currentDensity = 0;
        float currentIntensity = 0;
        Color currentColor;
        bool lightsOut;
        bool lightsOn;
        AudioSource source;
        void Start()
        {
            swapTimer = 0f;
            source = gameObject.AddComponent<AudioSource>();
            source.loop = false;
        }
        void Blink()
        {
            blinkPeriod = Random.Range(0, 0.4f);
            lampLight.enabled = !lampLight.enabled;
        }
        void Swap(SwappableObject[] swappables)
        {
            var indexer = new int[swappables.Length];
            var positions = new Vector3[swappables.Length];
            var rotations = new Quaternion[swappables.Length];

            for (int i = 0; i < swappables.Length; i++)
            {
                positions[i] = new Vector3(
                    swappables[i].transform.localPosition.x,
                    swappables[i].transform.localPosition.y,
                    swappables[i].transform.localPosition.z
                    );
                rotations[i] = new Quaternion(
                    swappables[i].transform.rotation.x,
                    swappables[i].transform.rotation.y,
                    swappables[i].transform.rotation.z,
                    swappables[i].transform.rotation.w);

                if (i + 1 < swappables.Length)
                {
                    indexer[i] = i + 1;
                }
                else
                {
                    indexer[i] = 0;
                }

            }

            for (int i = 0; i < swappables.Length; i++)
            {
                float y = swappables[i].gameObject.transform.localPosition.y; // saves current Y

                swappables[i].gameObject.transform.localPosition = new Vector3(positions[indexer[i]].x, y, positions[indexer[i]].z);

                swappables[i].gameObject.transform.rotation = rotations[indexer[i]];
            }

        }
        void LightsOut()
        {
            if (lightsOut) return;
            currentDensity = RenderSettings.fogDensity;
            currentIntensity = RenderSettings.ambientIntensity;
            currentColor = RenderSettings.fogColor;
            RenderSettings.ambientIntensity = 0.5f;
            RenderSettings.fogDensity = 0.1f;
            RenderSettings.fogColor = Color.black;
            lightsOut = true;
            lightsOn = false;
        }
        void LightsOn()
        {
            if (lightsOn) return;
            RenderSettings.ambientIntensity = currentIntensity;
            RenderSettings.fogDensity = currentDensity;
            RenderSettings.fogColor = currentColor;
            lightsOn = true;
            lightsOut = false;
        }
        void Update()
        {
            if (GameManager.Instance.State.Equals(GameState.Challenge) && swapTimer > ghostPeriod)
            {
                swapTimer = 0f;
                //Swap(swappables);
                if (swappables.Where(s => s.CanBeSwapped).Count() > 1)
                {
                    Swap(swappables.Where(s => s.CanBeSwapped).ToArray());
                    if (!source.isPlaying)
                    {
                        source.clip = swapSounds[Random.Range(0, swapSounds.Length)];
                        source.Play();
                    }
                }
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge) && blinkTimer > blinkPeriod)
            {
                blinkTimer = 0f;
                Blink();
            }
            if (GameManager.Instance.State.Equals(GameState.Challenge))
            {
                swapTimer += Time.deltaTime;
                blinkTimer += Time.deltaTime;
                LightsOut();
            }
            if (GameManager.Instance.State.Equals(GameState.GameCompleted))
            {
                LightsOn();
            }

        }
    }
}