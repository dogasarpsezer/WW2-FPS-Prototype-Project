using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField]
    private WeaponManager weaponManager;
    [SerializeField]
    private PlayerAttack playerAttackScript;
    private float recoilTarget;
    private Quaternion originalTarget;

    private void Start()
    {
        originalTarget = gameObject.transform.localRotation;
    }
    private void Update()
    {
        recoilTarget = weaponManager.CurrentWeaponGetter().getRecoilDegree();

        if (Input.GetMouseButtonDown(0))
        {
            gameObject.transform.Rotate(new Vector3((transform.localRotation.x - recoilTarget) * Time.deltaTime,transform.localRotation.y,transform.localRotation.z));

        }
        
    }
}
