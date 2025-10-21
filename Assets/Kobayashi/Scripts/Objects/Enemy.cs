using UnityEngine;
/// <summary>
/// �G�l�~�[�̃X�e�[�^�X�Ǘ�
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField] public float EnemyMaxHP;
    [SerializeField] public float EnemyAttackP;
    public float EnemyCurrentHP;
    /// <summary>
    /// �G�l�~�[�̃X�e�[�^�X���Z�b�g
    /// </summary>
    public void EnemyStateReset()
    {
        EnemyCurrentHP = EnemyMaxHP;
    }
    /// <summary>
    /// �G�l�~�[���_���[�W���󂯂�
    /// </summary>
    /// <param name="damage"></param>
    public void EnemyDamaged(float damage)
    {
        EnemyCurrentHP -= damage;
        Debug.Log($"{damage}��^����");
    }
}
