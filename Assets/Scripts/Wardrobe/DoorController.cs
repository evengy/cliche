using Assets.Scripts.Protagonist;
using Assets.Scripts.Wardrobe;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class DoorController : MonoBehaviour
{
    [SerializeField] float openSpeed = 10f;
    [SerializeField] GameObject hinge;
    [SerializeField] DoorPosition door = DoorPosition.Left;
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

        if (door.Equals(DoorPosition.Left))
        {
            if (Input.GetKey(KeyCode.L) && this.gameObject.transform.rotation.y < 0.65)
            {
                this.transform.RotateAround(hinge.transform.position, Vector3.up, openSpeed * Time.deltaTime);
            }
        }
        if (door.Equals(DoorPosition.Right))
        {
            if (Input.GetKey(KeyCode.R) && this.gameObject.transform.rotation.y > 0.65)
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
