using DG.Tweening;
using UnityEngine;

public class TextFadeLoop : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [Tooltip("‚©‚©‚éŽžŠÔ"),SerializeField] private float _fadeDuration = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.DOFade(1f,_fadeDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1,LoopType.Yoyo);
    }
}
