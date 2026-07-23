using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;

    private MusicSetup _currentMusicSetup;

    private void Start()
    {
        Play();
    }

    private void Play()
    {
        _currentMusicSetup = SoundManager.Instance.GetMusicByType(musicType);

        SoundManager.Instance.PlayMusicByType(musicType);
    }
}
