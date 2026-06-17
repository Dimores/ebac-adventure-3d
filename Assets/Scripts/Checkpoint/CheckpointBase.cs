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
    private string checkpointKey = "CheckpointKey";

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
        if(PlayerPrefs.GetInt(checkpointKey, 0) > key)
            PlayerPrefs.SetInt(checkpointKey, key);

        checkpointActive = true;
    }

    private void LoadCheckpoint()
    {
        PlayerPrefs.GetInt(checkpointKey, key);
    }
    #endregion
}
