using System.Collections;
using System.Transactions;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Cameras
{
    public class CameraBillboard : MonoBehaviour
    {
        /// If true, will ignore the selected camera and use Camera.main.
        public bool useMainCamera;

        [Space(10)]

        /// Camera to look at
        public Camera billboardCamera;

        /// Extra angle to rotate at, only change if the default is not working
        public float extraAngleRotation = 180;

        void Update()
        {
            if (useMainCamera)
                billboardCamera = Camera.main;
        }

        void LateUpdate()
        {
            LookAtCamera();
        }

        private void LookAtCamera()
        {
            Transform cameraToLookAt = useMainCamera ? Camera.main.transform : billboardCamera.transform;
            transform.LookAt(cameraToLookAt);
            transform.Rotate(0, extraAngleRotation, 0);
        }
    }
}