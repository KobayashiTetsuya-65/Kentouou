using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// プレイヤーのステータス管理
/// </summary>
public class Player : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] public int PlayerMaxHP;

    [Header("HPゲージ")]
    [SerializeField] private Image[] _hps;

    [Header("HPゲージの親オブジェクト")]
    [SerializeField] private RectTransform _hpPanel;
    public int PlayerCurrentHP;
    
    /// <summary>
    /// プレイヤーのステータスをリセット
    /// </summary>
    public void PlayerStateReset()
    {
        PlayerCurrentHP = PlayerMaxHP;
        foreach (var hp in _hps)
            hp.gameObject.SetActive(true);
    }
    /// <summary>
    /// プレイヤーがダメージを受ける
    /// </summary>
    /// <param name="damage"></param>
    public void PlayerDamaged(int damage)
    {
        PlayerCurrentHP -= damage;
        Debug.Log($"{damage}ダメージを受けた！");
        for (int i = 0; i < _hps.Length; i++)
        {
            _hps[i].gameObject.SetActive(i < PlayerCurrentHP);
        }
        _hpPanel.DOShakeAnchorPos(
            0.3f,   // 揺れる時間
            20f,    // 揺れの強さ
            10,     // 揺れる回数
            90f,    // ランダム性
            false,  // フェイドアウト
            true    // 相対モーション
        );
    }

}
