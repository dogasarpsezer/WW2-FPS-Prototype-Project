using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{

    public class MouseLookRotation : MonoBehaviour
    {

        #region Variables
        private Quaternion characterTargetRotation;
        private Transform characterTransform;
        private Quaternion cameraTargetRotation;
        private Transform cameraTransform;

        public float mouseSensitivityX;
        public float mouseSensitivityY;
        public bool clampVerticalRotation;
        public float minimumX;
        public float maximumX;
        public bool smooth;
        public float smoothTime;
        #endregion


        void Start()
        {
            //Locking the cursor from the beginning
            LockCursor(true);

            //Define the transforms to the instance of the objects when the code runs
            characterTransform = this.transform;
            cameraTransform = Camera.main.transform;

            //Define the rotations to the instance of the objects when the code runs
            characterTargetRotation = characterTransform.localRotation;
            cameraTargetRotation = cameraTransform.localRotation;
        }

        // Update is called once per frame
        void Update()
        {
            LookTowards();

            //These two conditinals unlock the cursor or lock them depending on the input
            if (Input.GetButton("Cancel"))
            {
                LockCursor(false);
            }

            if (Input.GetButton("Fire1"))
            {
                LockCursor(true);
            }


        }

        #region Methods

        //Locking cursor and making it visible or not visible by using a boolean that determines the state
        private void LockCursor(bool isLockedCursor)
        {

            if (isLockedCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }

       private void LookTowards()
        {
            float rotationY = Input.GetAxis("Mouse X") * mouseSensitivityX;
            float rotationX = Input.GetAxis("Mouse Y") * mouseSensitivityY;

            characterTargetRotation *= Quaternion.Euler(0f, rotationY, 0);
            cameraTargetRotation *= Quaternion.Euler(-rotationX, 0f, 0f);

            if (clampVerticalRotation)
            {
                cameraTargetRotation = ClampRotationAroundXAxis(cameraTargetRotation);
            }

            if (smooth)
            {
                characterTransform.localRotation = Quaternion.Slerp( characterTransform.localRotation, characterTargetRotation , smoothTime * Time.deltaTime);
                cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, cameraTargetRotation,smoothTime * Time.deltaTime);
            }
            else
            {
                characterTransform.localRotation = characterTargetRotation;
                cameraTransform.localRotation = cameraTargetRotation;
            }
        }
        //This algorithm is taken from Coursera Michigan State Course. I still don't know how it works.
        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, minimumX, maximumX);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
        #endregion
    }
}
