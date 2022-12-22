using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SearchService;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed;
    public float rotationSpeed;
    public float jumpHeight;
    public float gravityMultiplier;
    
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float nextFire = 0f;
    private float fireRate = 1f; //cooldown time
    int i;





    void Start()
    {
        i = InteractionManager.Instance.i;
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
      
    }

    void Update()
    {

        //basic player movement START
      
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
        //basic player movements END

        if (Input.GetKeyDown(KeyCode.F) && InteractionManager.Instance.isInRange && Time.time > nextFire)
        {
            //PickUp();
            Interact();
            nextFire = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.F) && InteractionManager.Instance.isInInventory && Time.time > nextFire)
        {

            Drop();
            nextFire = Time.time + fireRate;
        }

       
       


    }



   


    public void Interact()
    {
        if (InteractionManager.Instance.objInRange.CompareTag("Present"))
        {
            PickUp();

        }
        else if (InteractionManager.Instance.objInRange.CompareTag("Door") && InteractionManager.Instance.isInInventory)
        {
            Debug.Log("Deliver"); //To do deliver function
            Deliver();
        }
        else Debug.Log("nothing to do");



    }
    public void PickUp()
    {
        Debug.Log("PickUp");
       
       InteractionManager.Instance.isInInventory = true;
        InteractionManager.Instance.inventory[0] = InteractionManager.Instance.objInRange;
        InteractionManager.Instance.tempInvTag = InteractionManager.Instance.childInRange.tag;
        InteractionManager.Instance.objInRange.SetActive(false);
      
        InteractionManager.Instance.objInRange = null;
        InteractionManager.Instance.childInRange = null;


        InteractionManager.Instance.isInRange = false;
    }

    

    public void Drop()
    {
        Debug.Log("Drop");
     
        InteractionManager.Instance.inventory[0].transform.position = transform.TransformPoint(Vector3.forward);
        InteractionManager.Instance.inventory[0].SetActive(true);
        InteractionManager.Instance.isInInventory = false;
        InteractionManager.Instance.inventory[0] = null;
        InteractionManager.Instance.tempInvTag = null;
    }

    void Deliver()
    {
        InteractionManager.Instance.presentsTags[i] = InteractionManager.Instance.tempInvTag;
        InteractionManager.Instance.doorsTags[i] = InteractionManager.Instance.childInRange.tag;
        
        i++;
        InteractionManager.Instance.i = i;
        InteractionManager.Instance.tempInvTag = null;
        InteractionManager.Instance.inventory[0] = null; 
        InteractionManager.Instance.isInInventory = false;

    }

 



}