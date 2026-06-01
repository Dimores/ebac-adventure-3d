using Ebac.StateMachine;
using UnityEngine;

public class FSMExample : MonoBehaviour
{
    public enum ExampleEnum
    {
        StateA,
        StateB,
        StateC
    }

    public StateMachine<ExampleEnum> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();
        stateMachine.Init();

        stateMachine.RegisterStates(ExampleEnum.StateA, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.StateB, new StateBase());
        stateMachine.RegisterStates(ExampleEnum.StateC, new StateBase());

        stateMachine.SwitchState(ExampleEnum.StateA);
    }
}

