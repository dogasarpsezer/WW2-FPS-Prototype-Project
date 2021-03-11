using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMechanics : MonoBehaviour
{
    public Rigidbody bulletRb;
    public int power;
    public Transform muzzlePoint;
    public GameObject shotWallPrototype;

    Transform particleRotation;
    void Start()
    {
        this.transform.parent = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        bulletRb.AddForce(transform.forward * power, ForceMode.VelocityChange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Vector3 hitPosition = this.transform.position;
            BullteHit(hitPosition);
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        //BullteHit();
    }
    private void OnTriggerStay(Collider other)
    {
       // BullteHit();
    }
    private void BullteHit(Vector3 hitPosition)
    {
        Instantiate(shotWallPrototype,hitPosition,this.transform.rotation);
        Destroy(gameObject);
    }

}
