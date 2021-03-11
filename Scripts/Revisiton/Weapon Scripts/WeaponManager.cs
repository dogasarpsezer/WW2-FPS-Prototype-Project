using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private WeaponBase[] weapons;

    private int currentWeapon;

    private float reloadTimeLength;
    private float timeCounter = 0;

    private int ammoCount = 0;

    private bool isAiming = false; 

    private bool isReloading = false;

    private bool isShooting = false;
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = 0;
        weapons[currentWeapon].gameObject.SetActive(true);
        ammoCount = weapons[currentWeapon].getMagazineCapacity();
        reloadTimeLength = weapons[currentWeapon].getReloadTime();
    }
    #endregion

    void Update()
    {
        //Taking out weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeOutNewWeapon(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TakeOutNewWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TakeOutNewWeapon(2);
        }

       if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TakeOutNewWeapon(3);
        }


        if (Input.GetKeyDown(KeyCode.R) && !SprintingState() && ammoCount != weapons[currentWeapon].getMagazineCapacity() && weapons[currentWeapon].getMagazineLimit() != 0 && !isAiming)
        {
            ammoCount = weapons[currentWeapon].getMagazineCapacity();
            weapons[currentWeapon].ChangeMagazineLimit(-1);
        }

        if (weapons[currentWeapon].weaponAim == WeaponAim.AIM)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isAiming = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                isAiming = false;
            }
        }

        //Every fire decreases a bullet
        if (Input.GetMouseButtonDown(0) && !isReloading && ammoCount > 0 && !SprintingState())
        {
            ammoCount--;
            if(ammoCount == 0)
            {
                weapons[currentWeapon].SpecialAnimation();
            }
        }

        //Stoping the chace to fire and wait for the reload aniimation to be finished
        if (isReloading)
        {
            timeCounter += Time.deltaTime;
            if(timeCounter >= reloadTimeLength)
            {
                isReloading = false;
                timeCounter = 0f;
            }
        }

    }

    #region Methods
    void TakeOutNewWeapon(int newWeapon)
    {
        if(currentWeapon == newWeapon)
        {
            return;
        }

        weapons[currentWeapon].gameObject.SetActive(false);
        currentWeapon = newWeapon;
        weapons[currentWeapon].gameObject.SetActive(true);
        ammoCount = weapons[currentWeapon].getMagazineCapacity();
        reloadTimeLength = weapons[currentWeapon].getReloadTime();
    }

    public WeaponBase CurrentWeaponGetter()
    {
        return weapons[currentWeapon];
    }

    public void ReloadWeapon()
    {
        ammoCount = weapons[currentWeapon].getMagazineCapacity();
        weapons[currentWeapon].ReloadAnimation();
        isReloading = true;
    }
   
    public int getAmmoCount()
    {
        return ammoCount;
    }

    public bool getReloadState()
    {
        return isReloading;
    }

    public AudioSource GetReloadNeededSound()
    {
        return weapons[currentWeapon].GetReloadNeededSound();
    }

    public AudioSource GetShootingSound()
    {
        return weapons[currentWeapon].GetShootingSound();
    }

    public AudioSource GetSpecialSound()
    {
        return weapons[currentWeapon].GetSpecialSound();
    }

    public AudioSource GetReloadSound()
    {
        return weapons[currentWeapon].GetReloadingSound();
    }

    public void SteadyAimWeaponInAnimation()
    {
        weapons[currentWeapon].SteadyAimIn();
    }
    public void SteadyAimWeaponOutAnimation()
    {
        weapons[currentWeapon].SteadyAimOut();
    }

    public void WalkinAnimation(bool isWalking)
    {
        weapons[currentWeapon].WalkingAnimation(isWalking);
       
    }

    public void SprintingAnimation(bool isSprinting)
    {
        weapons[currentWeapon].SprintingAnimation(isSprinting);
    }

    public bool SprintingState()
    {
        return weapons[currentWeapon].SprintingState();
    }

    public int getMagazineCapacity()
    {
        return weapons[currentWeapon].getMagazineCapacity();
    }
    
    public int getMagazineLimit()
    {
        return weapons[currentWeapon].getMagazineLimit();
    }

    public void SpecialAnimation()
    {
        weapons[currentWeapon].SpecialAnimation();
    }

    public float getUsageTime()
    {
        return weapons[currentWeapon].getUsageTime();
    }

    public void setUsingItemState(bool usingItem)
    {
        if(weapons[currentWeapon].shootingType == WeaponShootingType.USABLE)
        {
            weapons[currentWeapon].setUsingItemState(usingItem);
        }
    }

    public ParticleSystem getMuzzleFalshEffect()
    {
        return weapons[currentWeapon].getMuzzleFlashEffect();
    }

    public GameObject getThrowableObject()
    {
        return weapons[currentWeapon].getThrowable();
    }
    #endregion
}
