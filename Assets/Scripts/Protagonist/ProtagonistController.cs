using Assets.Scripts.Game;
using Assets.Scripts.Interactive;
using Assets.Scripts.Protagonist;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProtagonistController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] float jumpForce = 10f;
    Animator animator;
    [SerializeField] GameObject handler;
    ProtagonistState state;
    private bool tVSwitchFound;
    bool usingTVSwitch;
    public bool TVSwitchFound => tVSwitchFound;
    ProtagonistInteractions interactions;

    AudioSource footstepsSource;
    [SerializeField] AudioClip[] footsteps;
   

    [SerializeField] CinemachineFreeLook cinemachine;
    [SerializeField] Material defaultFace;
    [SerializeField] Material defaultBlink;
    [SerializeField] Material scaredFace;
    [SerializeField] Material scaredBlink;

    [SerializeField] GameObject Face;
    [SerializeField] GameObject Blink;
    bool menuView;
    // Start is called before the first frame update
    void Start()
    {
        interactions = GetComponent<ProtagonistInteractions>();
        animator = gameObject.GetComponentInChildren<Animator>(); // TODO refactor
        state = ProtagonistState.Idle;
        footstepsSource = gameObject.GetComponent<AudioSource>();
        menuView = true;
    }

    void UpdateFaceExpressions()
    {
        if (GameManager.Instance.State.Equals(GameState.Challenge))
        {
            Face.GetComponent<MeshRenderer>().material = scaredFace;
            Blink.GetComponent<MeshRenderer>().material = scaredBlink;
        }
        if (GameManager.Instance.State.Equals(GameState.GameCompleted))
        {
            Face.GetComponent<MeshRenderer>().material = defaultFace;
            Blink.GetComponent<MeshRenderer>().material = defaultBlink;
        }
    }

    void ChangeCameraView(GameState state)
    {
        if (menuView && !state.Equals(GameState.Menu)) // state changed FROM menu
        {
            menuView = false;
            // top rig
            cinemachine.m_Orbits[0].m_Height = 3.25f;
            cinemachine.m_Orbits[0].m_Radius = 5;
            // middle rig
            cinemachine.m_Orbits[1].m_Height = 3;
            cinemachine.m_Orbits[1].m_Radius = 6;
            // buttom rig
            cinemachine.m_Orbits[2].m_Height = 1;// 2.75f;
            cinemachine.m_Orbits[2].m_Radius = 5;//7;
        }

        else if (!menuView && state.Equals(GameState.Menu)) // state changed TO menu
        {
            menuView = true;
            // top rig
            cinemachine.m_Orbits[0].m_Height = 4;
            cinemachine.m_Orbits[0].m_Radius = 8;
            // middle rig
            cinemachine.m_Orbits[1].m_Height = 3.5f;
            cinemachine.m_Orbits[1].m_Radius = 12;
            // buttom rig
            cinemachine.m_Orbits[2].m_Height = 3;
            cinemachine.m_Orbits[2].m_Radius = 6;
        }
    }

    void UpdateView()
    {
        if (GameManager.Instance.State.Equals(GameState.Wait))
        {
            cinemachine.m_XAxis.m_MaxSpeed = 0;
            cinemachine.m_YAxis.m_MaxSpeed = 0;
        }
        else
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                cinemachine.m_XAxis.m_MaxSpeed = 300;
                cinemachine.m_YAxis.m_MaxSpeed = 2;

            }
            else if (Input.GetMouseButtonUp((int)MouseButton.Left))
            {
                cinemachine.m_XAxis.m_MaxSpeed = 0;
                cinemachine.m_YAxis.m_MaxSpeed = 0;
            }
        }
    }

    void UpdateViewAndRotate()
    {
        if (GameManager.Instance.State.Equals(GameState.Wait))
        {
            cinemachine.m_XAxis.m_MaxSpeed = 0;
            cinemachine.m_YAxis.m_MaxSpeed = 0;
        }
        else
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Right))
            {
                cinemachine.m_XAxis.m_MaxSpeed = 300;
                cinemachine.m_YAxis.m_MaxSpeed = 2;

            }
            else if (Input.GetMouseButtonUp((int)MouseButton.Right))
            {
                cinemachine.m_XAxis.m_MaxSpeed = 0;
                cinemachine.m_YAxis.m_MaxSpeed = 0;
            }
            if (Input.GetMouseButton((int)MouseButton.Right)
                && !GameManager.Instance.State.Equals(GameState.GameOver)
                && !GameManager.Instance.State.Equals(GameState.GameCompleted))
            {
                var angleCorrection = 0f;
                if (Input.GetKey(KeyCode.A))
                {
                    angleCorrection = -30;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    angleCorrection = 30;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    angleCorrection *= -1;
                }
                else if (!Input.GetKey(KeyCode.W))
                {
                    angleCorrection *= 2;
                }
                float cameraY = Camera.main.gameObject.transform.rotation.eulerAngles.y;
                var currentEuler = this.transform.rotation.eulerAngles;
                currentEuler.Set(0, cameraY + 180 + angleCorrection, 0);
                this.transform.localRotation = Quaternion.Euler(currentEuler);

            }
        }
    }

    void Use()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            state = ProtagonistState.Use;
            if (interactions.IsInteractive && !tVSwitchFound)
            {
                interactions.Pick.UI.gameObject.SetActive(false);

                var pick = interactions.Pick;
                if (pick != null)
                {
                    pick.Reassign(handler);
                    tVSwitchFound = true;
                }
            }
        }
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            state = ProtagonistState.Use;
        }
        if (tVSwitchFound && Input.GetKeyDown(KeyCode.T))
        {
            usingTVSwitch = true;
            state = ProtagonistState.Use;
        }

    }

    void GetScared()
    {
        if (Input.GetKey(KeyCode.S))
        {
            state = ProtagonistState.Scared;
        }
    }
    void Move()
    {

        //if (Input.GetKey(KeyCode.UpArrow))
        if (Input.GetKey(KeyCode.W))
        {
            state = ProtagonistState.Move;
            if (!Input.GetMouseButton((int)MouseButton.Right))
            {
                this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
            }
        }

        //if (Input.GetKey(KeyCode.DownArrow))
        else if (Input.GetKey(KeyCode.S))
        {
            state = ProtagonistState.Move;
            if (!Input.GetMouseButton((int)MouseButton.Right))
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
            }
        }

        //if (Input.GetKey(KeyCode.LeftArrow))
        if (Input.GetKey(KeyCode.A))
        {
            state = ProtagonistState.Move;
            if (Input.GetMouseButton((int)MouseButton.Right))
            {

                this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed / 1.2f);

                if (Input.GetKey(KeyCode.W))
                {
                    this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed / 8f);
                    this.transform.Translate(Vector3.right * Time.deltaTime * movementSpeed / 8);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 1.6f * movementSpeed);
                    this.transform.Translate(Vector3.right * Time.deltaTime * movementSpeed / 8);
                }
            }
            else
                this.transform.Rotate(Vector3.up, -1 * movementSpeed);
        }

        //if (Input.GetKey(KeyCode.RightArrow))
        else if (Input.GetKey(KeyCode.D))
        {
            state = ProtagonistState.Move;
            if (Input.GetMouseButton((int)MouseButton.Right))
            {

                this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed / 1.2f);

                if (Input.GetKey(KeyCode.W))
                {
                    this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed / 8f);
                    this.transform.Translate(Vector3.left * Time.deltaTime * movementSpeed / 8);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * 1.6f * movementSpeed);
                    this.transform.Translate(Vector3.left * Time.deltaTime * movementSpeed / 8);
                }
            }
            else
                this.transform.Rotate(Vector3.up, 1 * movementSpeed);
        }

        if (!state.Equals(ProtagonistState.Jump) && interactions.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            state = ProtagonistState.Jump;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector2.up * jumpForce, (ForceMode)ForceMode2D.Impulse);
        }
        if (gameObject.transform.position.y > 4)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector2.down * jumpForce / 2, (ForceMode)ForceMode2D.Impulse);
        }


    }

    void Animate()
    {
        if (state.Equals(ProtagonistState.Idle))
        {
            animator.ResetTrigger("Move");
            animator.ResetTrigger("Scared");
            animator.ResetTrigger("Use");
            animator.SetTrigger("Idle");
        }
        if (state.Equals(ProtagonistState.Move))
        {
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Scared");
            animator.ResetTrigger("Use");
            animator.SetTrigger("Move");
        }
        if (state.Equals(ProtagonistState.Use))
        {
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Scared");
            animator.ResetTrigger("Move");
            animator.SetTrigger("Use");
        }
        if (state.Equals(ProtagonistState.Scared))
        {
            animator.ResetTrigger("Idle");
            animator.ResetTrigger("Move");
            animator.ResetTrigger("Use");
            animator.SetTrigger("Scared");
        }
    }

    void PlaySounds()
    {
        

        if (state.Equals(ProtagonistState.Idle))
        {

        }
        
        if (state.Equals(ProtagonistState.Move))
        {
            if (!footstepsSource.isPlaying)
            {

                var clip = footsteps[Random.Range(0, footsteps.Length)];
                footstepsSource.clip = clip;
                footstepsSource.loop = false;
                footstepsSource.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFaceExpressions();

        UpdateView(); // left mouse click
        UpdateViewAndRotate(); // right mouse click
        ChangeCameraView(GameManager.Instance.State);
        Use();
        if (!GameManager.Instance.State.Equals(GameState.GameCompleted)
            && !GameManager.Instance.State.Equals(GameState.GameOver)
            && !GameManager.Instance.State.Equals(GameState.Wait))
        {
            Move();
            //GetScared();
        }
        Animate();
        PlaySounds();
        state = ProtagonistState.Idle;
    }
}
