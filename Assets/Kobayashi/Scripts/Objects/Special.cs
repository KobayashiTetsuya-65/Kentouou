using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Special : MonoBehaviour,IPointerClickHandler
{
    [Header("オブジェクト取得")]
    [Tooltip("ゲージ"), SerializeField] private Image _gauge;
    [Tooltip("必殺弱点"), SerializeField] private Image _special;
    [Tooltip("必殺弱点"), SerializeField] private RectTransform _specialRectTr;
    [Tooltip("エネミー"),SerializeField] private RectTransform _enemyRectTr;

    [Header("数値設定")]
    [Tooltip("制限時間"),SerializeField] private float _duration = 7f;
    [Tooltip("ダメージ"), SerializeField] private int _damage = 5;

    private bool _isFlashing = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _specialRectTr.anchoredPosition = _enemyRectTr.anchoredPosition;

        _gauge.DOFade(1f, 0.4f);
        StartTimer();
        _specialRectTr
            .DOScale(1.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    void Update()
    {
        if (_gauge.fillAmount <= 0.3f && !_isFlashing)
        {
            _isFlashing = true;
            FlashWarning();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.Hit = true;
        GameManager.Instance.Damage = _damage;
        _specialRectTr.DOKill(); // 鼓動を止める
        Sequence seq = DOTween.Sequence();
        seq.Append(_specialRectTr.DOScale(1.2f, 0.05f));
        seq.Append(_specialRectTr.DOScale(1f, 0.1f));
    }
    private void OnDisable()
    {
        if (GlobalCursor.Instance != null)
        {
            GlobalCursor.Instance.SetNormalCursor();
        }
    }
    private void StartTimer()
    {
        _gauge.fillAmount = 1f;
        _gauge.DOFillAmount(0f, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnTimerEnd);
    }
    private void OnTimerEnd()
    {
        GameManager.Instance.IsSpecialFinish = true;
        Destroy(gameObject);
    }
    /// <summary>
    /// 点滅
    /// </summary>
    private void FlashWarning()
    {
        _specialRectTr
            .DOScale(1.3f, 0.15f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);

        _gauge
            .DOFade(0.3f, 0.15f)
            .SetLoops(-1, LoopType.Yoyo);
        _special
            .DOFade(0.3f, 0.15f)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
