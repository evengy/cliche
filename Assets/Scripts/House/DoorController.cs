using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
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
        // Start is called before the first frame update
        void Start()
        {
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
            if (GetComponent<UIInteractions>().IsInteractive && Input.GetKey(KeyCode.D) && transform.rotation.eulerAngles.y < doorRotation.eulerAngles.y)  // TODO work on criteria
            {
                transform.RotateAround(hinge.transform.position, Vector3.up, Time.deltaTime * openSpeed);
            }
            if (GetComponent<UIInteractions>().IsInteractive &&  Input.GetKey(KeyCode.C))
            {
                transform.localPosition = originPosition;
                transform.localRotation = originRotation;
            }
        }
    }
}