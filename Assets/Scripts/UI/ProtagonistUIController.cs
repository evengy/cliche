using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ProtagonistUIController : Singleton<ProtagonistUIController>
    {
        [SerializeField] GameObject chatter;
        [SerializeField] GameObject canvas;

        Queue<GameObject> messageQueue;
        // Use this for initialization
        void Start()
        {
            messageQueue = new Queue<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if (messageQueue.Count > 3)
            {
                Debug.Log($"{messageQueue.Count}");
                var foo = messageQueue.Dequeue();
                Destroy(foo);
                Debug.Log($"{messageQueue.Count}");
            }
        }

        public void AddToChat(Material message)
        {
            var foo = Instantiate(chatter); // TODO animate
            foo.transform.SetParent(canvas.transform,false);
            foo.GetComponent<Chatter>().SetMessage=message;
            messageQueue.Enqueue(foo);
        }


    }
}