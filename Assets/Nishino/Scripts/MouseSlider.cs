using UnityEngine;
using UnityEngine.UI;

public class MouseSlider : MonoBehaviour
{
    //[Header("マウス感度スライダー")]
    //public Slider sensitivitySlider;

    //private float savedValue;

    //private void Start()
    //{
    //    savedValue = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
    //    sensitivitySlider.SetValueWithoutNotify(savedValue);
    //    if (GlobalCursor.Instance != null)
    //    {
    //        GlobalCursor.Instance.SetSensitivity(savedValue);
    //    }
    //    sensitivitySlider.onValueChanged.AddListener((value) =>
    //    {
    //        if(GlobalCursor.Instance != null)
    //            GlobalCursor.Instance.SetSensitivity(value);
    //        PlayerPrefs.SetFloat("MouseSensitivity", value);
    //        PlayerPrefs.Save();
    //    });
    //}
}
