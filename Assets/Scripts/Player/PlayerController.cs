using Ebac.Core.Singleton;
using Ebac.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public Animator playerAnimator;
    public Rigidbody playerRigidbody;

    public float movementSpeed = 5f;
    public float jumpForce = 5f;

    public enum PlayerStates
    {
        IDLE,
        WALK,
        JUMP
    }

    public StateMachine<PlayerStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();

        stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerStateIdle());
        stateMachine.RegisterStates(PlayerStates.WALK, new PlayerStateWalk());
        stateMachine.RegisterStates(PlayerStates.JUMP, new PlayerStateJump());

        stateMachine.SwitchState(PlayerStates.IDLE);
    }

    public void SetAnimation(string v)
    {
        playerAnimator.SetTrigger(v);
    }

    public void Move(Vector3 movementInput)
    {
        playerRigidbody.velocity = movementInput * movementSpeed;
    }

    public void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * 0.3f);
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
