using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class CreditButton : MonoBehaviour
{
    [SerializeField] Image _panel;
    [SerializeField] Image _panelButton;
    [SerializeField] Button _button;
    [SerializeField] float _duration = 0.2f;
    [SerializeField] bool _show;

    private void Start()
    {
        _button.onClick.AddListener(ShowConfigPanel);
    }
    void ShowConfigPanel()
    {
        if (_show)
        {
            _panel.DOFade(0.5f, _duration)
                .OnComplete(() => _panel.gameObject.SetActive(_show));
            if(_panelButton != null)
                _panelButton.gameObject.SetActive(!_show);
        }
        else
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(_panel.DOFade(0f, _duration));
            seq.AppendCallback(() => _panel.gameObject.SetActive(_show));
            if (_panelButton != null)
                seq.AppendCallback(() => _panelButton.gameObject.SetActive(!_show));
        }
    }
}