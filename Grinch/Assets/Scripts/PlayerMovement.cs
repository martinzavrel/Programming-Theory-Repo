using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpHeight;
    public float gravityMultiplier;



    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    void Update()
    {  
        //assign player input to variables
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //movement according to player input
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();


        float gravity = Physics.gravity.y * gravityMultiplier;   //calculation of gravity physics
        ySpeed += gravity * Time.deltaTime; // adjust vertical speed according to gravity

        if (characterController.isGrounded) //if isgrounded reset physics vertical velocity
        {
            ySpeed = -0.5f;
            characterController.stepOffset = originalStepOffset;
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity); //calculation for jump height required
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        //rotate player to movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}