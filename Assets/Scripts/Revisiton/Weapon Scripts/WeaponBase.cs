using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums


public enum WeaponAim
{
    NONE,
    AIM,
    SELF_AIM
}

public enum WeaponShootingType
{
    LEVER,
    SEMIAUTO,
    FULLAUTO,
    THROWABLE,
    USABLE

}

public enum WeaponBulletType
{
    M1GARAND,
    M1911PISTOL,
    NONE,
    OTHERWEAPONBULLETS
}
#endregion

public class WeaponBase : MonoBehaviour
{
    #region Variables

    [Header("Enums")]
    public WeaponAim weaponAim;
    public WeaponBulletType bulletType;
    public WeaponShootingType shootingType;

    [Header("Weapon Animation Values")]
    [SerializeField]
    private AnimationClip reloadAnimation;
    [SerializeField]
    private AnimationClip idleAnimation;
    [SerializeField]
    private float usageTime;
    [SerializeField]
    private float reloadTime;


    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource shootSound;
    [SerializeField]
    private AudioSource reloadSound;
    [SerializeField]
    private AudioSource reloadNeeded;
    [SerializeField]
    private AudioSource specialSound;

    [Header("Shooting & Weapon Magazine Values")]
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private int magazineCapacity;
    [SerializeField]
    private int magazineLimit;
    [SerializeField]
    private ParticleSystem muzzleFlashEffect;

    #region Throwables
    [Header("Throwable")]
    [SerializeField]
    private GameObject throwableObject;
   // [SerializeField]
    //private ThrowableScript throwableScript;
    private Transform throwableTransform;
    [SerializeField]
    private AnimationClip throwableAimingAnimation;
    #endregion

    [Header("Extras")]
    public GameObject muzzleFlash;
    public GameObject attackPoint;
    private Animator animator;
    private bool sprintingState = false;
    [SerializeField]
    private float recoilDegree;
    #endregion
    void Awake()
    {
        //Get Component
        animator = GetComponent<Animator>();
        reloadTime = reloadAnimation.length;
    }
    private void Update()
    {
        Debug.DrawRay(muzzleFlash.transform.position,muzzleFlash.transform.forward,Color.green);
    }

    #region Methods
    public void ShootingAnimation()
    {
        animator.SetTrigger("Shoot");
    }

    public void Aiming(bool canAim)
    {
        animator.SetBool("isAiming",canAim);
    }

    public void MuzzleFlashSwitchOn()
    {
        muzzleFlash.SetActive(true);
    }

    public void MuzzleFlashSwitchOff()
    {
        muzzleFlash.SetActive(false);
    }

    public void SteadyAimIn()
    {
        //animator.SetFloat("SteadyAim", 0f);
        animator.SetBool("Aim", true);
    }

    public void SteadyAimOut()
    {
        //animator.SetFloat("SteadyAim", 0.65f);
        animator.SetBool("Aim", false);

    }

    public AudioSource GetShootingSound()
    {
        return shootSound;
    }

    public AudioSource GetReloadingSound()
    {
        return reloadSound;
    }

    public AudioSource GetReloadNeededSound()
    {
        return reloadNeeded;
    }

    public AudioSource GetSpecialSound()
    {
        return specialSound;
    }

    public void AttackPointOn()
    {
        attackPoint.SetActive(true);
    }

    public void AttackPointOff()
    {
        if(attackPoint.activeInHierarchy)
            attackPoint.SetActive(false);
    }

    public void ReloadAnimation()
    {
        animator.SetTrigger("Reload");
    }

    public float getReloadTime()
    {
        return reloadTime;
    }

    public float getFireRate()
    {
        return fireRate;
    }

    public void WalkingAnimation(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void SprintingAnimation(bool isSprinting)
    {
        animator.SetBool("isSprinting", isSprinting);
        sprintingState = isSprinting;
    }

    public bool SprintingState()
    {
        return sprintingState;
    }

    public int getMagazineCapacity()
    {
        return magazineCapacity;
    }

    public void ChangeMagazineLimit(int number) 
    {
        magazineLimit += number;
    }

    public int getMagazineLimit()
    {
        return magazineLimit;
    }

    public void SpecialAnimation()
    {
        animator.SetTrigger("SpecialAnimation");
    }

    public float getUsageTime() 
    {
        return usageTime; 
    }

    public void setUsingItemState(bool usingState)
    {
        animator.SetBool("UsingItem",usingState);
    }

    public ParticleSystem getMuzzleFlashEffect()
    {
        return muzzleFlashEffect;
    }

    public GameObject getThrowable()
    {
        return throwableObject;
    }

    public Transform gethrowableTransform()
    {
        throwableTransform = throwableObject.transform;
        return throwableTransform;
    }

    public AnimationClip getThrowableAimingAnimation()
    {
        return throwableAimingAnimation;
    }

    public float getRecoilDegree()
    {
        return recoilDegree;
    }
    #endregion
}
