using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Rigidbody bulletRigidbody;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float bulletDropRate;

    [SerializeField]
    private float bulletDropDistance;

    [SerializeField]
    private float bulletFirstDropStartTime;

    private float bulletGravity = 1;

    private float bulletTime = 0;

    private float initialBulletDrop;

    private Transform spawnTransform;
    #endregion

    private void Awake()
    {
        //Get component
        bulletRigidbody = GetComponent<Rigidbody>();
        spawnTransform = this.transform;
    }

    private void Update()
    {
        //Ajust the gravity towards negative so that the bullet drops in slow pace accelarating
        bulletGravity -= bulletDropRate;
    }

    private void FixedUpdate()
    {
        //Check if the player shoots downwards or upwards
       if(spawnTransform.forward.y >= 0)
        {
            //Actually multipling the bullets movement by negative gravity so that bullet has drop 
            bulletRigidbody.velocity = new Vector3(spawnTransform.forward.x * bulletSpeed, spawnTransform.forward.y * bulletGravity * bulletSpeed, spawnTransform.forward.z * bulletSpeed);


        }
        else
        {
            //No gravity applied
            bulletRigidbody.velocity = new Vector3(spawnTransform.forward.x * bulletSpeed, spawnTransform.forward.y * bulletSpeed, spawnTransform.forward.z * bulletSpeed);
        }
    }
}
