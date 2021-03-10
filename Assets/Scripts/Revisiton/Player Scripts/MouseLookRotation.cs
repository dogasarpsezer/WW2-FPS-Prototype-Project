using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookRotation : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Transform parentPlayer, childRoot;

    [SerializeField]
    private float zRot;

    [SerializeField]
    private bool invertOption = false;

    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    private int smoothSteps = 10;

    [SerializeField]
    private float smoothWeight = 0.4f;

    [SerializeField]
    private Vector2 xRotationLimits = new Vector2(-75f, 75f);

    [SerializeField]
    private bool canUnlock = true;

    [SerializeField]
    private float leaningRotationSpeed;

    private Vector2 lookAngle;
    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    private float currentRollAngle;
    private int lastlookFrame;

    #endregion

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CursorLockUnlock();

        if(Cursor.lockState == CursorLockMode.Locked)
            LookAround();
 

       // LeanWall();
    }
    #region Methods
    public void CursorLockUnlock()
    {
        //Locking the mouse and unlocking it by pressing escape (for menu etc.)
        if (Input.GetKeyDown(KeyCode.Escape) && canUnlock)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void LookAround()
    {
        //Get the mouse axis
        currentMouseLook = new Vector2(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"));
        Debug.Log(currentMouseLook.x);
        //Define the lookAngle vector as the current mouse rotation and sensitivity multiplication
        lookAngle.x += currentMouseLook.x * sensitivity * (invertOption ? 1f : -1f);
        lookAngle.y += currentMouseLook.y * sensitivity;

        //Limit the rotation
        lookAngle.x = Mathf.Clamp(lookAngle.x,xRotationLimits.x,xRotationLimits.y);

        //NOT FINISHED
        LeanWall();

        //Rotate the player
        childRoot.localRotation = Quaternion.Euler(lookAngle.x,childRoot.localRotation.y,currentRollAngle);
        parentPlayer.localRotation = Quaternion.Euler(0f, lookAngle.y, 0f);
    }

    public void LeanWall()
    {
        if (Input.GetKey(KeyCode.E))
        {
            currentRollAngle = Mathf.Lerp(currentRollAngle, -zRot, Time.deltaTime * leaningRotationSpeed);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            currentRollAngle = Mathf.Lerp(currentRollAngle, zRot, Time.deltaTime * leaningRotationSpeed);
        }
        else
        {
            currentRollAngle = Mathf.Lerp(currentRollAngle, 0, Time.deltaTime * leaningRotationSpeed);
        }


    }
    #endregion
}
