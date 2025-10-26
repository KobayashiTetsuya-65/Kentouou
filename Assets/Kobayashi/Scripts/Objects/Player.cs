using UnityEngine;
/// <summary>
/// プレイヤーのステータス管理
/// </summary>
public class Player : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] public float PlayerMaxHP;
    [SerializeField] public float PlayerAttackP;
    [Header("HPゲージ")]
    [SerializeField] private GameObject _hp;
    public float PlayerCurrentHP;
    private HPBarController _controller;
    private void Start()
    {
        _controller = _hp.GetComponent<HPBarController>();
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
    public void PlayerDamaged(float damage)
    {
        PlayerCurrentHP -= damage;
        Debug.Log($"{damage}ダメージを受けた！");
        _controller.TakeDamage( damage );
    }

}
