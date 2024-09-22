using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private const string MasterVolume = nameof(MasterVolume);
    private const string SoundsVolume = nameof(SoundsVolume);
    private const string MusicVolume = nameof(MusicVolume);

    [SerializeField] private AudioMixer _audioMixer;

    private float _masterVolume = 0;
    private float _soundsVolume = 0;
    private float _musicVolume = 0;
    private bool _isMuted = false;

    public void ToggleMute()
    {
        _isMuted = !_isMuted;

        if (_isMuted)
        {
            _audioMixer.SetFloat(MasterVolume, AudioConstants.MinVolume);
        }
        else
        {
            _audioMixer.SetFloat(MasterVolume, _masterVolume);
        }
    }

    public void SetSoundsVolume(Slider slider)
    {
        _soundsVolume = GetSliderAudioValue(slider);
        UpdateAudio();
    }

    public void SetMusicVolume(Slider slider)
    {
        _musicVolume = GetSliderAudioValue(slider);
        UpdateAudio();
    }

    public void SetMasterVolume(Slider slider)
    {
        _masterVolume = GetSliderAudioValue(slider);
        UpdateAudio();
    }

    private float GetSliderAudioValue(Slider slider)
    {
        return Mathf.Log10(Mathf.Max(slider.value, float.Epsilon)) * 20;
    }

    private void UpdateAudio()
    {
        if (_isMuted == false)
        {
            _audioMixer.SetFloat(MasterVolume, _masterVolume);
            _audioMixer.SetFloat(SoundsVolume, _soundsVolume);
            _audioMixer.SetFloat(MusicVolume, _musicVolume);
        }
    }
}
