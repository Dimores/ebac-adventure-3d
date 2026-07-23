using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using UnityEngine.Audio;

public class SFXPool : Singleton<SFXPool>
{
    public int poolSize = 10;
    public AudioMixerGroup sfxGroup;

    private List<AudioSource> _audioSourceList;

    private int _index = 0;

    protected override void Awake()
    {
        base.Awake();

        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for(int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject sfx = new GameObject("SFX_Pool");
        sfx.transform.SetParent(transform); 

        AudioSource audioSource = sfx.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxGroup;
        _audioSourceList.Add(audioSource);
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.NONE) return;

        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++;
        if (_index >= _audioSourceList.Count) _index = 0;
    }

    public void Play(SFXType sfxType, Vector2 random)
    {
        if (sfxType == SFXType.NONE) return;

        var sfx = SoundManager.Instance.GetSFXByType(sfxType);

        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].pitch = Random.Range(random.x, random.y);
        _audioSourceList[_index].Play();

        _index++;
        if (_index >= _audioSourceList.Count) _index = 0;
    }
}
