using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Special : MonoBehaviour,IPointerClickHandler
{
    [Header("オブジェクト取得")]
    [Tooltip("ゲージ"), SerializeField] private Image _gauge;
    [Tooltip("必殺弱点"), SerializeField] private RectTransform _specialRectTr;
    [Tooltip("エネミー"),SerializeField] private RectTransform _enemyRectTr;

    [Header("数値設定")]
    [Tooltip("制限時間"),SerializeField] private float _duration = 7f;
    [Tooltip("ダメージ"), SerializeField] private int _damage = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _specialRectTr.anchoredPosition = _enemyRectTr.anchoredPosition;
        StartTimer();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.Hit = true;
        GameManager.Instance.Damage = _damage;
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
}
