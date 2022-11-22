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
    public bool TVSwitchFound => tVSwitchFound;
    ProtagonistInteractions interactions;

    AudioSource audioSource;
    [SerializeField] AudioClip[] footsteps;

    [SerializeField] CinemachineFreeLook cinemachine;

    // Start is called before the first frame update
    void Start()
    {
        interactions = GetComponent<ProtagonistInteractions>();
        animator = gameObject.GetComponentInChildren<Animator>(); // TODO refactor
        state = ProtagonistState.Idle;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void UpdateView()
    {
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    // top rig
        //    cinemachine.m_Orbits[0].m_Height = 12;
        //    cinemachine.m_Orbits[0].m_Radius = 3;
        //    // middle rig
        //    cinemachine.m_Orbits[1].m_Height = 6;
        //    cinemachine.m_Orbits[1].m_Radius = 10;
        //    // buttom rig
        //    cinemachine.m_Orbits[2].m_Height = 1;
        //    cinemachine.m_Orbits[2].m_Radius = 5;
        //}

        //if (Input.GetKeyDown(KeyCode.F3))
        //{
        //    // top rig
        //    cinemachine.m_Orbits[0].m_Height = 12;
        //    cinemachine.m_Orbits[0].m_Radius = 3;
        //    // middle rig
        //    cinemachine.m_Orbits[1].m_Height = 6;
        //    cinemachine.m_Orbits[1].m_Radius = 10;
        //    // buttom rig
        //    cinemachine.m_Orbits[2].m_Height = 1;
        //    cinemachine.m_Orbits[2].m_Radius = 5;

        //    cinemachine.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        //}

        if (Input.GetMouseButtonDown((int)MouseButton.Right))
        {
            cinemachine.m_XAxis.m_MaxSpeed = 300;
            cinemachine.m_YAxis.m_MaxSpeed = 2;
        }
        if (Input.GetMouseButtonUp((int)MouseButton.Right))
        {
            cinemachine.m_XAxis.m_MaxSpeed = 0;
            cinemachine.m_YAxis.m_MaxSpeed = 0;
        }
    }

    void Use()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactions.IsInteractive)
            {
                interactions.Pick.UI.gameObject.SetActive(false);

                var pick = interactions.Pick;
                if (pick != null)
                {
                    pick.Reassign(handler);
                    tVSwitchFound = true;
                }
            }
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
            this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }

        //if (Input.GetKey(KeyCode.DownArrow))
        if (Input.GetKey(KeyCode.S))
        {
            state = ProtagonistState.Move;
            this.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }

        //if (Input.GetKey(KeyCode.LeftArrow))
        if (Input.GetKey(KeyCode.A))
        {
            state = ProtagonistState.Move;
            this.transform.Rotate(Vector3.up, -1 * movementSpeed);
        }

        //if (Input.GetKey(KeyCode.RightArrow))
        if (Input.GetKey(KeyCode.D))
        {
            state = ProtagonistState.Move;
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
            if (!audioSource.isPlaying)
            {

                var clip = footsteps[Random.Range(0, footsteps.Length)];
                audioSource.clip = clip;
                audioSource.loop = false;
                audioSource.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateView();
        Use();
        UpdateView();
        Move();
        //GetScared();
        Animate();
        PlaySounds();
        state = ProtagonistState.Idle;
    }
}
