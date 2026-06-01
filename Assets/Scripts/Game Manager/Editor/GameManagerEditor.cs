using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine Controls", EditorStyles.boldLabel);

        if (gameManager == null) return;

        if (gameManager.stateMachine == null)
        {
            EditorGUILayout.LabelField("State Machine is not initialized.");
            return;
        }

        if (gameManager.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: " + gameManager.stateMachine.CurrentState);
        }


        //if (GUILayout.Button("Switch State"))
        //{
        //    if (gameManager.stateMachine != null)
        //    {
        //        gameManager.stateMachine.SwitchState(GameManager.GameStates.GAMEPLAY);
        //    }
        //}

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Availabe States");

        if (showFoldout)
        {
            if (gameManager.stateMachine.dictionaryStates == null || gameManager.stateMachine.dictionaryStates.Count == 0)
            {
                EditorGUILayout.LabelField("No states available.");
                return;
            }

            var keys = gameManager.stateMachine.dictionaryStates.Keys.ToArray();
            var vals = gameManager.stateMachine.dictionaryStates.Values.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                EditorGUILayout.LabelField($"{keys[i]} :: {vals[i]}");
            }
        }
    }
}
