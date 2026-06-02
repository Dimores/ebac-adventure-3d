using Ebac.StateMachine;
using UnityEngine;

public class PlayerStateJump : StateBase
{
    private bool hasLeftGround;

    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);

        hasLeftGround = false;

        PlayerController.Instance.SetAnimation("Jump");
        PlayerController.Instance.Jump();
    }

    public override void OnStateStay()
    {
        base.OnStateStay();

        if (!PlayerController.Instance.IsGrounded())
        {
            hasLeftGround = true;
        }

        if (hasLeftGround && PlayerController.Instance.IsGrounded())
        {
            if (PlayerInputManager.Instance.MovementInput != Vector3.zero)
            {
                PlayerController.Instance.stateMachine.SwitchState(PlayerController.PlayerStates.WALK);
            }
            else
            {
                PlayerController.Instance.stateMachine.SwitchState(PlayerController.PlayerStates.IDLE);
            }
        }
    }
}