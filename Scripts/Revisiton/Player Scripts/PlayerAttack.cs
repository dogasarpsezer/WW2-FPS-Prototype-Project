using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Variables
    [Header("Scripts")]
    [SerializeField]
    private WeaponManager weaponManager;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerFootsteps footStepsOfThePlayer;
    [SerializeField]
    private PlayerStaminaScript playerStamina;
    [SerializeField]
    private PlayerHealthScript playerHealth;
    private ThrowableScript throwableScript;
 

    [Header("Animators")]
    [SerializeField]
    private Animator zoomCameraAnimator;
    [SerializeField]
    private Animator mainCameraAnimator;


    [Header("Extras")]
    [SerializeField]
    private GameObject Weapons;
    [SerializeField]
    private GameObject[] bulletType;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private Transform grenadeParent;

    [Header("Throwable")]
    [SerializeField]
    private GameObject throwableObject;


    private float nextTimeToFire;
    private float damage;
    private bool isSteadyAim = false;
    private bool isAimingSelf = false;
    private bool isAiming = false;
    private bool isShooting = false;
    private int ammoCount = 0;
    private float usageTimeCounter = 0f;
    private float throwableAimTime = 0;
    private bool isReloadingWeapon;
    #endregion
    void Awake()
    {
        #region Get Components
        weaponManager = GetComponent<WeaponManager>();

        zoomCameraAnimator = transform.Find("Root").transform.Find("FPS Camera").GetComponent<Animator>();

        mainCameraAnimator = transform.Find("Root").transform.Find("Main Camera").GetComponent<Animator>();

        playerStamina = GetComponent<PlayerStaminaScript>();


        playerMovement = GetComponent<PlayerMovement>();

        playerHealth = GetComponent<PlayerHealthScript>();

        ammoCount = weaponManager.getAmmoCount();

        isReloadingWeapon = weaponManager.getReloadState();

        footStepsOfThePlayer = GetComponent<PlayerFootsteps>();
        #endregion
    }

    void Update()
    {

        isReloadingWeapon = weaponManager.getReloadState();
        if (!isReloadingWeapon)
        {
            WeaponShoot();
            AimInAndOut();
        }
        Reload();
        ammoCount = weaponManager.getAmmoCount();
        SteadyAim();
    }

    #region Methods
    void WeaponShoot()
    {
        //Checking the weapon shooting type
        if(weaponManager.CurrentWeaponGetter().shootingType == WeaponShootingType.SEMIAUTO)
        {
            //Making the shot in time intervals according to fire rate
            if(Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire && !weaponManager.SprintingState())
            {
                nextTimeToFire = Time.time + 1/weaponManager.CurrentWeaponGetter().getFireRate();
                isShooting = true;

                if (weaponManager.CurrentWeaponGetter().bulletType == WeaponBulletType.M1GARAND && ammoCount > 0)
                {
                    weaponManager.CurrentWeaponGetter().ShootingAnimation();
                    GameObject bullet =  Instantiate(bulletType[0], weaponManager.CurrentWeaponGetter().muzzleFlash.transform.position, weaponManager.CurrentWeaponGetter().muzzleFlash.transform.rotation);
                    bullet.transform.parent = bulletParent;
                    weaponManager.CurrentWeaponGetter().getMuzzleFlashEffect().Play();
                    if (ammoCount != 1)
                    {
                        weaponManager.GetShootingSound().pitch = Random.Range(0.85f, 1);
                        weaponManager.GetShootingSound().Play();
                    }
                    else
                    {
                        weaponManager.GetSpecialSound().pitch = Random.Range(0.85f, 1);
                        weaponManager.GetSpecialSound().Play();
                        weaponManager.GetShootingSound().pitch = Random.Range(0.85f, 1);
                        weaponManager.GetShootingSound().Play();
                    }

                    
                    
                }
                else if(weaponManager.CurrentWeaponGetter().bulletType == WeaponBulletType.M1911PISTOL && ammoCount > 0)
                {
                    weaponManager.CurrentWeaponGetter().ShootingAnimation();
                    GameObject bullet = Instantiate(bulletType[1], weaponManager.CurrentWeaponGetter().muzzleFlash.transform.position, weaponManager.CurrentWeaponGetter().muzzleFlash.transform.rotation);
                    bullet.transform.parent = bulletParent;
                    weaponManager.CurrentWeaponGetter().getMuzzleFlashEffect().Play();
                    if (ammoCount != 1)
                    {
                        weaponManager.GetShootingSound().pitch = Random.Range(0.85f, 1);
                        weaponManager.GetShootingSound().Play();
                    }
                    /*else
                    {
                        weaponManager.GetSpecialSound().pitch = Random.Range(0.85f, 1);
                        weaponManager.GetSpecialSound().Play();
                        weaponManager.GetShootingSound().pitch = Random.Range(0.85f, 1);
                        weaponManager.GetShootingSound().Play();
                    }*/


                }

            }
        }
        else if (weaponManager.CurrentWeaponGetter().shootingType == WeaponShootingType.FULLAUTO)
        {
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                isShooting = true;
                nextTimeToFire = Time.time + 1/weaponManager.CurrentWeaponGetter().getFireRate();
                weaponManager.CurrentWeaponGetter().ShootingAnimation();
            }

            //if else for bullet type
        }
        else if (weaponManager.CurrentWeaponGetter().shootingType == WeaponShootingType.LEVER)
        {
            if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
            {
                isShooting = true;
                nextTimeToFire = Time.time + 1 / weaponManager.CurrentWeaponGetter().getFireRate();
                weaponManager.CurrentWeaponGetter().ShootingAnimation();
            }
            //if else for bullet type
        }
        else if (weaponManager.CurrentWeaponGetter().shootingType == WeaponShootingType.THROWABLE)
        {
            isShooting = false;
            if (Input.GetMouseButtonDown(0) && ammoCount > 0 && !weaponManager.SprintingState())
            {
                isAimingSelf = true;
            }

            if (isAimingSelf)
            {

                if (!Input.GetMouseButton(0) && throwableAimTime >= weaponManager.CurrentWeaponGetter().getThrowableAimingAnimation().length && isAimingSelf)
                {
                    throwableObject = Instantiate(weaponManager.CurrentWeaponGetter().getThrowable());
                    throwableObject.transform.parent = grenadeParent;
                    throwableScript = throwableObject.transform.GetComponent<ThrowableScript>();
                    throwableScript.ThrowableTransform(weaponManager.CurrentWeaponGetter().muzzleFlash.transform);
                    throwableScript.ThrowableFire();
                    isAimingSelf = false;
                }
            }

                

        }
        else if (weaponManager.CurrentWeaponGetter().shootingType == WeaponShootingType.USABLE)
        {
            isShooting = false;
            if (Input.GetKey(KeyCode.F) && ammoCount > 0 && playerHealth.getHealthPoints() != 100f)
            {
                weaponManager.setUsingItemState(true);
                usageTimeCounter += Time.deltaTime;
                if(usageTimeCounter >= weaponManager.getUsageTime())
                {
                    playerHealth.setHealthPoints(100f);
                    playerHealth.setBleedingEffect(false);
                    weaponManager.setUsingItemState(false);
                    ammoCount--;
                }
                
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                usageTimeCounter = 0f;
                weaponManager.setUsingItemState(false);
            }
        }
        else
        {
            isShooting = false;
        }
    }

    void AimInAndOut()
    {
        if(weaponManager.CurrentWeaponGetter().weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnimator.Play("Aim In");
                isAiming = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnimator.Play("Aim Out");
                isAiming = false;
            }
        }

        if (weaponManager.CurrentWeaponGetter().weaponAim == WeaponAim.SELF_AIM)
        {
            if (isAimingSelf)
            {
                weaponManager.CurrentWeaponGetter().Aiming(isAimingSelf);
                throwableAimTime += Time.deltaTime * 1;
            }

            if (!isAimingSelf && throwableAimTime >= weaponManager.CurrentWeaponGetter().getThrowableAimingAnimation().length)
            {
                weaponManager.CurrentWeaponGetter().Aiming(isAimingSelf);
                throwableAimTime = 0f;
            }
            else if (!isAimingSelf)
            {
                throwableAimTime = 0;
            }
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !weaponManager.SprintingState() && ammoCount != weaponManager.getMagazineCapacity() && weaponManager.getMagazineLimit() != 0 && !isAiming)
        {
            weaponManager.ReloadWeapon();
            weaponManager.GetReloadSound().pitch = Random.Range(0.7f,0.8f);
            weaponManager.GetReloadSound().Play();
        }
    }

    void SteadyAim()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isAiming && !playerMovement.getMovementState() && playerStamina.getStamina() > 0f)
        {
            weaponManager.SteadyAimWeaponInAnimation();
            zoomCameraAnimator.SetBool("isSteady", true);
            mainCameraAnimator.Play("MainCameraSteadyAimIn");
            mainCameraAnimator.SetBool("isSteadyMain", true);
            isSteadyAim = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isAiming || !isAiming || playerMovement.getMovementState() || playerStamina.getStamina() <= 0)
        {
            weaponManager.SteadyAimWeaponOutAnimation();
            zoomCameraAnimator.SetBool("isSteady",false);
           if (!mainCameraAnimator.GetBool("isWalking") && !mainCameraAnimator.GetBool("isSteadyMain"))
            {
                mainCameraAnimator.Play("Idle");
            }
            else if(mainCameraAnimator.GetBool("isSteadyMain"))
            {
                mainCameraAnimator.Play("MainCameraSteadyAimOut");
                mainCameraAnimator.SetBool("isSteadyMain", false);
            }
            
            
            isSteadyAim = false;
        }
    }

    public bool SteadyAimState()
    {
        return isSteadyAim;
    }

    public bool ShootingState()
    {
        return isShooting;
    }
    #endregion
}
