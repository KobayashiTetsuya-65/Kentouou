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
    [Header("キャラクターイメージ")]
    [Tooltip("プレイヤー"),SerializeField] private Image _playerImage;
    [Tooltip("エネミー"),SerializeField] private Image _enemyImage;
    [Header("攻撃モーション")]
    [Tooltip("プレイヤーの攻撃"), SerializeField] private Sprite[] _attackPlayerSprits;
    [Tooltip("エネミーの攻撃"), SerializeField] private Sprite[] _attackEnemySprits;
    [Header("数値設定")]
    [Tooltip("攻撃アニメーションの間隔"), SerializeField] private float _attackDuration = 0.25f;
    [Header("パネル")]
    [Tooltip("スタートパネル"),SerializeField] private GameObject _startPanel;
    [Tooltip("カウントダウンパネル"), SerializeField] private GameObject _countDownPanel;
    [Tooltip("敵弱点パネル"),SerializeField] private GameObject _weakPointPanelE;
    [SerializeField] private RectTransform _weakPanelErtr;
    [Tooltip("自分弱点パネル"),SerializeField] private GameObject _weakPointPanelP;
    [SerializeField] private RectTransform _weakPanelPrtr;
    [Header("弱点")]
    [Tooltip("弱点画像"), SerializeField] private GameObject _weakPointPrefab;
    [Header("カウントダウンテキスト")]
    [SerializeField] private TextMeshProUGUI[] _countDownTexts;
    [SerializeField] private float _countDuration = 0.7f;
    [SerializeField] private float _maxScale = 2.5f;
    private HPBarController _HPBarP, _HPBarE;
    private RectTransform _weakRect;
    private GameObject _weakPoint;
    private Sprite _idleSprite;
    private float _width, _height, _randomX, _randomY;

    /// <summary>
    /// UIをリセット
    /// </summary>
     public void ResetState()
    {
        _HPBarP = _playerHP.GetComponent<HPBarController>();
        _HPBarE = _enemyHP.GetComponent<HPBarController>();
        _HPBarP.HPBarReset();
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
            _idleSprite = _playerImage.sprite;
            _playerImage.sprite = _attackPlayerSprits[n];
            yield return new WaitForSeconds(_attackDuration);
            _playerImage.sprite = _idleSprite;
        }
        else
        {
            _idleSprite = _enemyImage.sprite;
            _enemyImage.sprite = _attackEnemySprits[n];
            yield return new WaitForSeconds(_attackDuration);
            _enemyImage.sprite = _idleSprite;
        }
    }
}
