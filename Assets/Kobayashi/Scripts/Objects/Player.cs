using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �v���C���[�̃X�e�[�^�X�Ǘ�
/// </summary>
public class Player : MonoBehaviour
{
    [Header("�X�e�[�^�X")]
    [SerializeField] public int PlayerMaxHP;

    [Header("HP�Q�[�W")]
    [SerializeField] private Image[] _hps;
    public int PlayerCurrentHP;
    
    private void Start()
    {
        
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
    public void PlayerDamaged(int damage)
    {
        PlayerCurrentHP -= damage;
        Debug.Log($"{damage}�_���[�W���󂯂��I");
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
