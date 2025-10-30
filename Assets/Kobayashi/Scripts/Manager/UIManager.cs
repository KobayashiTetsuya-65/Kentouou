using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("HPゲージ")]
    [Tooltip("プレイヤーHPゲージ"), SerializeField] private GameObject _playerHP;
    [Tooltip("エネミーHPゲージ"), SerializeField] private GameObject _enemyHP;

    [Header("キャラクター座標")]
    [Tooltip("プレイヤー"), SerializeField] private RectTransform _playerRectTr;
    [Tooltip("エネミー"), SerializeField] private RectTransform _enemyRectTr;
    [Tooltip("定位置"),SerializeField] private RectTransform playerRect;
    [Tooltip("定位置"),SerializeField] private RectTransform enemyRect;


    [Header("キャラクターイメージ")]
    [Tooltip("プレイヤー"),SerializeField] private Image _playerImage;
    [Tooltip("エネミー"),SerializeField] private Image _enemyImage;

    [Header("キャラクター画像")]
    [Tooltip("プレイヤー"),SerializeField] private Sprite _playerSprite;
    [Tooltip("エネミー"),SerializeField] private Sprite _enemySprites;

    [Header("攻撃モーション")]
    [Tooltip("プレイヤーの攻撃"), SerializeField] private Sprite[] _attackPlayerSprits;
    [Tooltip("エネミーの攻撃"), SerializeField] private Sprite[] _attackEnemySprits;

    [Header("被爆時の画像")]
    [Tooltip("プレイヤー"), SerializeField] private Sprite _damagedPlayerSprite;
    [Tooltip("エネミー"), SerializeField] private Sprite _damagedEnemySprite;

    [Header("数値設定")]
    [Tooltip("攻撃アニメーションの間隔"), SerializeField] private float _attackDuration = 0.25f;
    [SerializeField] private float _countDuration = 0.7f;
    [SerializeField] private float _maxScale = 2.5f;
    [Tooltip("ゲージが沸くまでの時間"), SerializeField] private float _timer = 5f;

    [Header("パネル")]
    [Tooltip("インゲームパネル"), SerializeField] private GameObject _panel;
    [Tooltip("スタートパネル"),SerializeField] private GameObject _startPanel;
    [Tooltip("カウントダウンパネル"), SerializeField] private GameObject _countDownPanel;
    [Tooltip("敵弱点パネル"),SerializeField] private GameObject _weakPointPanelE;
    [SerializeField] private RectTransform _weakPanelErtr;
    [Tooltip("自分弱点パネル"),SerializeField] private GameObject _weakPointPanelP;
    [SerializeField] private RectTransform _weakPanelPrtr;

    [Header("生成物")]
    [Tooltip("弱点画像"), SerializeField] private GameObject _weakPointPrefab;
    [Tooltip("弱点タイマー"), SerializeField] private GameObject _weakTimerPrefab;
    [Tooltip("必殺ゲージ"), SerializeField] private GameObject _gaugePrefab;
    [Tooltip("必殺弱点"), SerializeField] private GameObject _specialWeakPointPrefab;

    [Header("カウントダウンテキスト")]
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
    /// UIをリセット
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
    /// 弱点を生成
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
    /// 時間経過で弱点破壊
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
    /// 準備パネルの表示
    /// </summary>
    /// <param name="isClicked"></param>
    public void InGameStart(bool isClicked)
    {
        _startPanel.SetActive(!isClicked);
    }
    /// <summary>
    /// カウントダウン
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
    /// 攻撃時のSprite入れ替え
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
    /// 時間経過でスペシャルゲージ生成
    /// </summary>
    /// <returns></returns>
    public IEnumerator CreateSpecialGauge()
    {
        yield return new WaitForSeconds(_timer);
        _special = Instantiate(_gaugePrefab);
        _special.transform.SetParent(_canvas.transform,false);
        Debug.Log("必殺ゲージ出現！！");
    }
    /// <summary>
    /// 必殺技
    /// </summary>
    public void UseSpecial()
    {
        _bigWeakPoint = Instantiate(_specialWeakPointPrefab);
        _bigWeakPoint.transform.SetParent(_panel.transform,false);
        //演出
    }
    /// <summary>
    /// キャラクターの見た目を元に戻す
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
