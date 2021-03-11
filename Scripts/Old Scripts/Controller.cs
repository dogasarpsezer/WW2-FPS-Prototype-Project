using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region Variables
    private float speed;
    private bool isCrouching = false;
    private bool isProne = false;
    private CapsuleCollider collider;
    private Rigidbody myrb2d;
    private bool isJumping = false;

    public float normalSpeed;
    public float sprintingSpeed;
    public float crouchingNormalSpeed;
    public float crouchingSprintingSpeed;
    public float proneNormalSpeed;
    public float proneSrintingSpeed;
    public float jumpForce;
    #endregion

    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        myrb2d = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Calling the methods so that they can work every frame
        Movement(isCrouching, isProne);
        isCrouching = IsCrouching(isCrouching);
        Crouch(isCrouching);
        isProne = IsProne(isProne);
        Prone(isProne);
        isJumping = IsJumping(isJumping, myrb2d);

    }
    #region Methods
    public void Movement(bool isCrouching,bool isProne)
    {

        //Checking if the sprint button is pressed and giving the speed accordingly
        if (Input.GetButton("Sprint"))
        {
            if (isCrouching)
            {
                speed = crouchingSprintingSpeed;
            }
            else if (isProne)
            {
                speed = proneSrintingSpeed;
            }
            else
            {
                speed = sprintingSpeed;
                
            }
        }
        else
        {
            if (isCrouching)
            {
                speed = crouchingNormalSpeed;
            }
            else if (isProne)
            {
                speed = proneNormalSpeed;
            }
            else
            {
                speed = normalSpeed;
                
            }
        }

       
        //Movement towards given direction
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
        this.transform.Translate(Vector3.right * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        
    }

    public bool IsCrouching(bool isCrouching)
    {
        if (Input.GetButtonDown("Crouch") && !isProne)
        {
            isCrouching = !isCrouching;
        }

        return isCrouching;
    }

   public void Crouch(bool isCrouching)
    {
        if (isCrouching && !isProne)
        {
            collider.height = 1f;
        }
        else if (!isProne && !isCrouching)
        {
            
            collider.height = 2f;
        }
    }

    public bool IsProne(bool isProne)
    {
        //Moving the object downwards but we use manipulate the collider so that ther camera would stay on surface
        if (Input.GetButtonDown("Prone") && !isCrouching)
        {
            if (isProne)
            {
                this.transform.position = new Vector3(this.transform.position.x,collider.center.y, this.transform.position.z);
            }
            isProne = !isProne;
        }
        return isProne;
    }

    public void Prone(bool isProne)
    {
        //Using conditinal to actually use the prone action and not the crouch action
        //Adjusting collider height
        if (isProne && !isCrouching)
        {
            collider.center= new Vector3(collider.center.x,1f, collider.center.z);
            collider.height = 1f;
           
        }
        else if( !isProne && !isCrouching )
        {
            collider.center = new Vector3(0f, 0f, 0f);
            collider.height = 2f;
        }
    }

    public bool IsJumping(bool isJumping,Rigidbody myrb2d)
    {
        //Jump action by using Force upwards
        if(!isJumping && !isCrouching && !isProne)
        {
            if (Input.GetButtonDown("Jump"))
            {
                myrb2d.AddForce(Vector3.up * jumpForce);
                isJumping = true;
            }
        }

        return isJumping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Making double jump impossible
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }


    #endregion

}
