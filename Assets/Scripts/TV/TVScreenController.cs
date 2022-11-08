using Assets.Scripts.Interactive;
using Assets.Scripts.Protagonist;
using Assets.Scripts.TV;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVScreenController : MonoBehaviour
{
    [SerializeField] GameObject screen;
    TVState state;
    TVInteractions interactions;
    // Start is called before the first frame update
    void Start()
    {
        interactions = GetComponent<TVInteractions>();
        state = TVState.On;
    }

    void Switch()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (interactions.IsInteractive)
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
            else if (GetComponent<UIInteractions>().IsInteractive)
            {
                Debug.Log("Need to find a TV Switch");
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        Switch();
    }
}
