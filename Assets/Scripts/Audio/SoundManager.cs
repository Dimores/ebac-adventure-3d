using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

namespace Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [Header("Music Audio Source")]
        public AudioSource musicSource;

        [Header("Music")]
        public List<MusicSetup> musicSetups;

        [Space(5)]
        [Header("SFX")]
        public List<SFXSetup> sfxSetups;

        #region MUSIC
        public void PlayMusicByType(MusicType musicType)
        {
            var music = GetMusicByType(musicType);
            musicSource.clip = music.audioClip;
            musicSource.Play();
        }

        public MusicSetup GetMusicByType(MusicType musicType)
        {
            return musicSetups.Find(i => i.musicType == musicType);
        }
        #endregion

        #region SFX
        public SFXSetup GetSFXByType(SFXType sfxType)
        {
            return sfxSetups.Find(i => i.sfxType == sfxType);
        }
        #endregion
    }

    public enum MusicType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03,
    }

    public enum SFXType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03,
    }


    [System.Serializable]
    public class MusicSetup
    {
        public MusicType musicType;
        public AudioClip audioClip;
    }

    [System.Serializable]
    public class SFXSetup
    {
        public SFXType sfxType;
        public AudioClip audioClip;
    }
}
