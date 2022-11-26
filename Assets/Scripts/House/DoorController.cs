using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.House
{
    public class DoorController : MonoBehaviour
    {

        [SerializeField] float openSpeed = 360f;
        [SerializeField] float openAngle = 80f;
        [SerializeField] public GameObject hinge;
        [SerializeField] PositionState positionState = PositionState.Left;
        Quaternion doorRotation;
        Quaternion originRotation;
        Vector3 originPosition;
        bool isOpened;
        AudioSource source;
        [SerializeField] AudioClip doorSound;
        // Start is called before the first frame update
        void Start()
        {
            source = gameObject.AddComponent<AudioSource>();
            source.loop = false;
            source.clip = doorSound;
            originPosition = transform.localPosition;
            originRotation = transform.localRotation;

            if (positionState.Equals(PositionState.Left))
            {
                doorRotation = Quaternion.Euler(0, openAngle, 0);
            }
            else
            {
                doorRotation = Quaternion.Euler(0, -openAngle, 0);

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!isOpened && GetComponent<UIInteractions>().IsInteractive && Input.GetMouseButtonDown((int)MouseButton.Left))  // TODO work on criteria
            {
                transform.RotateAround(hinge.transform.position, Vector3.up, openAngle);
                source.Play();
                isOpened = true;
            }
            else if (isOpened && GetComponent<UIInteractions>().IsInteractive && Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                transform.localPosition = originPosition;
                transform.localRotation = originRotation;
                source.Play();
                isOpened = false;
            }
        }
    }
}