using UnityEngine;
using UnityEngine.UI;

public class MouseSlider : MonoBehaviour
{
    [Header("マウス感度スライダー")]
    public Slider sensitivitySlider;

    [Header("現在の感度")]
    public float sensitivity = 1f;

    private void Start()
    {
        if (sensitivitySlider != null)
        {
            // 🔹イベントを最初に登録
            sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

            // 🔹スライダーの現在値を反映
            OnSensitivityChanged(sensitivitySlider.value);
        }
        else
        {
            Debug.LogWarning("感度変更");
        }
    }

    // 🔹この関数の形が超重要！
    public void OnSensitivityChanged(float value)
    {
        sensitivity = value;
        Debug.Log("感度変更: " + sensitivity);
    }
}
