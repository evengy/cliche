using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interactive
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] GameObject assign;
        // Use this for initialization
        void Start()
        {
        }

        public void Reassign(GameObject assign)
        {
            this.assign = assign.gameObject;
        }

        void Foo()
        {
            if (Vector3.Distance(this.transform.position, assign.transform.position) > 0.001f)
            {
                this.transform.position = assign.transform.position;
            }
            this.transform.rotation = assign.transform.rotation;
        }

        // Update is called once per frame
        void Update()
        {
            Foo();
        }
    }
}