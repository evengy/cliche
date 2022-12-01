using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveUIController : MonoBehaviour
{
    // TODO add sounds
    //bool activated;
    void Update()
    {
        //if (!activated && gameObject.activeSelf)
        //{
        //    activated= true;
        //}
        //if (!gameObject.activeSelf)
        //{
        //    activated= false;
        //}
        transform.rotation = Camera.main.transform.rotation;
    }
}
