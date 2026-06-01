using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Ebac.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionaryStates;
        private StateBase _currentState;
        public float timeToStartGame = 1.0f;

        public StateBase CurrentState => _currentState;

        public void Init()
        {
            dictionaryStates = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T state, StateBase stateBase)
        {
            dictionaryStates.Add(state, stateBase);
        }

        [Button]
        public void SwitchState(T state)
        {
            if (_currentState != null) _currentState.OnStateExit();
            _currentState = dictionaryStates[state];
            _currentState.OnStateEnter();
        }

        public void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();
        }
    }
}