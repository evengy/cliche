using Assets.Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class MenuDrawerController : MonoBehaviour
    {
        Animator animator;
        bool animate;
        // Use this for initialization
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }
        // Update is called once per frame
        void Update()
        {
            if (!animate && GameManager.Instance.State.Equals(GameState.Menu))
            {
                animate = true;
                animator.enabled = true;
                animator.SetFloat("Offset", Random.Range(0f, 10f));
                animator.Play("Haunted_Wardrobe_Menu");
            }
            if (!GameManager.Instance.State.Equals(GameState.Menu))
            {
                animate = false;
                animator.enabled = false;
            }
        }
    }
}