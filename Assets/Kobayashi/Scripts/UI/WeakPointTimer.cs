using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeakPointTimer : MonoBehaviour
{
    [Header("イメージ設定")]
    [Tooltip("円形ゲージの見た目"), SerializeField] private Image _gaugeImage;
    [Header("数値設定")]
    [Tooltip("制限時間"), SerializeField] private float _duration = 5.0f;
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
        Debug.Log("時間切れ");
        GameManager.Instance.Miss = true;
        Destroy(gameObject);
    }
}
