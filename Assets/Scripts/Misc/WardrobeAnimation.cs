using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class WardrobeAnimation : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.Play("Haunted_Wardrobe_Menu", -1, Random.Range(0f, 1f));
        }
    }
}