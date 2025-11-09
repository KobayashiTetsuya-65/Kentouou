using UnityEngine;
using UnityEngine.UI;

public class MouseSlider : MonoBehaviour
{
    [Header("マウス感度スライダー")]
    public Slider sensitivitySlider;

    [Header("現在の感度")]
    public float sensitivity = 1f;

    private float savedValue;

    private void Start()
    {
        if (sensitivitySlider != null)
        {
            savedValue = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
            sensitivitySlider.SetValueWithoutNotify(savedValue);
            OnSensitivityChanged(savedValue);

            sensitivitySlider.onValueChanged.AddListener((value) =>
            {
                OnSensitivityChanged(value);
                PlayerPrefs.SetFloat("MouseSensitivity", value);
                PlayerPrefs.Save();
            });
        }
    }

    private void OnSensitivityChanged(float value)
    {
        sensitivity = value;
    }
}
