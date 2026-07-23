using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeParameterName = "MasterVolume";

    [Header("Image switch")]
    public Image buttonImage;
    public Sprite normalSprite;
    public Sprite mutedSprite;

    private bool _isMuted = false;
    private float _lastVolume = 0f;

    public void Mute()
    {
        Debug.Log("MUTE FOI CHAMADO!");

        _isMuted = !_isMuted;

        if (_isMuted)
        {
            audioMixer.GetFloat(volumeParameterName, out _lastVolume);
            audioMixer.SetFloat(volumeParameterName, -80f);
            buttonImage.sprite = mutedSprite;
        }
        else
        {
            audioMixer.SetFloat(volumeParameterName, _lastVolume);
            buttonImage.sprite = normalSprite;
        }
    }
}