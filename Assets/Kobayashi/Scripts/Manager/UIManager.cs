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
    [Tooltip("�X�^�[�g�p�l��"),SerializeField] private GameObject _startPanel;
    [Tooltip("�G��_�p�l��"),SerializeField] private GameObject _weakPointPanelE;
    [SerializeField] private RectTransform _weakPanelErtr;
    [Tooltip("������_�p�l��"),SerializeField] private GameObject _weakPointPanelP;
    [SerializeField] private RectTransform _weakPanelPrtr;
    [Header("��_")]
    [Tooltip("��_�摜"), SerializeField] private GameObject _weakPointPrefab;
    private RectTransform _weakRect;
    private GameObject _weakPoint;
    private float _width, _height, _randomX, _randomY;
    private bool _chose = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
    /// <summary>
    /// ��_�𐶐�
    /// </summary>
    /// <param name="isEnemy"></param>
    public void SpawnWeakPoint(bool isEnemy)
    {
        if (isEnemy)
        {
            _weakPoint = Instantiate(_weakPointPrefab, _weakPointPanelE.transform);
            _width = _weakPanelErtr.rect.width;
            _height = _weakPanelErtr.rect.height;
        }
        else
        {
            _weakPoint = Instantiate(_weakPointPrefab,_weakPointPanelP.transform);
            _width = _weakPanelPrtr.rect.width;
            _height = _weakPanelPrtr.rect.height;
        }
        _randomX = Random.Range(-_width / 2f, _width / 2f);
        _randomY = Random.Range(-_height / 2f, _height / 2f);
        _weakRect = _weakPoint.GetComponent<RectTransform>();
        _weakRect.anchoredPosition = new Vector2(_randomX, _randomY);
    }
    public void InGameStart(bool isClicked)
    {
        _startPanel.SetActive(!isClicked);
    }
    private void Attack()
    {
        Debug.Log("�U��");
    }
}
