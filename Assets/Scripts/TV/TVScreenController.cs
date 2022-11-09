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
    Animator animator;
    TVInteractions interactions;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] GameObject protagonist;
    [SerializeField] GameObject idleHands;
    [SerializeField] GameObject hauntedHands;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>(); // TODO refactor
        interactions = GetComponent<TVInteractions>();
        state = TVState.Idle;
    }
    void Foo()
    {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    state = TVState.Haunted;
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    state = TVState.Idle;
        //}
        if (Input.GetKeyDown(KeyCode.A))
        {
            state = TVState.Awake;
        }
        if (!state.Equals(TVState.Haunted) && animator.GetCurrentAnimatorStateInfo(0).IsName("Haunted_TV_Crawl"))
        {
            state = TVState.Haunted;
        }
    }

    void FollowProtagonist()
    {
        var from = transform.position;
        var towards = new Vector3(protagonist.transform.position.x, from.y, protagonist.transform.position.z);
        
        var direction = (- (towards - from)).normalized;
        var rotation = Quaternion.LookRotation(direction);
        
        transform.position = Vector3.MoveTowards(transform.position, towards, movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    void Switch()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (interactions.IsInteractive)
            {
                if (!state.Equals(TVState.Off))
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

    void Animate()
    {
        if (state.Equals(TVState.Idle))
        {
            idleHands.SetActive(true);
            hauntedHands.SetActive(false);
            animator.ResetTrigger("Haunted");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Stop");

            animator.SetTrigger("Idle");
        }
        if (state.Equals(TVState.Haunted))
        {
            idleHands.SetActive(false);
            hauntedHands.SetActive(true);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Stop");

            animator.SetTrigger("Haunted");
        }
        if (state.Equals(TVState.Off))
        {
            idleHands.SetActive(false);
            hauntedHands.SetActive(false);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Haunted");

            animator.SetTrigger("Stop");

        }
        if (state.Equals(TVState.Awake))
        {
            idleHands.SetActive(false);
            hauntedHands.SetActive(false);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Stop");
            animator.ResetTrigger("Haunted");

            animator.SetTrigger("Awake");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Foo();
        Switch();
        Animate();
        if (state.Equals(TVState.Haunted)) FollowProtagonist();
    }
}
