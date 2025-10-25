using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [Tooltip("�J�E���g�_�E���p�l��"), SerializeField] private GameObject _countDownPanel;
    [Tooltip("�G��_�p�l��"),SerializeField] private GameObject _weakPointPanelE;
    [SerializeField] private RectTransform _weakPanelErtr;
    [Tooltip("������_�p�l��"),SerializeField] private GameObject _weakPointPanelP;
    [SerializeField] private RectTransform _weakPanelPrtr;
    [Header("��_")]
    [Tooltip("��_�摜"), SerializeField] private GameObject _weakPointPrefab;
    [Header("�J�E���g�_�E���e�L�X�g")]
    [SerializeField] private TextMeshProUGUI[] _countDownTexts;
    [SerializeField] private float _countDuration = 0.7f;
    [SerializeField] private float _maxScale = 2.5f;
    private RectTransform _weakRect;
    private GameObject _weakPoint;
    private float _width, _height, _randomX, _randomY;
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
        InGameStart(false);
        _countDownPanel.SetActive(false);
        foreach(var count in _countDownTexts)
        {
            count.gameObject.SetActive(false);
        }
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
    /// <summary>
    /// �����p�l���̕\��
    /// </summary>
    /// <param name="isClicked"></param>
    public void InGameStart(bool isClicked)
    {
        _startPanel.SetActive(!isClicked);
    }
    /// <summary>
    /// �J�E���g�_�E��
    /// </summary>
    /// <returns></returns>
    public IEnumerator CountDown()
    {
        _countDownPanel.SetActive(true);
        yield return null;
        for(int i = 0; i < _countDownTexts.Length;i++)
        {
            if(i != 0)
            {
                _countDownTexts[i - 1].gameObject.SetActive(false);
            }
            _countDownTexts[i].gameObject.SetActive(true);
            if (i == _countDownTexts.Length - 1)
            {
                _countDuration *= 1.5f;
                _countDownTexts[i].rectTransform.localScale = Vector3.zero;
                _countDownTexts[i].rectTransform.DOScale(_maxScale, _countDuration);
            }
            yield return new WaitForSeconds(_countDuration);
        }
        _countDownTexts[_countDownTexts.Length - 1].gameObject.SetActive(false);
        _countDownPanel.SetActive(false);
        GameManager.Instance._gamePhase = InGamePhase.Chose;
    }
}
