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

    private float vSpeed = 0f; 
    
    void Update() {
        characterTransform.Rotate(0, Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxisRaw("Vertical"); 
        var speedVector = characterTransform.forward * inputAxisVertical * speed;

        vSpeed = gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(speedVector * Time.deltaTime); 

        animator.SetBool("Run", inputAxisVertical != 0);
    }
}
