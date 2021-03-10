using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    #region Variables
    private float timeCalculator = 0f;

    private Rigidbody rb;

    [SerializeField]
    private float timeLimit = 2;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Collision detection and destroying the object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        //Destroying the object after a number of seconds
        timeCalculator += Time.deltaTime;

        if(timeCalculator > timeLimit)
        {
            Destroy(this.gameObject);
        }
    }
}
