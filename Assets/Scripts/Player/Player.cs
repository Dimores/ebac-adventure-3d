using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public Transform characterTransform;
    public Animator animator;

    public float speed = 1f; 
    public float turnSpeed = 1f; 
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    public float runSpeed = 1.5f;

    private float vSpeed = 0f; 
    private string run = "Run";
    private string jump = "Jump";

    void Update() {
        characterTransform.Rotate(0, Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxisRaw("Vertical"); 
        var speedVector = characterTransform.forward * inputAxisVertical * speed;

        if(characterController.isGrounded) {
            vSpeed = 0f;
            if (Input.GetButtonDown(jump)) {
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(Input.GetButton(run) ? speedVector * runSpeed * Time.deltaTime : speedVector * Time.deltaTime); 

        animator.SetBool(run, inputAxisVertical != 0);
        animator.speed = Input.GetButton(run) ? runSpeed : 1f;
    }
}
