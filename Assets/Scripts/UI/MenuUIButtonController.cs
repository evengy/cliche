using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class MenuUIButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] AudioClip buttonSelected;
        AudioSource source;
        [SerializeField] float volume = 0.5f;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!source.isPlaying)
                source.Play();
            Debug.Log("hover over button");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("hover is over");
        }

        void Start()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.loop = false;
            source.clip = buttonSelected;
            source.volume = volume;
        }

    }
}