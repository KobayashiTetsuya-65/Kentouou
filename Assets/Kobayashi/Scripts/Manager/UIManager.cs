using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HPゲージ")]
    [Tooltip("プレイヤーHPゲージ"), SerializeField] private Slider _playerHP;
    [Tooltip("エネミーHPゲージ"), SerializeField] private Slider _enemeyHP;
    [Header("キャラクター")]
    [Tooltip("プレイヤー"),SerializeField] private Image _playerImage;
    [Tooltip("エネミー"),SerializeField] private Image _enemyImage;
    [Header("パネル")]
    [Tooltip("スタートパネル"),SerializeField] private GameObject _startPanel;
    [Tooltip("敵弱点パネル"),SerializeField] private GameObject _weakPointPanelE;
    [SerializeField] private RectTransform _weakPanelErtr;
    [Tooltip("自分弱点パネル"),SerializeField] private GameObject _weakPointPanelP;
    [SerializeField] private RectTransform _weakPanelPrtr;
    [Header("弱点")]
    [Tooltip("弱点画像"), SerializeField] private GameObject _weakPointPrefab;
    private RectTransform _weakRect;
    private GameObject _weakPoint;
    private float _width, _height, _randomX, _randomY;
    private bool _chose = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    /// <summary>
    /// UIの値をリセット
    /// </summary>
     public void ResetState()
    {
        _playerHP.value = 1;
        _enemeyHP.value = 1;
    }
    /// <summary>
    /// 弱点を生成
    /// </summary>
    /// <param name="isEnemy"></param>
    public void SpawnWeakPoint(bool isEnemy)
    {
        if (isEnemy)
        {
            _weakPoint = Instantiate(_weakPointPrefab, _weakPointPanelE.transform);
            _width = _weakPanelErtr.rect.width;
            _height = _weakPanelErtr.rect.height;
        }
        else
        {
            _weakPoint = Instantiate(_weakPointPrefab,_weakPointPanelP.transform);
            _width = _weakPanelPrtr.rect.width;
            _height = _weakPanelPrtr.rect.height;
        }
        _randomX = Random.Range(-_width / 2f, _width / 2f);
        _randomY = Random.Range(-_height / 2f, _height / 2f);
        _weakRect = _weakPoint.GetComponent<RectTransform>();
        _weakRect.anchoredPosition = new Vector2(_randomX, _randomY);
    }
    public void InGameStart(bool isClicked)
    {
        _startPanel.SetActive(!isClicked);
    }
    private void Attack()
    {
        Debug.Log("攻撃");
    }
}
