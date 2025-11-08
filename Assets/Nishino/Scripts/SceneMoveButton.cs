using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneMoveButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private string _sceneName;
    [SerializeField] private SceneDivision _moveScene;
    private bool _push;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button.onClick.AddListener(SceneChange);
        _image.color = new Color(0, 0, 0, 0);
        _image.gameObject.SetActive(false);
        _push = false;
    }
    void SceneChange()
    {
        if (_push)return;
        _image.gameObject.SetActive(true);
        _image.DOFade(1f, _duration)
            .OnComplete(() =>
            {
                GameManager.Instance.CurrentScene = _moveScene;
                SceneManager.LoadScene(_sceneName);
            });
        _push = true;
    }
}
