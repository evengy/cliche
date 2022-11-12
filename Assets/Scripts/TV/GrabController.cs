using Assets.Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TV
{
    public class GrabController : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Protagonist"))
            {
                GameManager.Instance.State = GameState.End;
            }
        }
       
    }
}