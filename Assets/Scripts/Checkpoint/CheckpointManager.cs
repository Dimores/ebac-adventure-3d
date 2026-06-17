using System.Collections;
using System.Collections.Generic;
using Ebac.Core.Singleton;
using NaughtyAttributes;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpointKey = 0;

    public List<CheckpointBase> checkpoints;

    [Header("Offset")]
    [MinMaxSlider(0f, 20f)] 
    public Vector2 randomOffsetInterval; 

    private CheckpointBase _currentCheckpoint;

    private float randomX;
    private float randomZ;

    private Vector3 GenerateRandomOffset()
    {
        randomX = Random.Range(randomOffsetInterval.x, randomOffsetInterval.y);
        randomZ = Random.Range(randomOffsetInterval.x, randomOffsetInterval.y);

        randomX *= (Random.value > 0.5f) ? 1f : -1f;
        randomZ *= (Random.value > 0.5f) ? 1f : -1f;

        return new Vector3(randomX, 0, randomZ);
    }

    public bool HasCheckpoint()
    {
        return lastCheckpointKey > 0;
    }

    public void SaveCheckpoint(int i)
    {
        if (i > lastCheckpointKey)
        {
            lastCheckpointKey = i;
        }
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        _currentCheckpoint = checkpoints.Find(i => i.key == lastCheckpointKey);

        return _currentCheckpoint.transform.position + GenerateRandomOffset();
    }
}