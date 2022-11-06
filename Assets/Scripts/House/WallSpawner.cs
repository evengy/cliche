using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] GameObject plank;
    // Start is called before the first frame update
    void Start()
    {
        Transform current = transform;
        for(int i = 0; i < 24; i++)
        {
            var pos = current.position;
            pos.x = i;
            Instantiate(plank, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
