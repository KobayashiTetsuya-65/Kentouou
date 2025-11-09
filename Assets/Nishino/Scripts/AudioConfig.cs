using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioConfig : MonoBehaviour 
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider seSlider;
    [SerializeField] Slider bgmSlider;
    float masterVal, bgmVal, seVal;

    private void Start()
    {
        masterSlider.onValueChanged.RemoveAllListeners();
        seSlider.onValueChanged.RemoveAllListeners();
        bgmSlider.onValueChanged.RemoveAllListeners();

        masterVal = PlayerPrefs.GetFloat("Master",1f);
        bgmVal = PlayerPrefs.GetFloat("BGM", 1f);
        seVal = PlayerPrefs.GetFloat("SE", 1f);

        masterSlider.SetValueWithoutNotify(masterVal);
        bgmSlider.SetValueWithoutNotify(bgmVal);
        seSlider.SetValueWithoutNotify(seVal);

        ApplyVolume("Master", masterVal);
        ApplyVolume("BGM", bgmVal);
        ApplyVolume("SE", seVal);

        masterSlider.onValueChanged.AddListener((value) => SaveAndApply("Master", value));
        bgmSlider.onValueChanged.AddListener((value) => SaveAndApply("BGM",value));
        seSlider.onValueChanged.AddListener((value) => SaveAndApply("SE",value));
    }
    private void SaveAndApply(string param,float value)
    {
        ApplyVolume(param, value);
        PlayerPrefs.SetFloat(param, value);
        PlayerPrefs.Save();
    }
    private void ApplyVolume(string param, float value)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(param, dB);
    }
}
