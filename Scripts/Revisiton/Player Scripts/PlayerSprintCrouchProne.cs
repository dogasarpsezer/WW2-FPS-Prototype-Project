using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintCrouchProne : MonoBehaviour
{
    #region Variables
    [Header("Scripts")]
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerFootsteps playerFootsteps;
    [SerializeField]
    private PlayerStaminaScript playerStamina;
    [SerializeField]
    private WeaponManager weaponManager;

    [Header("Speeds of Movement")]
    public float moveSpeed = 4f;
    public float sprintSpeed = 6f;
    public float crouchSpeed = 1.5f;
    public float proneSpeed = 0.5f;


    [Header("Heights")]
    [SerializeField]
    private float playerNormalHeight = 1.7f;
    [SerializeField]
    private float playerCrouchHeight = 1f;
    [SerializeField]
    private float playerProneHeight = 0.5f;

    private bool isCrouching = false;
    private bool isProne = false;


    [Header("Volumes SoundFX")]
    [SerializeField]
    private float sprintVolume;
    [SerializeField]
    private float crouchVolume;
    [SerializeField]
    private float walkVolumeMin = 0.2f, walkVolumeMax = 0.6f;

    [Header("Extras")]
    [SerializeField]
    private Transform playerRoot;

    private bool isSprinting = false;

    private float walkDistance = 0.4f;
    private float sprintDistance = 0.25f;
    private float crouchDistance = 0.5f;
    #endregion
    void Awake()
    {
        #region Get Components
        playerMovement = GetComponent<PlayerMovement>();
        playerRoot = transform.GetChild(0);
        playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
        playerStamina = GetComponent<PlayerStaminaScript>();
        weaponManager = GetComponent<WeaponManager>();
        #endregion
    }

    private void Start()
    {
        #region Initializing
        playerFootsteps.volumeMin = walkVolumeMin;
        playerFootsteps.volumeMax = walkVolumeMax;
        playerFootsteps.stepLength = walkDistance;
        #endregion
    }

    void Update()
    {
        Sprint();
        Crouch();
        Prone();
    }

    #region Methods
    public void Sprint()
    {
        //Making the player sprint
        if(Input.GetKey(KeyCode.LeftShift) && !isCrouching && !isProne && playerStamina.getStamina() > 15f)
        {
            //Changing the variables according to the new state of the player
            playerMovement.playerSpeed = sprintSpeed;
            playerFootsteps.volumeMin = sprintVolume;
            playerFootsteps.volumeMax = sprintVolume;
            playerFootsteps.stepLength = sprintDistance;
            isSprinting = true;
            weaponManager.WalkinAnimation(false);
            weaponManager.SprintingAnimation(true);
        }
        //Makng the player not sprint
        else if(!Input.GetKey(KeyCode.LeftShift) && !isCrouching && !isProne)
        {
            //Changing the variables according to the new state of the player
            playerMovement.playerSpeed = moveSpeed;
            playerFootsteps.volumeMin = walkVolumeMin;
            playerFootsteps.volumeMax = walkVolumeMax;
            playerFootsteps.stepLength = walkDistance;
            isSprinting = false;
            //weaponManager.WalkinAnimation(true);
            weaponManager.SprintingAnimation(false);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && !isProne && playerStamina.getStamina() < 15f)
        { 
            //Changing the variables according to the new state of the player
            playerMovement.playerSpeed = moveSpeed;
            playerFootsteps.volumeMin = walkVolumeMin;
            playerFootsteps.volumeMax = walkVolumeMax;
            playerFootsteps.stepLength = walkDistance;
            isSprinting = false;
            weaponManager.WalkinAnimation(true);
            weaponManager.SprintingAnimation(false);
        }
    }

    public void Crouch()
    {
        //Making the player crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                //Changing the players state to the state of crouching
                playerRoot.localPosition = new Vector3(0f,playerCrouchHeight,0f);
                playerMovement.playerSpeed = crouchSpeed;
                isCrouching = true;
                isProne = false;
                //Changing variables according to the new state
                playerFootsteps.volumeMin = crouchVolume;
                playerFootsteps.volumeMax = crouchVolume;
                playerFootsteps.stepLength = crouchDistance;
                isSprinting = false;

            }
            else
            {
                //Changing the players state to the state of crouching
                playerRoot.localPosition = new Vector3(0f, playerNormalHeight, 0f);
                playerMovement.playerSpeed = moveSpeed;
                isCrouching = false;
                //Changing variables according to the new state
                playerFootsteps.volumeMin = walkVolumeMin;
                playerFootsteps.volumeMax = walkVolumeMax;
                playerFootsteps.stepLength = walkDistance;
                isSprinting = false;
            }


        }
    }
    public void Prone()
    {
        //Making the player prone
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isProne)
            {
                //Changing the players state to the state of prone
                playerRoot.localPosition = new Vector3(0f, playerProneHeight, 0f);
                playerMovement.playerSpeed = proneSpeed;
                isProne = true;
                isCrouching = false;
                isSprinting = false;
            }
            else
            {
                //Changing the players state to the state of prone
                playerRoot.localPosition = new Vector3(0f, playerNormalHeight, 0f);
                playerMovement.playerSpeed = moveSpeed;
                isProne = false;
                isSprinting = false;
            }


        }
    }

    public bool SprintingState()
    {
        return isSprinting;
    }
    #endregion
}
