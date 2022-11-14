using Assets.Scripts.Game;
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
    [SerializeField] TriggerOnProtagonist grabTrigger;
    [SerializeField] TriggerOnProtagonist awakeTrigger;

    bool awake;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>(); // TODO refactor
        interactions = GetComponent<TVInteractions>();
        state = TVState.Idle;
    }
    void UpdateState()
    {
        if (!awake && awakeTrigger.Triggered)
        {
            state = TVState.Awake;
            awake = true;
        }
        if (grabTrigger.Triggered)
        {
            GameManager.Instance.State = GameState.GameOver;
        }
        if (!state.Equals(TVState.Haunted) && animator.GetCurrentAnimatorStateInfo(0).IsName("Haunted_TV_Crawl"))
        {
            state = TVState.Haunted;
        }
    }

    void FollowProtagonist()
    {
        if (GameManager.Instance.State.Equals(GameState.GameComplete)) return; 
        if (GameManager.Instance.State.Equals(GameState.GameOver)) return; 

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
                    GameManager.Instance.State = GameState.GameComplete;
                }
                else
                {
                    state = TVState.On;
                    screen.SetActive(true);
                }
            }
            else if (GetComponent<UIInteractions>().IsInteractive)
            {
                Debug.Log("Need to find a TV Switch"); // Show on UI
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
            animator.ResetTrigger("Grab");

            animator.SetTrigger("Idle");
        }
        if (state.Equals(TVState.Haunted))
        {
            idleHands.SetActive(false);
            hauntedHands.SetActive(true);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Stop");
            animator.ResetTrigger("Grab");

            animator.SetTrigger("Haunted");
        }
        if (state.Equals(TVState.Off))
        {
            idleHands.SetActive(false);
            hauntedHands.SetActive(false);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Haunted");
            animator.ResetTrigger("Grab");

            animator.SetTrigger("Stop");
        }
        if (state.Equals(TVState.Awake))
        {
            idleHands.SetActive(false);
            hauntedHands.SetActive(false);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Stop");
            animator.ResetTrigger("Haunted");
            animator.ResetTrigger("Grab");

            animator.SetTrigger("Awake");
        }

        if (GameManager.Instance.State.Equals(GameState.GameOver)) // grab changed the gamestate -> game ended before TV was switched off -> game lost
        {
            idleHands.SetActive(false);
            hauntedHands.SetActive(true);

            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Stop");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Haunted");

            animator.SetTrigger("Grab");
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        Switch();
        Animate();
        if (state.Equals(TVState.Haunted)) FollowProtagonist();
    }
}
