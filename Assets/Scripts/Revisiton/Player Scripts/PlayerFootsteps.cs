using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private AudioSource playerAudioSource;

    [SerializeField]
    private AudioClip[] grassAudioSteps;

    [SerializeField]
    private CharacterController playerController;

    [HideInInspector]
    public float volumeMin, volumeMax;

    private float distanceTaken = 0f;

    [HideInInspector]
    public float stepLength;

    private int repeatChecker;
    private int oldClip = 6;
    #endregion
    void Awake()
    {
        #region Get Components
        playerAudioSource = GetComponent<AudioSource>();
        playerController = GetComponentInParent<CharacterController>();
        #endregion
    }

    void Update()
    {
        CheckFootstep();
    }

    public void CheckFootstep()
    {
        //If the player is not grounded just return the function and don't do the rest
        if (!playerController.isGrounded)
        {
            return;
        }

        //Check the movement of the player
        if(playerController.velocity.sqrMagnitude > 0)
        {
            //Check the distance taken to the step length
            distanceTaken += Time.deltaTime;
            if(distanceTaken > stepLength)
            {
                //Apply changes and play Audio according to the random clip
                playerAudioSource.volume = Random.Range(volumeMin,volumeMax);
                repeatChecker = Random.Range(0, grassAudioSteps.Length);
                while (repeatChecker == oldClip)
                {
                    repeatChecker = Random.Range(0, grassAudioSteps.Length);
                }
                oldClip = repeatChecker;
                playerAudioSource.clip = grassAudioSteps[repeatChecker];
                playerAudioSource.Play();
                
                //Initialize
                distanceTaken = 0f;
            }

        }
        else
        {
            //Initialize
            distanceTaken = 0f;
        }
    }
}
