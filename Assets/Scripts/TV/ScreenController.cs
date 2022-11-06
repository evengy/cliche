using Assets.Scripts.Protagonist;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] GameObject screen;
    TVState state;
    // Start is called before the first frame update
    void Start()
    {
        state = TVState.On;
    }

    void Switch()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (state.Equals(TVState.On))
            {
                state = TVState.Off;
                screen.SetActive(false);
            }
            else
            {
                state = TVState.On;
                screen.SetActive(true);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        Switch();
    }
}
