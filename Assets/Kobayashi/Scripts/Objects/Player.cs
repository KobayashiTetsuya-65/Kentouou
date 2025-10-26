using UnityEngine;
/// <summary>
/// �v���C���[�̃X�e�[�^�X�Ǘ�
/// </summary>
public class Player : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField] public float PlayerMaxHP;
    [SerializeField] public float PlayerAttackP;
    [Header("HP�Q�[�W")]
    [SerializeField] private GameObject _hp;
    public float PlayerCurrentHP;
    private HPBarController _controller;
    private void Start()
    {
        _controller = _hp.GetComponent<HPBarController>();
    }
    /// <summary>
    /// �v���C���[�̃X�e�[�^�X�����Z�b�g
    /// </summary>
    public void PlayerStateReset()
    {
        PlayerCurrentHP = PlayerMaxHP;
    }
    /// <summary>
    /// �v���C���[���_���[�W���󂯂�
    /// </summary>
    /// <param name="damage"></param>
    public void PlayerDamaged(float damage)
    {
        PlayerCurrentHP -= damage;
        Debug.Log($"{damage}�_���[�W���󂯂��I");
        _controller.TakeDamage( damage );
    }

}
