using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 180.0f;
    public float jumpForce = 8.0f;
    public float RunningForce = 15.0f;

    public float groundDistance = 0.2f;
    public LayerMask groundMask;
    
   public Rigidbody rb;
   private bool isGrounded;

  
   

    
    // Start is called before the first frame update
   public void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {


        //check if player is on ground
         isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        //Player Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;


       
        // Rotate towards movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Apply Movement
        Vector3 move = transform.forward * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);


   
        //Jumping

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        //Running
        if (isGrounded && Input.GetButtonDown("Space")){
            rb.AddForce(Vector3.up * RunningForce, ForceMode.Impulse);
        }
    }
}
