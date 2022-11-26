using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
using Assets.Scripts.TV;
using Assets.Scripts.UI;
using UnityEngine;

public class TVScreenController : MonoBehaviour
{
    [SerializeField] GameObject screen;

    TVState state;
    Animator animator;
    TVInteractions interactions;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float maxMotivationRepeat = 15f;
    [SerializeField] float minMotivationRepeat = 5f;
    [SerializeField] GameObject protagonist;
    [SerializeField] GameObject idleHands;
    [SerializeField] GameObject hauntedHands;
    [SerializeField] TriggerOnProtagonist grabTrigger;
    [SerializeField] TriggerOnProtagonist awakeTrigger;

    [SerializeField] AudioClip[] motivationSounds;
    [SerializeField] Material motivationMessage; 
    [SerializeField] Material hintMessage;
    [SerializeField] Material batteryLowMessage;

    [SerializeField] AudioClip dropSound;
    [SerializeField] AudioClip crawlSound;
    AudioSource TVsource;
    Rigidbody rb;
    bool awake;

    [SerializeField] AudioSource motivationSoundSource;

    float timer;
    float repeat;
    // Start is called before the first frame update
    void Start()
    {
        TVsource = GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>(); // TODO refactor
        interactions = GetComponent<TVInteractions>();
        state = TVState.Idle;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        //source = gameObject.AddComponent<AudioSource>();
        repeat = Random.Range(minMotivationRepeat,maxMotivationRepeat);
    }

    void Motivation() // TODO refactor
    {
        if (GameManager.Instance.State.Equals(GameState.Menu))
        {
            timer = 0; // timer starts only when game starts
        }

        if ((GameManager.Instance.State.Equals(GameState.Start) || GameManager.Instance.State.Equals(GameState.Continue)) && timer > repeat)
        {
            //GameManager.Instance.State = GameState.Motivation;
            timer = 0;
            motivationSoundSource.loop = false;
            motivationSoundSource.clip = motivationSounds[Random.Range(0,motivationSounds.Length)]; 
            motivationSoundSource.Play();
            ProtagonistUIController.Instance.AddToChat(motivationMessage, PositionState.Left); // TODO random
            repeat = Random.Range(minMotivationRepeat, maxMotivationRepeat);
        }
        timer += Time.deltaTime;
    }
    void UpdateState()
    {
        if (!awake && awakeTrigger.Triggered)
        {
            state = TVState.Awake;
            TVsource.clip = dropSound; TVsource.Play();
            GameManager.Instance.State = GameState.Wait;
            awake = true;
        }
        if (grabTrigger.Triggered)
        {
            GameManager.Instance.State = GameState.GameOver;
        }
        if (!state.Equals(TVState.Haunted) && animator.GetCurrentAnimatorStateInfo(0).IsName("Haunted_TV_Crawl"))
        {
            state = TVState.Haunted;
            TVsource.clip = crawlSound; TVsource.Play();
            GameManager.Instance.State = GameState.Challenge;
            ProtagonistUIController.Instance.AddToChat(hintMessage, PositionState.Left);
            rb.isKinematic = false;
        }
    }

    void FollowProtagonist()
    {

        if (GameManager.Instance.State.Equals(GameState.GameCompleted)) return;
        if (GameManager.Instance.State.Equals(GameState.GameOver)) return;

        var from = transform.position;
        var towards = new Vector3(protagonist.transform.position.x, from.y, protagonist.transform.position.z);

        var direction = (-(towards - from)).normalized;
        var rotation = Quaternion.LookRotation(direction);

        //transform.position = Vector3.MoveTowards(transform.position, towards, movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        // if stuck ->  rb.AddForce(// random Vector 3, (ForceMode)ForceMode2D.Impulse);
    }

    void Switch()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (interactions.IsInteractive)
            {
                if (!state.Equals(TVState.Off))
                {
                    // TODO add low battery to the chatter
                    //ProtagonistUIController.Instance.AddToChat(batteryLowMessage, PositionState.Left); // placeholder
                    state = TVState.Off;
                    screen.SetActive(false);
                    GameManager.Instance.State = GameState.GameCompleted;
                }
                //else
                //{
                //    state = TVState.On;
                //    screen.SetActive(true);
                //}
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
        Motivation();
        UpdateState();
        Switch();
        Animate();
        if (state.Equals(TVState.Haunted)) FollowProtagonist();
    }
}
