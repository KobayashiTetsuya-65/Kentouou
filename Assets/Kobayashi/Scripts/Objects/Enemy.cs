using UnityEngine;
/// <summary>
/// エネミーのステータス管理
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] public float EnemyMaxHP;
    [SerializeField] public float EnemyAttackP;
    public float EnemyCurrentHP;
    /// <summary>
    /// エネミーのステータスリセット
    /// </summary>
    public void EnemyStateReset()
    {
        EnemyCurrentHP = EnemyMaxHP;
    }
    /// <summary>
    /// エネミーがダメージを受ける
    /// </summary>
    /// <param name="damage"></param>
    public void EnemyDamaged(float damage)
    {
        EnemyCurrentHP -= damage;
        Debug.Log($"{damage}を与えた");
    }
}
