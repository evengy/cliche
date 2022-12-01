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
        [SerializeField] AudioClip messageSound; // TODO
        [SerializeField] float messageShowPeriod = 3f;
        float messageShowTimer;
        Queue<GameObject> messageQueue;
        bool isShowing;
        GameObject message;
        void Start()
        {
            canvas.SetActive(false);
            messageQueue = new Queue<GameObject>();
        }

        void Update()
        {
            if (messageQueue.Count > 0)
            {
                canvas.SetActive(true);
                ShowMessages();
            }
            else if (messageShowTimer > messageShowPeriod)
            {
                canvas.SetActive(false);
            }
            messageShowTimer += Time.deltaTime;
        }

        private void ShowMessages()
        {
            if (!isShowing && messageShowTimer < messageShowPeriod)
            {
                isShowing = true;
                message = messageQueue.Dequeue();
                message.SetActive(true);
            }
            else if (messageShowTimer >= messageShowPeriod)
            {
                messageShowTimer = 0;
                if (message != null) Destroy(message);
                isShowing = false;
            }

        }

        public void AddToChat(Material message, PositionState position = Helpers.PositionState.Left, bool messageOverride = true)
        {
            if (messageOverride)
            {
                messageShowTimer = messageShowPeriod;
            }
            chatter.SetActive(false);
            var foo = Instantiate(chatter);

            foo.transform.SetParent(canvas.transform, false);
            #region interactive dialogs
            //if (position.Equals(PositionState.Left))
            //{
            //    //foo.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            //    //    foo.GetComponent<RectTransform>().anchoredPosition.x - 10,
            //    //    foo.GetComponent<RectTransform>().anchoredPosition.y
            //    //    );
            //    //foo.GetComponent<Animator>().SetFloat("Offset", 1f);
            //}
            //else if (position.Equals(PositionState.Right)) 
            //    {
            //    //foo.GetComponent<RectTransform>().anchoredPosition = new Vector2(
            //    //    foo.GetComponent<RectTransform>().anchoredPosition.x + 10,
            //    //    foo.GetComponent<RectTransform>().anchoredPosition.y
            //    //    );
            //    //foo.GetComponent<Animator>().SetFloat("Offset", 1f);
            //}
            #endregion
            foo.GetComponent<Chatter>().SetMessage = message;
            messageQueue.Enqueue(foo);
        }
    }
}