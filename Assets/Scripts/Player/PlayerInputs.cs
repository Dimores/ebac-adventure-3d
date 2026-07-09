using System.Collections.Generic;
using UnityEngine;
using Actions;

public class PlayerInput : MonoBehaviour
{
    public List<InputActionToExecute> actions = new();

    private void Update()
    {
        foreach (var input in actions)
        {
            if (input.action == null)
                continue;

            if (input.key != KeyCode.None && Input.GetKeyDown(input.key))
            {
                input.action.Execute();
            }
        }
    }
}

[System.Serializable]
public class InputActionToExecute
{
    public KeyCode key = KeyCode.None;
    public ActionBase action;
}