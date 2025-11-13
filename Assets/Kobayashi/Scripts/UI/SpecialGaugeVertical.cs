using UnityEngine;
using UnityEngine.UI;

public class SpecialGaugeVertical : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    private void Start()
    {
        _fillImage.fillAmount = 0;
    }
    private void OnEnable()
    {
        _fillImage.fillAmount = 0f;
    }
    /// <summary>
    /// FillAmountを変換
    /// </summary>
    /// <param name="value"></param>
    public void ValueChange(float value)
    {
        _fillImage.fillAmount += value;
        if(_fillImage.fillAmount >= 1)
        {
            //アニメーション作るかも
        }
    }
}
