using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    [Tooltip("�ǂ������x"), SerializeField] private float _delaySpeed = 30f;
    private Coroutine _delayCoroutine;
    private Image _fillImage;
    private float _currentHP,_normalizedHP,_startFill,_target;
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
    }
    /// <summary>
    /// �_���[�W���󂯃Q�[�W�ɔ��f
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        _currentHP = Mathf.Max(_currentHP - damage, 0);
        _hpSlider.value = _currentHP;
        _normalizedHP = _currentHP / _maxHP;
        UpdateColor(_normalizedHP);
        if(_delayCoroutine != null)StopCoroutine(_delayCoroutine);
        _delayCoroutine = StartCoroutine(UpdateDelayBar());
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
    /// <summary>
    /// �x���o�[��ǂ������鏈��
    /// </summary>
    /// <param name="targetFill"></param>
    /// <returns></returns>
    private IEnumerator UpdateDelayBar()
    {
        while (_delaySlider.value > _hpSlider.value)
        {
            _delaySlider.value -= Time.deltaTime * _delaySpeed;
            yield return null;
        }
        _delaySlider.value = _hpSlider.value;
    }
}
