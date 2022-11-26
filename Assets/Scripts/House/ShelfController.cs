using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace Assets.Scripts.House
{
    public class ShelfController : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 2f;
        [SerializeField] float shelfSize = 0.8f;
        bool isOpened;
        UIInteractions interactions;
        AudioSource source;
        [SerializeField] AudioClip sound;
        private void Start()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.loop = false;
            source.clip = sound;
            interactions = GetComponent<UIInteractions>();
            if (interactions != null)
            {
                return;
            }
            interactions = GetComponentInParent<UIInteractions>();
        }
        void OpenShelf(GameObject shelf)
        {
            var current = shelf.transform.localPosition;
            var towards = new Vector3(current.x, current.y, current.z - shelfSize);
            if (current.z > -shelfSize)
            {
                shelf.transform.localPosition = Vector3.MoveTowards(current, towards, Time.deltaTime * movementSpeed);
            }
            var pin = shelf.GetComponentInChildren<Pin>();
            if (pin != null)
            {
                if (!pin.Opened)
                {
                    Debug.Log($"{pin.name} Opened");
                }
                pin.Opened = true;
            }
        }

        void CloseShelf(GameObject shelf)
        {
            var current = shelf.transform.localPosition;
            var towards = new Vector3(current.x, current.y, 0);
            if (current.z <= 0)
            {
                shelf.transform.localPosition = Vector3.MoveTowards(current, towards, Time.deltaTime * movementSpeed);
            }
        }

        void ShelfUpdate()
        {
            if (!isOpened && interactions.IsInteractive && Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                source.Play();
                isOpened = true;
            }
            else if (isOpened && interactions.IsInteractive && Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                source.Play();
                isOpened = false;
            }

            if (isOpened)
            {
                OpenShelf(gameObject);
            }
            else
            {
                CloseShelf(gameObject);
            }
        }

        private void Update()
        {
            ShelfUpdate();
        }
    }
}