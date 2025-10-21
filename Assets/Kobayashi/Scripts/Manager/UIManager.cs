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
    [Tooltip("行動選択"), SerializeField] private GameObject _activeChosePanel;
    [Header("ボタン")]
    [Tooltip("攻撃"), SerializeField] private Button _attackButton;
    [Tooltip("防御"),SerializeField] private Button _defenseButton;
    [Tooltip("溜める"), SerializeField] private Button _accumulateButton;
    private bool _chose = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackButton.onClick.AddListener(Attack);
        _defenseButton.onClick.AddListener(Defense);
        _accumulateButton.onClick.AddListener(Accumulate);
    }

    /// <summary>
    /// UIの値をリセット
    /// </summary>
     public void ResetState()
    {
        _playerHP.value = 1;
        _enemeyHP.value = 1;
    }
    private void Attack()
    {
        Debug.Log("攻撃");
    }
    private void Defense()
    {
        Debug.Log("防御");
    }
    private void Accumulate()
    {
        Debug.Log("溜める");
    }
}
