using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using Assets.Scripts.Interactive;
using Assets.Scripts.TV;
using Assets.Scripts.UI;
using UnityEditor.VersionControl;
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
    [SerializeField] GameObject creditsCube;
    [SerializeField] GameObject hauntedHands;
    [SerializeField] TriggerOnProtagonist grabTrigger;
    [SerializeField] TriggerOnProtagonist awakeTrigger;

    [SerializeField] Material[] motivationMessages;
    [SerializeField] Material[] hintMessages;
    [SerializeField] Material lightsOutMessage;
    [SerializeField] Material regretMessage;
    // TODO refactor sounds should be managed outside TV class

    [SerializeField] AudioClip[] motivationSounds;
    [SerializeField] AudioClip[] dropSounds;
    [SerializeField] AudioClip crawlSound;
    AudioSource TVsource;
    Rigidbody rb;
    bool awake;
    [SerializeField] Transform awakeView;

    [SerializeField] AudioSource motivationSoundSource;
    #region Challenge
    [SerializeField] AudioClip[] hauntedSounds; // crawling
    AudioSource hauntAmbientSource;
    [SerializeField] Material[] clicheLines;
    [SerializeField] AudioClip[] clicheSounds;
    [SerializeField] GameObject clicheUI;
    [SerializeField] GameObject chatter;
    GameObject chatterInstance;
    bool isShowing;

    [SerializeField] float messageShowPeriod = 3f;
    #endregion
    float awakeTimer;
    float motivationTimer;
    float motivationPeriod;


    bool isShowingCredits;
    float creditsPeriod = 15f;
    float creditsTimer = 0f;
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
        motivationPeriod = Random.Range(minMotivationRepeat, maxMotivationRepeat);

        hauntAmbientSource = gameObject.AddComponent<AudioSource>();
    }
    void Motivation() // TODO refactor
    {
        if (GameManager.Instance.State.Equals(GameState.Menu))
        {
            motivationTimer = 0; // timer starts only when game starts
        }
        if (!isShowing && GameManager.Instance.State.Equals(GameState.Challenge) && motivationTimer > motivationPeriod)
        {
            isShowing = true;
            clicheUI.SetActive(true);

            motivationTimer = 0;
            motivationSoundSource.loop = false;
            //motivationSoundSource.spatialBlend = 1f;
            motivationSoundSource.clip = clicheSounds[Random.Range(0, clicheSounds.Length)];
            motivationSoundSource.Play();
            chatter.SetActive(true);
            chatterInstance = Instantiate(chatter);
            chatterInstance.transform.SetParent(clicheUI.transform, false);
            chatterInstance.GetComponent<Chatter>().SetMessage = clicheLines[Random.Range(0, clicheLines.Length)];
        }
        if (motivationTimer > messageShowPeriod)
        {
            isShowing = false;
            Destroy(chatterInstance);
            clicheUI.SetActive(false);
        }

        if ((GameManager.Instance.State.Equals(GameState.Start) || GameManager.Instance.State.Equals(GameState.Continue)) && motivationTimer > motivationPeriod)
        {
            //GameManager.Instance.State = GameState.Motivation;
            motivationTimer = 0;
            motivationSoundSource.loop = false;
            //motivationSoundSource.spatialBlend = 0f;
            motivationSoundSource.clip = motivationSounds[Random.Range(0, motivationSounds.Length)];
            motivationSoundSource.Play();
            ProtagonistUIController.Instance.AddToChat(motivationMessages[Random.Range(0, motivationMessages.Length)], PositionState.Left); // TODO random
            motivationPeriod = Random.Range(minMotivationRepeat, maxMotivationRepeat);
        }
        if (interactions.IsInteractive)
        {
            clicheUI.SetActive(false);
        }
        motivationTimer += Time.deltaTime;
    }
    void UpdateState()
    {
        if (state.Equals(TVState.Off))
        {
            GameManager.Instance.State = GameState.GameCompleted;
            hauntAmbientSource.Stop();
            return;
        }
        if (grabTrigger.Triggered)
        {
            GameManager.Instance.State = GameState.GameOver;
            hauntAmbientSource.Stop();
            return;
        }

        if (!awake && awakeTrigger.Triggered)
        {
            awakeTimer = 0;
            state = TVState.Awake;
            CameraViewManager.Instance.AttachViewTo(awakeView);
            GameManager.Instance.State = GameState.Wait;
            awake = true;
        }
        if (state.Equals(TVState.Awake))
        {
            awakeTimer += Time.deltaTime;
            if (!TVsource.isPlaying && awakeTimer > 0.1f && awakeTimer < 0.4f)
            {
                TVsource.clip = dropSounds[0]; TVsource.loop = false; TVsource.Play();
            }

            if (!TVsource.isPlaying && awakeTimer > 0.4f && awakeTimer < 1f)
            {
                TVsource.clip = dropSounds[1]; TVsource.loop = false; TVsource.Play();
            }
            if (!TVsource.isPlaying && awakeTimer > 1.5f && awakeTimer < 2f)
            {
                TVsource.clip = dropSounds[2]; TVsource.loop = false; TVsource.Play();
            }
        }
        if (!state.Equals(TVState.Haunted) && animator.GetCurrentAnimatorStateInfo(0).IsName("Haunted_TV_Crawl"))
        {
            state = TVState.Haunted;
            CameraViewManager.Instance.Release();
            ProtagonistUIController.Instance.AddToChat(lightsOutMessage, PositionState.Left, true);
            ProtagonistUIController.Instance.AddToChat(hintMessages[Random.Range(0, hintMessages.Length)], PositionState.Left, false);
            GameManager.Instance.State = GameState.Challenge;
            rb.isKinematic = false;
        }
        if (state.Equals(TVState.Haunted))
        {
            if (!hauntAmbientSource.isPlaying)
            {
                hauntAmbientSource.clip = hauntedSounds[Random.Range(0, hauntedSounds.Length)];
                hauntAmbientSource.loop = false;
                hauntAmbientSource.spatialBlend = 0.9f;
                hauntAmbientSource.Play();
            }
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

                state = TVState.Off;
                screen.SetActive(false);
            }
            else if (GetComponent<UIInteractions>().IsInteractive)
            {
                ProtagonistUIController.Instance.AddToChat(hintMessages[Random.Range(0, hintMessages.Length)], PositionState.Left);
                Debug.Log("Need to find a TV Switch"); // Show on UI
            }
        }

    }

    void Animate()
    {
        
        if (state.Equals(TVState.Idle))
        {
            creditsCube.SetActive(false);
            hauntedHands.SetActive(false);
            animator.ResetTrigger("Haunted");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Stop");
            animator.ResetTrigger("Grab");

            animator.SetTrigger("Idle");
        }
        if (state.Equals(TVState.Haunted))
        {
            creditsCube.SetActive(false);
            hauntedHands.SetActive(true);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Stop");
            animator.ResetTrigger("Grab");

            animator.SetTrigger("Haunted");
        }
        if (state.Equals(TVState.Off))
        {
            creditsCube.SetActive(true);
            hauntedHands.SetActive(false);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Haunted");
            animator.ResetTrigger("Grab");

            animator.SetTrigger("Stop");
           

            //animator.SetTrigger("Idle");
        }
        if (state.Equals(TVState.Awake))
        {
            creditsCube.SetActive(false);
            hauntedHands.SetActive(false);
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Stop");
            animator.ResetTrigger("Haunted");
            animator.ResetTrigger("Grab");

            animator.SetTrigger("Awake");
        }

        if (GameManager.Instance.State.Equals(GameState.GameOver)) // grab changed the gamestate -> game ended before TV was switched off -> game lost
        {
            creditsCube.SetActive(false);
            hauntedHands.SetActive(true);

            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Stop");
            animator.ResetTrigger("Awake");
            animator.ResetTrigger("Haunted");

            animator.SetTrigger("Grab");
        }
        if (GameManager.Instance.State.Equals(GameState.GameCompleted))
        {
            if (!isShowingCredits)
            {
                creditsTimer = 0;
                isShowingCredits = true;
                creditsCube.SetActive(true);
                animator.ResetTrigger("Idle");
                animator.ResetTrigger("Awake");
                animator.ResetTrigger("Haunted");
                animator.ResetTrigger("Grab");
                animator.ResetTrigger("Stop");

                animator.SetTrigger("Credits");
            }
            creditsTimer += Time.deltaTime;
            if (creditsTimer > creditsPeriod)
            {
                creditsCube.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Motivation();
        Switch();
        UpdateState();
        Animate();
        if (state.Equals(TVState.Haunted)) FollowProtagonist();
    }
}
