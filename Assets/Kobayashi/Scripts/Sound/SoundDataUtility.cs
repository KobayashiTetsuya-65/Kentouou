using UnityEditor.PackageManager;
using UnityEngine;

public static class SoundDataUtility
{
    public static class KeyConfig
    {
        public static class Se
        {
            public static readonly string Punch = "Punch";
            public static readonly string Damaged = "Damaged";
            public static readonly string Taiko = "Taiko";
            public static readonly string Gong = "Gong";
            public static readonly string Finish = "Finish";
            public static readonly string Critical = "Critical";
            public static readonly string UseSpecial = "UseSpecial";
            public static readonly string Biribiri = "Biribiri";
            public static readonly string Charge = "Charge";
        }

        public static class Bgm
        {
            public static readonly string InGame = "InGame";
        }
    }

    public enum SoundType
    {
        Bgm = 0,
        Se = 1
    }

    public static void PrepareAudioSource(this AudioSource source, SoundData soundData)
    {
        source.playOnAwake = soundData.PlayOnAwake;
        source.loop = soundData.IsLoop;
        source.clip = soundData.Clip;
        source.volume = soundData.Volume;
    }
}
