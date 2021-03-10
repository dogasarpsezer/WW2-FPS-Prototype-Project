using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeBulletScript : MonoBehaviour
{
    [SerializeField]
    private BulletMovement bullet;
    private void OnCollisionEnter(Collision collision)
    {
        bullet.enabled = false;
        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
