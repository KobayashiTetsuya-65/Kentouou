using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Audio/SoundData", order = 1)]
public class SoundDataBaseSo : ScriptableObject
{
    [SerializeField]
    private List<SoundData> _soundMap = new List<SoundData>();

    public SoundData GetSoundData(string key)
    {
        return _soundMap.FirstOrDefault(x => x.Key == key);
    }
}

[Serializable]
public class SoundData
{
    [SerializeField, Header("key���w��")]
    private string _key;

    [SerializeField, Header("�T�E���h�̎�ނ�ݒ�")]
    private SoundDataUtility.SoundType _type;

    [SerializeField, Header("���[�v�Đ������邩")]
    private bool _isLoop;

    [SerializeField, Header("���Đ������邩")]
    private bool _playOnAwake;

    [SerializeField, Range(0f,1f),Header("���ʒ���")]
    private float _volume;

    [SerializeField, Header("AudioClip���A�T�C��")]
    private AudioClip _clip;

    public string Key => _key;
    public SoundDataUtility.SoundType Type => _type;
    public bool IsLoop => _isLoop;
    public bool PlayOnAwake => _playOnAwake;
    public float Volume => _volume;
    public AudioClip Clip => _clip;
}