using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    [Header("参照UI")]
    [Tooltip("HPバー"), SerializeField] private Slider _hpSlider;
    [Tooltip("遅れて表示されるバー"), SerializeField] private Slider _delaySlider;
    [Header("色設定")]
    [Tooltip("高いとき"), SerializeField] private Color _healthyColor = Color.green;
    [Tooltip("中くらい"),SerializeField] private Color _warningColor = Color.yellow;
    [Tooltip("瀕死"),SerializeField] private Color _dangerColor = Color.red;
    [Header("数値設定")]
    [Tooltip("最大HP"), SerializeField] private float _maxHP = 10f;
    [Tooltip("追いつく速度"), SerializeField] private float _delaySpeed = 30f;
    private Coroutine _delayCoroutine;
    private Image _fillImage;
    private float _currentHP,_normalizedHP,_startFill,_target;
    /// <summary>
    /// HPバーの初期化
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
    /// ダメージを受けゲージに反映
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
    /// HP割合に応じてバーの色を変化
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
    /// 遅延バーを追いつかせる処理
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
