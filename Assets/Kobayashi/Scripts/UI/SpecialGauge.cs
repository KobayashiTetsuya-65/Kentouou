using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

public class SpecialGauge : MonoBehaviour,IPointerClickHandler
{
    [Header("�e�I�u�W�F�N�g")]
    [SerializeField] private RectTransform _gauge;
    [Header("�Q�[�W�̌�����")]
    [Tooltip("���̉摜"), SerializeField] private GameObject _gaugeBack;
    [Tooltip("��O�̉摜"),SerializeField] private GameObject _gaugeFront;
    [Header("���l�ݒ�")]
    [Tooltip("�ŏ��̓����x"), SerializeField, Range(0f, 1f)] private float _alphaStart = 0.2f;
    [Tooltip("�N���b�N��"), SerializeField, Range(1,10)] private int _maxClick = 5;
    [Tooltip("����܂ł̕b��"), SerializeField] private float _duration = 0.5f;
    [Tooltip("���󂷂�܂ł̎���"),SerializeField] private float _timer = 10;
    [Header("�͈͐ݒ�")]
    [SerializeField] private float _minX = -750f;
    [SerializeField] private float _maxX = 750f;
    [SerializeField] private float _minY = -450f;
    [SerializeField] private float _maxY = 450f;
    private Image _backImage,_frontImage;
    private int _currentClick = 0;
    private float _increase,_randomX,_randomY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _backImage = _gaugeBack.GetComponent<Image>();
        _frontImage = _gaugeFront.GetComponent<Image>();
        _backImage.color = new Color(1,1,1,_alphaStart);
        _frontImage.fillAmount = 0;
        _increase = 1f / (float)_maxClick;
        StartCoroutine(BreakTimer());
        _randomX = Random.Range(_minX, _maxX);
        _randomY = Random.Range(_minY, _maxY);
        _gauge.anchoredPosition = new Vector2(_randomX, _randomY);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _currentClick++;
        _frontImage.fillAmount += _increase;
        if(_currentClick >= _maxClick)
        {
            StartCoroutine(BreakGauge());
        }
        else
        {
            _randomX = Random.Range(_minX, _maxX);
            _randomY = Random.Range(_minY, _maxY);
            _gauge.anchoredPosition = new Vector2(_randomX, _randomY);
        }
    }
    /// <summary>
    /// �K�E�Q�[�W�����܂������o������
    /// </summary>
    /// <returns></returns>
    private IEnumerator BreakGauge()
    {
        GameManager.Instance._gamePhase = InGamePhase.Direction;
        Sequence seq = DOTween.Sequence();
        for(int i = 0;i < _maxClick - 1; i++)
        {
            seq.Append(_frontImage.DOColor(Color.yellow, 0.1f));
            seq.Append(_frontImage.DOColor(Color.white, 0.1f));
            seq.Append(_frontImage.DOColor(Color.red, 0.1f));
        }
        seq.Join(_gauge.DOShakeAnchorPos(0.3f * (_maxClick - 1), 10f, 10, 90f, false, true));
        seq.Join(_gauge.DOScale(3f, 0.1f).SetEase(Ease.OutBack));
        seq.Append(_gauge.DOScale(1f, 0.2f).SetEase(Ease.InOutCubic));
        yield return seq.WaitForCompletion();
        GameManager.Instance._gamePhase = InGamePhase.Attack;
        _frontImage.DOFade(0f, 0.2f);
        _backImage.DOFade(0f, 0.2f);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    /// <summary>
    /// ���Ԑ؂�łȂ��Ȃ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator BreakTimer()
    {
        yield return new WaitForSeconds(_timer);
        _frontImage.DOFade(0f, 0.2f);
        _backImage.DOFade(0f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        GameManager.Instance._spcialCreate = false;
        Destroy(gameObject);
    }
}
