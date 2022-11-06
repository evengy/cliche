using Assets.Scripts.Interactions;
using Assets.Scripts.Protagonist;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    Animator animator;
    [SerializeField] GameObject handler;
    ProtagonistState state;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>(); // TODO refactor
        state = ProtagonistState.Idle;
    }

    void Use()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GetComponent<ProtagonistInteractions>().Interactive)
            {
                var pick = GetComponent<ProtagonistInteractions>().Pick;
                pick.Reassign(handler);

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            state = ProtagonistState.Move;
            this.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            state = ProtagonistState.Move;
            this.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            state = ProtagonistState.Move;
            this.transform.Rotate(Vector3.up, -1 * movementSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            state = ProtagonistState.Move;
            this.transform.Rotate(Vector3.up, 1 * movementSpeed);
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

    // Update is called once per frame
    void Update()
    {
        Use();
        Move();
        GetScared();
        Animate();
        state = ProtagonistState.Idle;
    }
}
