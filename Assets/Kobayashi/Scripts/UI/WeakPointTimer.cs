using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeakPointTimer : MonoBehaviour
{
    [Header("�C���[�W�ݒ�")]
    [Tooltip("�~�`�Q�[�W�̌�����"), SerializeField] private Image _gaugeImage;
    [Header("���l�ݒ�")]
    [Tooltip("��������"), SerializeField] private float _duration = 5.0f;
    private void Start()
    {
        StartTimer();
    }
    public void StartTimer()
    {
        _gaugeImage.fillAmount = 1f;
        _gaugeImage.DOFillAmount(0f, _duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnTimerEnd);
    }
    private void OnTimerEnd()
    {
        Debug.Log("���Ԑ؂�");
        GameManager.Instance.Miss = true;
        Destroy(gameObject);
    }
}
