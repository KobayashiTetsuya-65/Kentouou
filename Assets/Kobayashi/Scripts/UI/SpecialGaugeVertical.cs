using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class SpecialGaugeVertical : MonoBehaviour
{
    [SerializeField] private Image _img;
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _specialTime = 7f;
    private float _currentAmount = 0,_hue = 0,_brightness = 1f;
    private Tween _fillTween,_blinkTween, _rainbowTween, _brightnessTween;
    private Color _purple;
     private void Start()
    {
        _fillImage.fillAmount = 0;
        _purple = new Color(1f, 0f, 1f, 1f);
        _fillImage.color = _purple;
    }
    private void OnEnable()
    {
        _fillImage.fillAmount = 0f;
        _currentAmount = 0;
        _fillImage.rectTransform.localScale = Vector3.one * 0.2f;
        _fillImage.rectTransform.DOScale(1.2f, 0.2f)
            .SetEase(Ease.Linear)
            .OnComplete(() => _fillImage.rectTransform.DOScale(1f, 1f));
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
                    StartFlowEffect();
                }
            });
    }
    /// <summary>
    /// 必殺技使用中
    /// </summary>
    public void DecreaseGauge()
    {
        DG.Tweening.Sequence seq = DOTween.Sequence();
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
                StopFlowEffect();
                if (_blinkTween != null && _blinkTween.IsActive())
                {
                    _blinkTween.Kill();
                }
                _fillImage.color = _purple;
            }));
        
    }
    /// <summary>
    /// 点滅アニメーション開始
    /// </summary>
    private void StartBlink()
    {
        if (_blinkTween != null && _blinkTween.IsActive()) return;

        _blinkTween = DOTween.ToAlpha(
            () => _img.color,
            c => _img.color = c,
            0.3f, 0.3f
        )
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
        DOTween.ToAlpha(
            () => _fillImage.color,
            c => _fillImage.color = c,
            0.3f, 0.3f)
            .SetLoops(1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

    }
    /// <summary>
    /// 満タン時のカラフル演出
    /// </summary>
    private void StartFlowEffect()
    {
        if (_rainbowTween != null && _rainbowTween.IsActive()) return;

        _hue = 0f;
        _brightness = 1f;

        _rainbowTween = DOTween.To(() => _hue, x => {
            _hue = x;
            UpdateGaugeColor();
        }, 1f, 1.5f)
        .SetLoops(-1, LoopType.Restart)
        .SetEase(Ease.Linear);

        _brightnessTween = DOTween.To(() => _brightness, x => {
            _brightness = x;
            UpdateGaugeColor();
        }, 0.6f, 0.5f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.InOutSine);
    }
    private void UpdateGaugeColor()
    {
        Color col = Color.HSVToRGB(_hue % 1f, 1f, _brightness);
        _fillImage.color = new Color(col.r, col.g, col.b, _fillImage.color.a); // αは維持
    }
    /// <summary>
    /// 光の流れ停止
    /// </summary>
    private void StopFlowEffect()
    {
        _rainbowTween?.Kill();
        _brightnessTween?.Kill();
        _fillImage.color = _purple;
        _img.color = Color.white;
    }
}
