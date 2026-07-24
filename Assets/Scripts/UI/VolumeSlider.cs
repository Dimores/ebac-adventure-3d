using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [Header("Configurações do Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private string volumeParameterName = "MusicVolume";

    [Header("Configurações do Slider")]
    [SerializeField] private Slider slider;

    private const float MinDb = -80f;
    private const float MaxDb = 10f;
    private const float DefaultVolume = 0.1f;

    private void Start()
    {
        slider.minValue = 0.0001f;
        slider.maxValue = 1f;

        bool isMuted = PlayerPrefs.GetInt(volumeParameterName + "_IsMuted", 0) == 1;
        float targetVolume;

        if (PlayerPrefs.HasKey(volumeParameterName))
        {
            targetVolume = PlayerPrefs.GetFloat(volumeParameterName);
        }
        else
        {
            targetVolume = DefaultVolume;
        }

        if (isMuted)
        {
            slider.value = slider.minValue;
            SetVolume(slider.minValue);
        }
        else
        {
            slider.value = targetVolume;
            SetVolume(targetVolume);
        }

        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        float volumeInDb;

        if (sliderValue <= 0.0001f)
        {
            volumeInDb = MinDb;
        }
        else
        {
            volumeInDb = Mathf.Log10(sliderValue) * 20f;
            volumeInDb = Mathf.Lerp(MinDb, MaxDb, (volumeInDb + 80f) / 80f);
        }

        audioMixer.SetFloat(volumeParameterName, volumeInDb);

        if (sliderValue > 0.0001f)
        {
            PlayerPrefs.SetFloat(volumeParameterName, sliderValue);
            PlayerPrefs.Save();
        }
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(SetVolume);
    }
}