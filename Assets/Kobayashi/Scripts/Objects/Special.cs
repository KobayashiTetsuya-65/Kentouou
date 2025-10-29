using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Special : MonoBehaviour,IPointerClickHandler
{
    [Header("�I�u�W�F�N�g�擾")]
    [Tooltip("�Q�[�W"), SerializeField] private Image _gauge;
    [Tooltip("�K�E��_"), SerializeField] private RectTransform _specialRectTr;
    [Tooltip("�G�l�~�["),SerializeField] private RectTransform _enemyRectTr;

    [Header("���l�ݒ�")]
    [Tooltip("��������"),SerializeField] private float _duration = 7f;
    [Tooltip("�_���[�W"), SerializeField] private int _damage = 5;

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
