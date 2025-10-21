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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetState();
    }

    // Update is called once per frame
    void Update()
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
}
