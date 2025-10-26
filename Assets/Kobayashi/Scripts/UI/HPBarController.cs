using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HPBarController : MonoBehaviour
{
    [Header("�Q��UI")]
    [Tooltip("HP�o�["), SerializeField] private Slider _hpSlider;
    [Tooltip("�x��ĕ\�������o�["), SerializeField] private Slider _delaySlider;
    [Header("�F�ݒ�")]
    [Tooltip("�����Ƃ�"), SerializeField] private Color _healthyColor = Color.green;
    [Tooltip("�����炢"),SerializeField] private Color _warningColor = Color.yellow;
    [Tooltip("�m��"),SerializeField] private Color _dangerColor = Color.red;
    [Header("���l�ݒ�")]
    [Tooltip("�ő�HP"), SerializeField] private float _maxHP = 10f;
    [Tooltip("�ǂ������x"), SerializeField] private float _delayDuration = 30f;
    [Tooltip("�t�F�[�h����"), SerializeField] private float _fadeDuration = 0.4f;
    private Image _fillImage,_backImage;
    private float _currentHP,_normalizedHP;
    /// <summary>
    /// HP�o�[�̏�����
    /// </summary>
    public void HPBarReset()
    {
        _currentHP = _maxHP;
        _hpSlider.maxValue = _currentHP;
        _delaySlider.maxValue = _currentHP;
        _hpSlider.value = _currentHP;
        _delaySlider.value = _currentHP;
        _fillImage = _hpSlider.fillRect.GetComponent<Image>();
        _fillImage.color = _healthyColor;
        _backImage = _delaySlider.fillRect.GetComponent<Image>();
        _fillImage.enabled = true;
        _backImage.enabled = true;
    }
    /// <summary>
    /// �_���[�W���󂯃Q�[�W�ɔ��f
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        _currentHP = Mathf.Max(_currentHP - damage, 0f);
        _hpSlider.value = _currentHP;
        _normalizedHP = _currentHP / _maxHP;
        UpdateColor(_normalizedHP);

        DOTween.Kill(_delaySlider);
        DOTween.Kill(_backImage);

        if(_currentHP > 0f)
        {
            _delaySlider.DOValue(_hpSlider.value, _delayDuration).SetEase(Ease.OutCubic);
            _backImage.DOFade(1f, 0.1f);
        }
        else
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(_backImage.DOColor(Color.red, 0.1f).SetLoops(3, LoopType.Yoyo));
            seq.Append(_delaySlider.DOValue(0f, _fadeDuration).SetEase(Ease.OutCubic));
            seq.Join(_backImage.DOFade(0f, _fadeDuration));
            seq.Join(_fillImage.DOFade(0f, _fadeDuration));
            seq.OnComplete(() =>
            {
                _fillImage.enabled = false;
                _backImage.enabled = false;
                _fillImage.color = new Color(_fillImage.color.r, _fillImage.color.g, _fillImage.color.b, 1f);
                _backImage.color = new Color(_backImage.color.r, _backImage.color.g, _backImage.color.b, 1f);
            });
        }
        
    }
    /// <summary>
    /// HP�����ɉ����ăo�[�̐F��ω�
    /// </summary>
    /// <param name="normalizedHP"></param>
    private void UpdateColor(float normalizedHP)
    {
        if(normalizedHP > 0.5f)
        {
            _fillImage.color = Color.Lerp(_warningColor, _healthyColor, (normalizedHP - 0.5f) * 2f);
        }
        else
        {
            _fillImage.color = Color.Lerp(_dangerColor, _warningColor, normalizedHP * 2f);
        }
    }
}
