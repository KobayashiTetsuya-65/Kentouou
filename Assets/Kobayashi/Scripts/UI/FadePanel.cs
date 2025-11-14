using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    [SerializeField] private Image _img;
    [SerializeField] private float _duration = 1f;
    private void Awake()
    {
        _img.color = Color.black;
        _img.DOFade(0f, _duration);
    }
}
