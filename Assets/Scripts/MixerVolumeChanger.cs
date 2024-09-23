using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerVolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioConstants.MixerType _mixerType;
    [SerializeField] private AudioMuter _audioMuter;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        if (_audioMuter.IsMuted == false)
        {
            _audioMixer.SetFloat(AudioConstants.GetMixerName(_mixerType), GetAudioValue(volume));
        }
    }

    private float GetAudioValue(float sliderValue)
    {
        if (sliderValue == 0)
        {
            sliderValue = float.Epsilon;
        }

        return Mathf.Log10(sliderValue) * 20;
    }
}
