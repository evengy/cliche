using Assets.Scripts.Helpers;
using Cinemachine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class CameraViewManager : Helpers.Singleton<CameraViewManager>
    {
        [SerializeField] CinemachineFreeLook cinemachine;
        // [SerializeField] GameObject menuView; // TODO
        [SerializeField] GameObject protagonistView;
        [SerializeField] GameObject tvAwakeView;
        // [SerializeField] GameObject gameCompletedView; // TODO
        // Use this for initialization
        CinemachineVirtualCamera vCamera;
        bool isAttached;
        Transform follow;
        Transform lookAt;
        private void Start()
        {
            vCamera = cinemachine.GetComponent<CinemachineVirtualCamera>();
        }
        public void AttachViewTo(Transform traget)
        {
            if (!isAttached)
            {
                follow = cinemachine.m_Follow;
                lookAt = cinemachine.m_LookAt;
                isAttached = true;
                cinemachine.m_Follow = traget.transform;
                cinemachine.m_LookAt = traget.transform;
            }
        }
        public void Release()
        {
            cinemachine.m_Follow = follow;
            cinemachine.m_LookAt = lookAt;
        }
    }
}