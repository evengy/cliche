using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class PickableObject : MonoBehaviour
    {
        GameObject pin;
        void Start()
        {
        }

        public void Reassign(GameObject newPin)
        {
            this.pin = newPin.gameObject;
        }

        void Foo()
        {
            if (Vector3.Distance(this.transform.position, pin.transform.position) > 0.001f)
            {
                this.transform.position = pin.transform.position;
            }
            this.transform.rotation = pin.transform.rotation;
        }

        void Update()
        {
            Foo();
        }
    }
}