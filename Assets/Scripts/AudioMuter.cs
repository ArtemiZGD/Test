using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMuter : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _soundsSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Button _muteButton;

    private bool _isMuted = false;
    private float _masterVolume = 0;
    private float _soundsVolume = 0;
    private float _musicVolume = 0;

    public bool IsMuted => _isMuted;

    private void OnEnable()
    {
        _muteButton.onClick.AddListener(ToggleMute);
    }

    private void OnDisable()
    {
        _muteButton.onClick.RemoveListener(ToggleMute);
    }

    public void ToggleMute()
    {
        _isMuted = !_isMuted;

        if (_isMuted)
        {
            Mute(AudioConstants.MixerType.Master, out _masterVolume);
            Mute(AudioConstants.MixerType.Sounds, out _soundsVolume);
            Mute(AudioConstants.MixerType.Music, out _musicVolume);
        }
        else
        {
            Unmute(AudioConstants.MixerType.Master, _masterVolume);
            Unmute(AudioConstants.MixerType.Sounds, _soundsVolume);
            Unmute(AudioConstants.MixerType.Music, _musicVolume);
        }
    }

    private void Mute(AudioConstants.MixerType mixerType, out float mixerVolume)
    {
        _audioMixer.GetFloat(AudioConstants.GetMixerName(mixerType), out mixerVolume);
        _audioMixer.SetFloat(AudioConstants.GetMixerName(mixerType), AudioConstants.MinVolume);
    }

    private void Unmute(AudioConstants.MixerType mixerType, float mixerVolume)
    {
        _audioMixer.SetFloat(AudioConstants.GetMixerName(mixerType), mixerVolume);
    }
}
