using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("References")]
    public CharacterController characterController;
    public Transform characterTransform;
    public Animator animator;

    [Header("Movement Settings")]
    public float speed = 1f; 
    public float turnSpeed = 1f; 
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    public float runSpeed = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    private float vSpeed = 0f; 
    private string run = "Run";
    private string jump = "Jump";

    #region LIFE
    public void Damage(float damage)
    {
        flashColors.ForEach(flashColor => flashColor.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }
    #endregion

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

        if (inputAxisVertical != 0)
            animator.SetFloat("AnimSpeed", inputAxisVertical < 0 ? -1f : 1f);
    }
}
