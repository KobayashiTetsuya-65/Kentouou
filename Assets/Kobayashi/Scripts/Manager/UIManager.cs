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
    /// UI�̒l�����Z�b�g
    /// </summary>
     public void ResetState()
    {
        _playerHP.value = 1;
        _enemeyHP.value = 1;
    }
}
