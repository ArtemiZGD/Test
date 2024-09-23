using System;

public class AudioConstants
{
    public enum MixerType
    {
        Master,
        Sounds,
        Music
    }

    public const float MinVolume = -80f;
    
    private const string MasterVolume = nameof(MasterVolume);
    private const string SoundsVolume = nameof(SoundsVolume);
    private const string MusicVolume = nameof(MusicVolume);

    public static string GetMixerName(MixerType mixerType)
    {
        return mixerType switch
        {
            MixerType.Master => MasterVolume,
            MixerType.Sounds => SoundsVolume,
            MixerType.Music => MusicVolume,
            _ => throw new ArgumentOutOfRangeException(nameof(mixerType), mixerType, null)
        };
    }
}
