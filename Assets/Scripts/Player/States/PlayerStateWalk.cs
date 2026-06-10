using Ebac.StateMachine;
using UnityEngine;

public class PlayerStateWalk : StateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        PlayerController.Instance.SetAnimation("Walk");
    }

    public override void OnStateStay()
    {
        base.OnStateStay();

        Debug.Log("PlayerStateWalk.OnStateStay");

        PlayerController.Instance.Move(PlayerInputManager.Instance.MovementInput);

        if(PlayerInputManager.Instance.MovementInput == Vector3.zero)
        {
            PlayerController.Instance.stateMachine.SwitchState(PlayerController.PlayerStates.IDLE);
        }
    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        PlayerController.Instance.Move(Vector3.zero);
    }
}
