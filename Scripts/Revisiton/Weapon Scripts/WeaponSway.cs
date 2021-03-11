using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    private float xAxisMouse, yAxisMouse;

    [SerializeField]
    private float rotationRate, smooth;
    [SerializeField]
    private WeaponManager weaponManager;

    private Quaternion targetRotation;
    private Quaternion targetChangeX;
    private Quaternion targetChangeY;
    private Quaternion initialRotation;

    void Awake()
    {
        //Taking the first rotation
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        if(weaponManager.CurrentWeaponGetter().shootingType != WeaponShootingType.THROWABLE)
        {
           WeaponSwayFunc();
        }
    }

    public void WeaponSwayFunc()
    {
        //Mouse axis
        xAxisMouse = Input.GetAxis("Mouse X");
        yAxisMouse = Input.GetAxis("Mouse Y");

        //Calculating the target rotation after mouse moves
        targetChangeX = Quaternion.AngleAxis(-rotationRate * xAxisMouse,Vector3.up);
        targetChangeY = Quaternion.AngleAxis(rotationRate * yAxisMouse, Vector3.right);
        targetRotation = targetChangeY * targetChangeX * initialRotation;

        //Rotatate the Weapons object like it has gun sway
        transform.localRotation = Quaternion.Lerp(transform.localRotation,targetRotation, Time.deltaTime * smooth);

    }
}
