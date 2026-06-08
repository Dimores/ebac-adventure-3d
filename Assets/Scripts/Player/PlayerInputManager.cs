using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : Singleton<PlayerInputManager>
{
    public Vector3 MovementInput { get; private set; }
    public bool JumpInput { get; private set; }


    void Update()
    {
        MovementInput = new Vector3(0, 0, Input.GetAxisRaw("Vertical"));
        HandleJumpInput();
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            JumpInput = true;
        }
        else
        {
            JumpInput = false;
        }
    }
}
