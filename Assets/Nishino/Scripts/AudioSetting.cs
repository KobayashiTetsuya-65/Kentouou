using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioConfig : MonoBehaviour 
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Slider bgmSlider;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master",1f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 1f);
        seSlider.value = PlayerPrefs.GetFloat("SE", 1f);

        ApplyVolume("Master", masterSlider.value);
        ApplyVolume("BGM", bgmSlider.value);
        ApplyVolume("SE", seSlider.value);

        bgmSlider.onValueChanged.AddListener((value) =>
        {
            ApplyVolume("BGM",value);
            PlayerPrefs.SetFloat("BGM", value);
            PlayerPrefs.Save();
        });

        seSlider.onValueChanged.AddListener((value) =>
        {
            ApplyVolume("SE",value);
            PlayerPrefs.SetFloat("SE", value);
            PlayerPrefs.Save();
        });

        masterSlider.onValueChanged.AddListener((value) =>
        {
            ApplyVolume("Master", value);
            PlayerPrefs.SetFloat("Master", value);
            PlayerPrefs.Save();
        });
    }
    private void ApplyVolume(string param, float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(param, dB);
    }
}
