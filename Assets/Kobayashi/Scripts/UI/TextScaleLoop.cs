using DG.Tweening;
using UnityEngine;

public class TextScaleLoop : MonoBehaviour
{
    [SerializeField] private RectTransform _target;
    [Tooltip("ÉXÉPÅ[ÉãÇÃç≈ëÂ"),SerializeField] private float _scaleUp = 1.4f;
    [Tooltip("Ç©Ç©ÇÈéûä‘"),SerializeField] private float _duration = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _target.localScale = Vector3.one;
        _target.DOScale(_scaleUp, _duration)
               .SetEase(Ease.InOutSine)
               .SetLoops(-1, LoopType.Yoyo);
    }
}
