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
    public int PlayerCurrentHP;
    
    private void Start()
    {
        
    }
    /// <summary>
    /// プレイヤーのステータスをリセット
    /// </summary>
    public void PlayerStateReset()
    {
        PlayerCurrentHP = PlayerMaxHP;
    }
    /// <summary>
    /// プレイヤーがダメージを受ける
    /// </summary>
    /// <param name="damage"></param>
    public void PlayerDamaged(int damage)
    {
        PlayerCurrentHP -= damage;
        Debug.Log($"{damage}ダメージを受けた！");
        for(int i = 0; i < _hps.Length; i++)
        {
            _hps[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < PlayerCurrentHP; i++)
        {
            _hps[i].gameObject.SetActive(true);
        }
    }

}
