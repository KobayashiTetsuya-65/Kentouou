using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("HP�Q�[�W")]
    [Tooltip("�v���C���[HP�Q�[�W"), SerializeField] private GameObject _playerHP;
    [Tooltip("�G�l�~�[HP�Q�[�W"), SerializeField] private GameObject _enemyHP;

    [Header("�L�����N�^�[���W")]
    [Tooltip("�v���C���["), SerializeField] private RectTransform _playerRectTr;
    [Tooltip("�G�l�~�["), SerializeField] private RectTransform _enemyRectTr;
    [Tooltip("��ʒu"),SerializeField] private RectTransform playerRect;
    [Tooltip("��ʒu"),SerializeField] private RectTransform enemyRect;


    [Header("�L�����N�^�[�C���[�W")]
    [Tooltip("�v���C���["),SerializeField] private Image _playerImage;
    [Tooltip("�G�l�~�["),SerializeField] private Image _enemyImage;

    [Header("�L�����N�^�[�摜")]
    [Tooltip("�v���C���["),SerializeField] private Sprite _playerSprite;
    [Tooltip("�G�l�~�["),SerializeField] private Sprite _enemySprites;

    [Header("�U�����[�V����")]
    [Tooltip("�v���C���[�̍U��"), SerializeField] private Sprite[] _attackPlayerSprits;
    [Tooltip("�G�l�~�[�̍U��"), SerializeField] private Sprite[] _attackEnemySprits;

    [Header("�픚���̉摜")]
    [Tooltip("�v���C���["), SerializeField] private Sprite _damagedPlayerSprite;
    [Tooltip("�G�l�~�["), SerializeField] private Sprite _damagedEnemySprite;

    [Header("���l�ݒ�")]
    [Tooltip("�U���A�j���[�V�����̊Ԋu"), SerializeField] private float _attackDuration = 0.25f;
    [SerializeField] private float _countDuration = 0.7f;
    [SerializeField] private float _maxScale = 2.5f;
    [Tooltip("�Q�[�W�������܂ł̎���"), SerializeField] private float _timer = 5f;

    [Header("�p�l��")]
    [Tooltip("�C���Q�[���p�l��"), SerializeField] private GameObject _panel;
    [Tooltip("�X�^�[�g�p�l��"),SerializeField] private GameObject _startPanel;
    [Tooltip("�J�E���g�_�E���p�l��"), SerializeField] private GameObject _countDownPanel;
    [Tooltip("�G��_�p�l��"),SerializeField] private GameObject _weakPointPanelE;
    [SerializeField] private RectTransform _weakPanelErtr;
    [Tooltip("������_�p�l��"),SerializeField] private GameObject _weakPointPanelP;
    [SerializeField] private RectTransform _weakPanelPrtr;

    [Header("������")]
    [Tooltip("��_�摜"), SerializeField] private GameObject _weakPointPrefab;
    [Tooltip("��_�^�C�}�["), SerializeField] private GameObject _weakTimerPrefab;
    [Tooltip("�K�E�Q�[�W"), SerializeField] private GameObject _gaugePrefab;
    [Tooltip("�K�E��_"), SerializeField] private GameObject _specialWeakPointPrefab;

    [Header("�J�E���g�_�E���e�L�X�g")]
    [SerializeField] private TextMeshProUGUI[] _countDownTexts;

    [SerializeField]private Canvas _canvas;
    private HPBarController _HPBarE;
    private Player _player;
    private RectTransform _weakRect,_weakTimerRect;
    private GameObject _weakPoint,_weakTimer,_special,_bigWeakPoint;
    private float _width, _height, _randomX, _randomY;

    private void Awake()
    {
        InGamePanel(true,0);
    }
    /// <summary>
    /// UI�����Z�b�g
    /// </summary>
    public void ResetState()
    {
        _HPBarE = _enemyHP.GetComponent<HPBarController>();
        _player = _playerHP.GetComponent<Player>();
        _player.PlayerStateReset();
        _HPBarE.HPBarReset();
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
            _weakTimer = Instantiate(_weakTimerPrefab, _weakPointPanelE.transform);
            _weakPoint = Instantiate(_weakPointPrefab, _weakPointPanelE.transform);
            _width = _weakPanelErtr.rect.width;
            _height = _weakPanelErtr.rect.height;
        }
        else
        {
            _weakTimer = Instantiate(_weakTimerPrefab, _weakPointPanelP.transform);
            _weakPoint = Instantiate(_weakPointPrefab,_weakPointPanelP.transform);
            _width = _weakPanelPrtr.rect.width;
            _height = _weakPanelPrtr.rect.height;
        }
        _randomX = Random.Range(-_width / 2f, _width / 2f);
        _randomY = Random.Range(-_height / 2f, _height / 2f);
        _weakRect = _weakPoint.GetComponent<RectTransform>();
        _weakTimerRect = _weakTimer.GetComponent<RectTransform>();
        _weakRect.anchoredPosition = new Vector2(_randomX, _randomY);
        _weakTimerRect.anchoredPosition = new Vector2(_randomX, _randomY);
    }
    /// <summary>
    /// ���Ԍo�߂Ŏ�_�j��
    /// </summary>
    public void TimerChecker(bool isBreak)
    {
        if(_weakTimer == null)
        {
            Destroy(_weakPoint);
        }
        if (isBreak){
            Destroy(_weakTimer);
            if(_weakPoint != null)
            {
                Destroy(_weakPoint);
            }
        }
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
    /// <summary>
    /// �U������Sprite����ւ�
    /// </summary>
    /// <param name="isPlayer"></param>
    public IEnumerator AttackMotion(bool isPlayer)
    {
        int n = Random.Range(0, 2);
        if (isPlayer)
        {
            _enemyImage.sprite = _damagedEnemySprite;
            _playerImage.sprite = _attackPlayerSprits[n];
            _playerRectTr.anchoredPosition = Vector2.zero;
            _playerRectTr.anchoredPosition += new Vector2(50, 0);
        }
        else
        {
            _playerImage.sprite = _damagedPlayerSprite;
            _enemyImage.sprite = _attackEnemySprits[n];
            _enemyRectTr.anchoredPosition = Vector2.zero;
            _enemyRectTr.anchoredPosition -= new Vector2(50, 0);
        }
        yield return new WaitForSeconds(_attackDuration);
        _playerImage.sprite = _playerSprite;
        _enemyImage.sprite = _enemySprites;
        _playerRectTr.anchoredPosition = playerRect.anchoredPosition;
        _enemyRectTr.anchoredPosition = enemyRect.anchoredPosition;
        GameManager.Instance._coroutine = null;
    }
    /// <summary>
    /// ���Ԍo�߂ŃX�y�V�����Q�[�W����
    /// </summary>
    /// <returns></returns>
    public IEnumerator CreateSpecialGauge()
    {
        yield return new WaitForSeconds(_timer);
        _special = Instantiate(_gaugePrefab);
        _special.transform.SetParent(_canvas.transform,false);
        Debug.Log("�K�E�Q�[�W�o���I�I");
    }
    /// <summary>
    /// �K�E�Z
    /// </summary>
    public void UseSpecial()
    {
        _bigWeakPoint = Instantiate(_specialWeakPointPrefab);
        _bigWeakPoint.transform.SetParent(_panel.transform,false);
        //���o
    }
    /// <summary>
    /// �L�����N�^�[�̌����ڂ����ɖ߂�
    /// </summary>
    public void ResetCharactorSprite()
    {
        _playerImage.sprite = _playerSprite;
        _enemyImage.sprite = _enemySprites;
        _playerRectTr.anchoredPosition = playerRect.anchoredPosition;
        _enemyRectTr.anchoredPosition = enemyRect.anchoredPosition;
    }
    public IEnumerator InGamePanel(bool show,float duration)
    {
        yield return new WaitForSeconds(duration);
        _panel.gameObject.SetActive(show);
    }
}
