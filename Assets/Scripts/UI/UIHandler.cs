using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class UIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
            //source.Stop();
            Debug.Log("hover is over");
        }

        // Use this for initialization
        void Start()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.loop = false;
            source.clip = buttonSelected;
            source.volume = volume;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}