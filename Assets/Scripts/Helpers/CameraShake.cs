using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class CameraShake : MonoBehaviour
    {
        public IEnumerator Shake(float shakeTime, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;

            float elapsed = 0f;

            while (elapsed < shakeTime)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = originalPos;
        }
    }
}