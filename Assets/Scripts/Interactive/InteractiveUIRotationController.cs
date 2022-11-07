using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveUIRotationController : MonoBehaviour
{
    Camera camera;
    Transform current;
    void Start()
    {
        camera = Camera.main;
        current = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
