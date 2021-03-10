using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandlerPlayer : MonoBehaviour
{
    public Animator playerAnimator;
    void Start()
    {
        
    }

    
    void Update()
    {
        MovementAnimation();
        if (Input.GetButtonDown("Fire1"))
        {
            playerAnimator.SetTrigger("isShooting");
        }
    }
    #region Methods
    public void MovementAnimation()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            playerAnimator.SetBool("isRunningR", true);
            playerAnimator.SetBool("isRunningL", false);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            playerAnimator.SetBool("isRunningR", false);
            playerAnimator.SetBool("isRunningL", true);
        }
        else
        {
            playerAnimator.SetBool("isRunningR", false);
            playerAnimator.SetBool("isRunningL", false);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            playerAnimator.SetBool("isRunningF", true);
            playerAnimator.SetBool("isRunningB", false);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            playerAnimator.SetBool("isRunningF", false);
            playerAnimator.SetBool("isRunningB", true);
        }
        else
        {
            playerAnimator.SetBool("isRunningF", false);
            playerAnimator.SetBool("isRunningB", false);
        }

    }
    #endregion
}
