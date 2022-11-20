using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Ghost
{
    public class GhostController : MonoBehaviour
    {
        [SerializeField] SwappableObject[] swappables;

        // Use this for initialization
        void Start()
        {

        }

        void Swap(SwappableObject[] swappables)
        {
            var indexer = new int[swappables.Length];
            var positions = new Vector3[swappables.Length];
            var rotations = new Quaternion[swappables.Length];

            for (int i = 0; i < swappables.Length; i++)
            {
                positions[i] = new Vector3(
                    swappables[i].transform.localPosition.x,
                    swappables[i].transform.localPosition.y,
                    swappables[i].transform.localPosition.z
                    );
                rotations[i] = new Quaternion(
                    swappables[i].transform.rotation.x,
                    swappables[i].transform.rotation.y,
                    swappables[i].transform.rotation.z,
                    swappables[i].transform.rotation.w);



                if (i + 1 < swappables.Length)
                {
                    indexer[i] = i+1;
                }
                else
                {
                    indexer[i] = 0;
                }
                
            }

            for (int i = 0; i < swappables.Length; i++)
            {
                float y = swappables[i].gameObject.transform.localPosition.y; // save current Y

                swappables[i].gameObject.transform.localPosition = new Vector3(positions[indexer[i]].x, y, positions[indexer[i]].z);
                
                swappables[i].gameObject.transform.rotation = rotations[indexer[i]];
            }

        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    Swap(swappables);
            //    //if (swappables.Where(s => s.CanBeSwapped).Count() > 1)
            //    //{
            //    //    Swap(swappables.Where(s => s.CanBeSwapped).ToArray());
            //    //}
            //}
        }
    }
}