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
    [SerializeField] private ParticleSystem _ps;
    private bool _push;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button.onClick.AddListener(SceneChange);
        _push = false;
    }
    void SceneChange()
    {
        if (_push)return;
        AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Click);
        _image.gameObject.SetActive(true);
        if (_ps != null)
        {
            ParticleSystem.MainModule main = _ps.main;
            main.startColor = new Color(1, 1, 1, 0);
        }
        _image.DOFade(1f, _duration)
            .OnComplete(() =>
            {
                GameManager.Instance.CurrentScene = _moveScene;
                SceneManager.LoadScene(_sceneName);
            });
        _push = true;
    }
    private void OnDisable()
    {
        if (GlobalCursor.Instance != null)
        {
            GlobalCursor.Instance.SetNormalCursor();
        }
    }
}
