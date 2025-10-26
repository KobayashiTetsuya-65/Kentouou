using UnityEngine;
/// <summary>
/// �G�l�~�[�̃X�e�[�^�X�Ǘ�
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField] public float EnemyMaxHP;
    [SerializeField] public float EnemyAttackP;
    [Header("HP�Q�[�W")]
    [SerializeField] private GameObject _hp;
    private HPBarController _controller;
    public float EnemyCurrentHP;
    private void Start()
    {
        _controller = _hp.GetComponent<HPBarController>();
    }
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
        _controller.TakeDamage(damage);
    }
}
