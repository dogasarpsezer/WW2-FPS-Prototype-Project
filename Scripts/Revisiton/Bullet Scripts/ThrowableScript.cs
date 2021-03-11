using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThrowableType{
    DAMAGE,
    OTHER
}

public class ThrowableScript : MonoBehaviour
{
    [SerializeField]
    private ThrowableType throwableType;
    [SerializeField]
    private SphereCollider explosionZone;
    private bool explosion = false;
    private Rigidbody throwableRigid;
    [SerializeField]
    private float throwableForce;
    [SerializeField]
    private float explosionTime;
    [SerializeField]
    private ParticleSystem throwableEffect;
    [SerializeField]
    private MeshRenderer[] meshes;
    private Vector3 distanceFromExplosion;
    private void Awake()
    {
        throwableRigid = gameObject.transform.GetComponent<Rigidbody>();
        explosionZone = gameObject.transform.GetComponent<SphereCollider>();
    }

    void Start()
    {
        Invoke("ThrowableDestroyer",explosionTime + 3f);
        Invoke("ThrowableEffect", explosionTime);
        Invoke("ExplosionStart", explosionTime);
        Invoke("ExplosionEnd", explosionTime + 0.025f);
    }


    public void ThrowableTransform(Transform throwableTransform)
    {
        gameObject.transform.position = throwableTransform.position;
        gameObject.transform.rotation = throwableTransform.rotation;
        ThrowableFire();
    }

    public void ThrowableFire()
    {
        throwableRigid.AddForce(gameObject.transform.forward * throwableForce,ForceMode.Impulse);
    }

    public void ThrowableEffect()
    {
        for(int i = 0; i < meshes.Length; i++)
        {
            meshes[i].enabled = false;
        }
        throwableEffect.Play();
    }

    public void ThrowableDestroyer()
    {
        if (gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    public void ExplosionStart()
    {
        explosion = true;
    }

    public void ExplosionEnd()
    {
        explosion = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && explosion)
        {
            distanceFromExplosion = other.gameObject.transform.position - this.gameObject.transform.position;
            Debug.Log(distanceFromExplosion.sqrMagnitude);
            if(distanceFromExplosion.sqrMagnitude < 18f)
            {
                Debug.Log("DEAD");
            }
            else if (distanceFromExplosion.sqrMagnitude > 18f && distanceFromExplosion.sqrMagnitude < 35f)
            {
                Debug.Log("Bleed LEVEL 1");
            }
            else
            {
                Debug.Log("Bleed LEVEL2");
            }
        }
    }

}
