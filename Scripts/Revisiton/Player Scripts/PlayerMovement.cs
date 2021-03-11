using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Animator mainCamAnimator;
    private CharacterController playerCharacterController;
    private Vector3 moveDirection;
    private string horizontal = "Horizontal";
    private string vertical = "Vertical";
    private float gravity = 25f;
    private float verticalSpeed = 0f;
    private bool isMoving = false;
    [SerializeField]
    private WeaponManager weaponManager;

    public float playerSpeed;
    public float jumpForce;
    #endregion
    void Awake()
    {
        //Get Component
        playerCharacterController = GetComponent<CharacterController>();
        weaponManager = GetComponent<WeaponManager>();
        mainCamAnimator = transform.Find("Root").Find("Main Camera").GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log(isMoving);
        Movement();
        //Check for player movement
        if(playerCharacterController.velocity.sqrMagnitude > 1f)
        {
            isMoving = true;
            weaponManager.WalkinAnimation(true);
        }
        else
        {
            isMoving = false;
            weaponManager.WalkinAnimation(false);
            weaponManager.SprintingAnimation(false);
        }

        mainCamAnimator.SetBool("isWalking", isMoving);
    }

    #region Methods
    public void Movement()
    {
        //Move the player int the direction chosen by the Input function
        moveDirection = new Vector3(Input.GetAxis(horizontal),0f,Input.GetAxis(vertical));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= playerSpeed * Time.deltaTime;

        AddGravity();

        playerCharacterController.Move(moveDirection);
    }

    public void AddGravity()
    {
        verticalSpeed -= gravity * Time.deltaTime;

        JumpAction();

        moveDirection.y = verticalSpeed * Time.deltaTime;
    }

    public void JumpAction()
    {
        if(playerCharacterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalSpeed = jumpForce;
        }
    }

    public bool getMovementState()
    {
        return isMoving;
    }
    #endregion
}
