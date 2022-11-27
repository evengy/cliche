using Assets.Scripts.Helpers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class MenuManager : Singleton<MenuManager>
    {
        [SerializeField] Animator chairAnimator;
        [SerializeField] Animator wardrobeAnimator;
        [SerializeField] Animator handAnimator;
        [SerializeField] Animator bookAnimator;

        float timer;
        private void Start()
        {
            timer = 9.2f;
        }
        void Update()
        {
            timer += Time.deltaTime;
            if (!GameManager.Instance.State.Equals(GameState.Menu))
            {
                chairAnimator.enabled = false;
            }
            else
            {
                if (timer > 10f)
                {
                    timer = 0;
                    chairAnimator.SetTrigger("GetBook");
                    bookAnimator.SetTrigger("Appear");
                    wardrobeAnimator.SetTrigger("Open");
                    handAnimator.SetTrigger("Take");
                }
            }
        }
    }
}