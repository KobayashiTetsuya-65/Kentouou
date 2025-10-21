using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HP�Q�[�W")]
    [Tooltip("�v���C���[HP�Q�[�W"), SerializeField] private Slider _playerHP;
    [Tooltip("�G�l�~�[HP�Q�[�W"), SerializeField] private Slider _enemeyHP;
    [Header("�L�����N�^�[")]
    [Tooltip("�v���C���["),SerializeField] private Image _playerImage;
    [Tooltip("�G�l�~�["),SerializeField] private Image _enemyImage;
    [Header("�p�l��")]
    [Tooltip("�s���I��"), SerializeField] private GameObject _activeChosePanel;
    [Header("�{�^��")]
    [Tooltip("�U��"), SerializeField] private Button _attackButton;
    [Tooltip("�h��"),SerializeField] private Button _defenseButton;
    [Tooltip("���߂�"), SerializeField] private Button _accumulateButton;
    private bool _chose = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _attackButton.onClick.AddListener(Attack);
        _defenseButton.onClick.AddListener(Defense);
        _accumulateButton.onClick.AddListener(Accumulate);
    }

    /// <summary>
    /// UI�̒l�����Z�b�g
    /// </summary>
     public void ResetState()
    {
        _playerHP.value = 1;
        _enemeyHP.value = 1;
    }
    private void Attack()
    {
        Debug.Log("�U��");
    }
    private void Defense()
    {
        Debug.Log("�h��");
    }
    private void Accumulate()
    {
        Debug.Log("���߂�");
    }
}
