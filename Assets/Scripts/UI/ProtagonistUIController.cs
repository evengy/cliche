using Assets.Scripts.Game;
using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ProtagonistUIController : Singleton<ProtagonistUIController>
    {
        [SerializeField] GameObject chatter; // TODO should be 2 - left and right, left slower, right faster.
        [SerializeField] GameObject canvas;

        Queue<GameObject> messageQueue;
        float timerStart;
        float timerStop;
        // Use this for initialization
        void Start()
        {
            canvas.SetActive(false);
            timerStart = Time.time;
            timerStop = Time.time;

            messageQueue = new Queue<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            timerStop += Time.deltaTime;
            if (messageQueue.Count > 3)
            {
                Debug.Log($"{messageQueue.Count}");
                var foo = messageQueue.Dequeue();
                Destroy(foo);
                Debug.Log($"{messageQueue.Count}");
            }
            if (timerStop - timerStart > 5)
            {
                canvas.SetActive(false);
            }
        }

        public void AddToChat(Material message)
        {
            timerStart = Time.time;
            timerStop = Time.time;
            canvas.SetActive(true);

            if (messageQueue.Count > 0)
            {
                var chatters = messageQueue.ToArray();
                foreach (var chatter in chatters)
                {
                    chatter.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                        chatter.GetComponent<RectTransform>().anchoredPosition.x,
                        chatter.GetComponent<RectTransform>().anchoredPosition.y + 50
                        );
                }
            }
            var foo = Instantiate(chatter);
            foo.transform.SetParent(canvas.transform, false);
            foo.GetComponent<Chatter>().SetMessage = message;
            messageQueue.Enqueue(foo);
        }


    }
}