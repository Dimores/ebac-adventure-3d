using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string volumeParameterName = "MasterVolume";

    [Header("Image switch")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite mutedSprite;

    private bool _isMuted = false;
    private float _lastVolume = 1f;

    private void Start()
    {
        _isMuted = PlayerPrefs.GetInt(volumeParameterName + "_IsMuted", 0) == 1;
        _lastVolume = PlayerPrefs.GetFloat(volumeParameterName, 1f);

        ApplyCurrentState();
    }

    public void Mute()
    {
        _isMuted = !_isMuted;

        if (_isMuted)
        {
            _lastVolume = PlayerPrefs.GetFloat(volumeParameterName, 1f);
        }

        PlayerPrefs.SetInt(volumeParameterName + "_IsMuted", _isMuted ? 1 : 0);
        PlayerPrefs.Save();

        ApplyCurrentState();
    }

    private void ApplyCurrentState()
    {
        if (_isMuted)
        {
            audioMixer.SetFloat(volumeParameterName, -80f);
        }
        else
        {
            ApplyVolumeToMixer(_lastVolume);
        }

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (buttonImage != null)
        {
            buttonImage.sprite = _isMuted ? mutedSprite : normalSprite;
        }
    }

    private void ApplyVolumeToMixer(float sliderValue)
    {
        if (sliderValue <= 0.0001f)
        {
            audioMixer.SetFloat(volumeParameterName, -80f);
        }
        else
        {
            float volumeInDb = Mathf.Log10(sliderValue) * 20f;
            volumeInDb = Mathf.Lerp(-80f, 10f, (volumeInDb + 80f) / 80f);
            audioMixer.SetFloat(volumeParameterName, volumeInDb);
        }
    }
}