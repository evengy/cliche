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
            if (messageQueue.Count > 1) 
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

        public void AddToChat(Material message, PositionState position)
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
            if (position.Equals(PositionState.Left))
            {
                //foo.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                //    foo.GetComponent<RectTransform>().anchoredPosition.x - 10,
                //    foo.GetComponent<RectTransform>().anchoredPosition.y
                //    );
                //foo.GetComponent<Animator>().SetFloat("Offset", 1f);
            }
            else if (position.Equals(PositionState.Right))
            {
                //foo.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                //    foo.GetComponent<RectTransform>().anchoredPosition.x + 10,
                //    foo.GetComponent<RectTransform>().anchoredPosition.y
                //    );
                //foo.GetComponent<Animator>().SetFloat("Offset", 1f);
            }
            foo.GetComponent<Chatter>().SetMessage = message;
            messageQueue.Enqueue(foo);
        }


    }
}