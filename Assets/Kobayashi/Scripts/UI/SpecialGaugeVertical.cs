using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpecialGaugeVertical : MonoBehaviour
{
    [SerializeField] private GameObject _obj;
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _specialTime = 7f;
    private float _currentAmount = 0;
    private Tween _fillTween,_blinkTween;
    private void Start()
    {
        _fillImage.fillAmount = 0;
    }
    private void OnEnable()
    {
        _fillImage.fillAmount = 0f;
        _currentAmount = 0;
        _fillImage.rectTransform.localScale = Vector3.one * 0.2f;
        _fillImage.rectTransform.DOScale(1.2f, 0.2f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _fillImage.rectTransform.DOScale(1f, 1f);
            });
    }
    /// <summary>
    /// FillAmountを変換
    /// </summary>
    /// <param name="value"></param>
    public void ValueChange(float value)
    {
        _currentAmount += value;
        if (_fillTween != null && _fillTween.IsActive()) _fillTween.Kill();
        _fillTween = _fillImage.DOFillAmount(_currentAmount, _duration)
            .OnComplete(() =>
            {
                if (Mathf.Approximately(_fillImage.fillAmount, 1f))
                {
                    ShakeGauge();
                }
            });
    }
    /// <summary>
    /// 必殺技使用中
    /// </summary>
    public void DecreaseGauge()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_fillImage.DOFillAmount(0, _specialTime)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                if (_fillImage.fillAmount <= 0.33f && (_blinkTween == null || !_blinkTween.IsActive()))
                {
                    StartBlink();
                }
            })
            .OnComplete(() =>
            {
                if (_blinkTween != null && _blinkTween.IsActive())
                {
                    _blinkTween.Kill();
                    _fillImage.color = new Color(1, 1, 1, 1);
                }
            }));
        
    }
    /// <summary>
    /// 点滅アニメーション開始
    /// </summary>
    private void StartBlink()
    {
        if (_blinkTween != null && _blinkTween.IsActive()) return;

        _blinkTween = _fillImage.DOFade(0.3f, 0.3f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    /// <summary>
    /// 満タン時のプルプル演出
    /// </summary>
    private void ShakeGauge()
    {
        _obj.transform
            .DOPunchScale(new Vector3(0f,0.5f,0f), 1.2f) // (時間, 強さ, 振動回数, 乱数角度, 相対スケール)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                _obj.transform.localScale = Vector3.one;
            });
    }
}
