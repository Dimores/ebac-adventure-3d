using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    [Header("Visual")]
    public MeshRenderer meshRenderer;

    [Header("Save")]
    public int key = 01;

    private bool checkpointActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!checkpointActive && other.transform.tag == "Player")
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
        SaveCheckpoint();
    }

    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.grey);
    }

    #region SAVE
    private void SaveCheckpoint()
    {
        CheckpointManager.Instance.SaveCheckpoint(key);

        checkpointActive = true;
    }
    #endregion
}
