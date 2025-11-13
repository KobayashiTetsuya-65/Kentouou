using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

public class SpecialGauge : MonoBehaviour,IPointerClickHandler
{
    [Header("親オブジェクト")]
    [SerializeField] private RectTransform _gauge;
    [Header("ゲージの見た目")]
    [Tooltip("後ろの画像"), SerializeField] private GameObject _gaugeBack;
    [Tooltip("手前の画像"),SerializeField] private GameObject _gaugeFront;
    [Header("数値設定")]
    [Tooltip("最初の透明度"), SerializeField, Range(0f, 1f)] private float _alphaStart = 0.2f;
    [Tooltip("クリック回数"), SerializeField, Range(1,10)] private int _maxClick = 5;
    [Tooltip("自壊するまでの時間"),SerializeField] private float _timer = 10;
    [Header("範囲設定")]
    [SerializeField] private float _minX = -750f;
    [SerializeField] private float _maxX = 750f;
    [SerializeField] private float _minY = -450f;
    [SerializeField] private float _maxY = 450f;
    private SpecialGaugeVertical _verticalGauge;
    private Image _backImage,_frontImage;
    private int _currentClick = 0;
    private float _increase,_randomX,_randomY;
    private bool _isAction = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _backImage = _gaugeBack.GetComponent<Image>();
        _frontImage = _gaugeFront.GetComponent<Image>();
        _verticalGauge = FindAnyObjectByType<SpecialGaugeVertical>();
        _backImage.color = new Color(1,1,1,_alphaStart);
        _frontImage.fillAmount = 1;
        _increase = 1f / (float)_maxClick;
        StartCoroutine(BreakTimer());
        _randomX = Random.Range(_minX, _maxX);
        _randomY = Random.Range(_minY, _maxY);
        _gauge.anchoredPosition = new Vector2(_randomX, _randomY);
        _isAction = false;
        _gaugeFront.transform.localScale = Vector3.zero;
        _gaugeBack.transform.localScale = Vector3.zero;
        _gaugeFront.transform.DOScale(Vector3.one * 1.2f, 0.3f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                _gaugeFront.transform.DOScale(Vector3.one, 0.1f);
            });
        _gaugeBack.transform.DOScale(Vector3.one * 1.2f, 0.3f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                _gaugeBack.transform.DOScale(Vector3.one, 0.1f);
            });
        AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Special);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _currentClick++;
        _verticalGauge.ValueChange(_increase);
        if (!_isAction)
        {
            if (_currentClick >= _maxClick)
            {
                StartCoroutine(BreakGauge());
                _isAction = true;
            }
            else
            {
                AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Charge);
                _randomX = Random.Range(_minX, _maxX);
                _randomY = Random.Range(_minY, _maxY);
                _gauge.anchoredPosition = new Vector2(_randomX, _randomY);
            }
        }
    }
    /// <summary>
    /// 必殺ゲージがたまった演出＆処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator BreakGauge()
    {
        StopAnimation();
        GameManager.Instance.GamePhase = InGamePhase.Direction;
        Sequence seq = DOTween.Sequence();
        AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Biribiri);
        for (int i = 0;i < _maxClick - 1; i++)
        {
            seq.Append(_frontImage.DOColor(Color.yellow, 0.1f));
            seq.Append(_frontImage.DOColor(Color.white, 0.1f));
            seq.Append(_frontImage.DOColor(Color.red, 0.1f));
        }
        StopAnimation();
        seq.Join(_gauge.DOShakeAnchorPos(0.3f * (_maxClick - 1), 10f, 10, 90f, false, true));
        seq.AppendCallback(() =>
        {
            AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.UseSpecial);
        });
        StopAnimation();
        seq.Join(_gauge.DOScale(3f, 0.15f).SetEase(Ease.OutBack));
        seq.Append(_gauge.DOScale(1f, 0.2f).SetEase(Ease.InOutCubic));
        yield return seq.WaitForCompletion();
        StopAnimation();
        GameManager.Instance.GamePhase = InGamePhase.Attack;
        _frontImage.DOFade(0f, 0.2f);
        _backImage.DOFade(0f, 0.2f);
        yield return new WaitForSeconds(0.3f);
        DOTween.Kill(_frontImage);
        DOTween.Kill(_backImage);
        DOTween.Kill(_gauge);
        DOTween.Kill(gameObject);
        Destroy(gameObject);
    }
    /// <summary>
    /// 時間切れでなくなる
    /// </summary>
    /// <returns></returns>
    private IEnumerator BreakTimer()
    {
        yield return new WaitForSeconds(_timer / 2);
        if(!_isAction)StartCoroutine(FlashingChange(0.3f));
        yield return new WaitForSeconds(_timer / 4);
        if (!_isAction)StartCoroutine(FlashingChange(0.15f));
        yield return new WaitForSeconds(_timer / 4);
        if (!_isAction)
        {
            _frontImage.DOFade(0f, 0.2f);
            _backImage.DOFade(0f, 0.2f);
            yield return new WaitForSeconds(0.2f);
            GameManager.Instance._spcialCreate = false;
            DOTween.Kill(_frontImage);
            DOTween.Kill(_backImage);
            DOTween.Kill(_gauge);
            DOTween.Kill(gameObject);
            Destroy(gameObject);
        }
    }
    private IEnumerator FlashingChange(float duration)
    {
        StopAnimation();
        yield return null;
        Flashing(duration);
    }
    private void Flashing(float duration)
    {
        _frontImage.DOFade(0.1f, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .SetId("FlashingFront");
        _backImage.DOFade(0.1f, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .SetId("FlashingBack");
    }
    private void StopAnimation()
    {
        DOTween.Kill("FlashingFront");
        DOTween.Kill("FlashingBack");
        Color c1 = _frontImage.color;
        Color c2 = _backImage.color;
        c1.a = 1f;
        c2.a = _alphaStart;
        _frontImage.color = c1;
        _backImage.color = c2;
    }
}
