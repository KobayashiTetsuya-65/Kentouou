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
            public static readonly string Click = "Click";
            public static readonly string Special = "Special";
            public static readonly string Defence = "Defence";
            public static readonly string TurnThePage = "TurnThePage";
            public static readonly string ManScream = "ManScream";
            public static readonly string WomanScream = "WomanScream";
            public static readonly string FallDown = "FallDown";
        }

        public static class Bgm
        {
            public static readonly string Title = "Title";
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
