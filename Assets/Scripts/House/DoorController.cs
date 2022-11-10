using System.Collections;
using UnityEngine;

namespace Assets.Scripts.House
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] float openSpeed = 10f;
        [SerializeField] GameObject hinge;
        [SerializeField] DoorRotationType door = DoorRotationType.Left;
        Vector3 originPosition;
        Quaternion originRotation;
        // Start is called before the first frame update
        void Start()
        {
            if (hinge == null)
            {
                Debug.Log("hinge is missing");
            }
            originPosition = this.transform.position;
            originRotation = this.transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {

            if (door.Equals(DoorRotationType.Left))
            {
                if (Input.GetKey(KeyCode.O) && this.gameObject.transform.rotation.y < 0.65)
                {
                    this.transform.RotateAround(hinge.transform.position, Vector3.up, openSpeed * Time.deltaTime);
                }
            }
            if (door.Equals(DoorRotationType.Right))
            {
                if (Input.GetKey(KeyCode.O))
                {
                    this.transform.RotateAround(hinge.transform.position, Vector3.up, -openSpeed * Time.deltaTime);
                }
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                this.transform.position = originPosition;
                this.transform.rotation = originRotation;
            }
        }
    }
}