using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FSMExample fsmExample = (FSMExample)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine Controls", EditorStyles.boldLabel);

        if (fsmExample == null) return;

        if(fsmExample.stateMachine == null)
        {
            EditorGUILayout.LabelField("State Machine is not initialized.");
            return;
        }

        if (fsmExample.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: " + fsmExample.stateMachine.CurrentState);
        }
        

        if (GUILayout.Button("Switch State"))
        {
            if (fsmExample.stateMachine != null)
            {
                fsmExample.stateMachine.SwitchState(FSMExample.ExampleEnum.StateB);
            }
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Availabe States");

        if(showFoldout)
        {
            if(fsmExample.stateMachine.dictionaryStates == null || fsmExample.stateMachine.dictionaryStates.Count == 0)
            {
                EditorGUILayout.LabelField("No states available.");
                return;
            }

            var keys = fsmExample.stateMachine.dictionaryStates.Keys.ToArray();
            var vals = fsmExample.stateMachine.dictionaryStates.Values.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                EditorGUILayout.LabelField($"{keys[i]} :: {vals[i]}");
            }
        }
    }
}

