using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
            CheckCheckpoint();
    }

    private void CheckCheckpoint()
    {
        TurnItOn();
    }

    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }

    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }
}
