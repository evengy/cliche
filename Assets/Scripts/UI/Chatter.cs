using System.Collections;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class Chatter : MonoBehaviour
    {
        [SerializeField] GameObject message;
        public Material SetMessage {
            set
            {
                message.GetComponent<Image>().material = value;
            }
        }


    }
}