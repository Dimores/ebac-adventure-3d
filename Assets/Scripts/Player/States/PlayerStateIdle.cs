using Ebac.StateMachine;
using UnityEngine;

public class PlayerStateIdle : StateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);

        Debug.Log("PlayerStateIdle.OnStateEnter");

        PlayerController.Instance.SetAnimation("Idle");
    }

    public override void OnStateStay()
    {
        base.OnStateStay();

        if (PlayerInputManager.Instance.MovementInput != Vector3.zero)
        {
            PlayerController.Instance.stateMachine.SwitchState(PlayerController.PlayerStates.WALK);
        }
        else if (PlayerInputManager.Instance.JumpInput && PlayerController.Instance.IsGrounded())
        {
            PlayerController.Instance.stateMachine.SwitchState(PlayerController.PlayerStates.JUMP);
        }

    }
}
