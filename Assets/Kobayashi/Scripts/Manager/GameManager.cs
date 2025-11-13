using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player _player;
    private Enemy _enemy;
    private UIManager _uiManager;
    public InGamePhase GamePhase;
    public SceneDivision CurrentScene;
    public Coroutine _coroutine;
    public bool Hit = false;
    public bool Defence = false;
    public bool Miss = false;
    public bool IsSpecialFinish = false;
    public bool PlayerWin = false;
    private bool _changeBGM = false;
    private bool _special;
    private bool _weakPoint = false;
    private bool _enemyWeak;
    private bool _isPanel = false;
    private bool _titleBGM = false;
    private bool _isFirstStage = true;
    public bool _spcialCreate = false;
    public float Damage;
    private float _point;
    [Tooltip("敵に弱点が沸く確率"), SerializeField] private float _parcent = 0.9f;
    private void Awake()
    {
        Application.targetFrameRate = 100;
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        switch (CurrentScene)
        {
            case SceneDivision.Title://タイトルシーンで実行したいこと
                if (!_titleBGM)
                {
                    AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Bgm.Title);
                    _titleBGM = true;
                }
            break;
            case SceneDivision.InGame://インゲームシーンで実行したいこと
                switch (GamePhase)
                {
                    case InGamePhase.Start:
                        if (!_changeBGM)
                        {
                            AudioManager.Instance.PlayBGM(SoundDataUtility.KeyConfig.Bgm.InGame);
                            _changeBGM = true;
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            GamePhase = InGamePhase.CountDown;
                            _uiManager.InGameStart(true);
                            _player.PlayerStateReset();
                            _enemy.EnemyStateReset();
                            _spcialCreate = false;
                            StartCoroutine(_uiManager.CountDown());
                        }
                        break;
                    case InGamePhase.CountDown:
                        //UIManagerの方でカウントダウン処理
                    break;
                    case InGamePhase.Chose:
                        if (!_spcialCreate)
                        {
                            StartCoroutine(_uiManager.CreateSpecialGauge());
                            _spcialCreate = true;
                        }
                        if (Hit)//弱点攻撃時
                        {
                            AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Punch);
                            if (_coroutine == null) _coroutine = StartCoroutine(_uiManager.AttackMotion(Hit));
                            _enemy.EnemyDamaged(Damage);
                            _uiManager.TimerChecker(true);
                            Hit = false;
                            _weakPoint = false;
                            if(_enemy.EnemyCurrentHP <= 0)//勝利時
                            {
                                AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Finish);
                                GamePhase = InGamePhase.Start;
                                CurrentScene = SceneDivision.Result;
                                PlayerWin = true;
                                _uiManager.SpecialGaugePanel(false);
                            }
                        }
                        else if (Defence)//防御成功時
                        {
                            //作ってるなう
                            AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Defence);
                            if (_coroutine == null) _coroutine = StartCoroutine(_uiManager.DefenceMotion());
                            _enemy.EnemyDamaged(Damage);
                            _uiManager.TimerChecker(true);
                            Defence = false;
                            _weakPoint = false;
                        }
                        else
                        {
                            if (_weakPoint)
                            {
                                _uiManager.TimerChecker(false);
                            }
                            else
                            {
                                _point = Random.Range(0f, 1f);
                                _enemyWeak = _point <= _parcent;
                                _uiManager.SpawnWeakPoint(_enemyWeak);
                                _weakPoint = true;
                            }
                            if (Miss)//弱点外を検知
                            {
                                AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Damaged);
                                if (_coroutine == null) _coroutine = StartCoroutine(_uiManager.AttackMotion(Hit));
                                _player.PlayerDamaged(1);
                                _uiManager.TimerChecker(true);
                                Miss = false;
                                _weakPoint = false;
                                if (_player.PlayerCurrentHP <= 0)//敗北時
                                {
                                    AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Finish);
                                    GamePhase = InGamePhase.Start;
                                    CurrentScene = SceneDivision.Result;
                                    PlayerWin = false;
                                    _uiManager.SpecialGaugePanel(false);
                                }
                            }
                        }
                            break;
                    case InGamePhase.Attack://必殺技
                        if (!_special)
                        {
                            _uiManager.TimerChecker(true);
                            _uiManager.UseSpecial();
                            _special = true;
                        }
                        if (Hit)
                        {
                            AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Critical);
                            StartCoroutine(_uiManager.AttackMotion(Hit));
                            _enemy.EnemyDamaged(Damage);
                            Hit = false;
                            if (_enemy.EnemyCurrentHP <= 0)//勝利時
                            {
                                AudioManager.Instance.PlaySe(SoundDataUtility.KeyConfig.Se.Finish);
                                GamePhase = InGamePhase.Start;
                                CurrentScene = SceneDivision.Result;
                                PlayerWin = true;
                                _uiManager.SpecialGaugePanel(false);
                            }
                        }
                        if (IsSpecialFinish)//必殺技終了時の処理
                        {
                            GamePhase = InGamePhase.Chose;
                            _uiManager.ResetCharactorSprite();
                            _uiManager.SpecialGaugePanel(false);
                            _weakPoint = false;
                            _spcialCreate = false;
                            _special = false;
                            Hit = false;
                            Miss = false;
                            IsSpecialFinish = false;
                        }
                    break;
                    case InGamePhase.Direction://演出中
                        _uiManager.TimerChecker(true);
                    break;
                    case InGamePhase.Explanation:
                        if (_isFirstStage)
                        {
                            _uiManager.RuleExplanation();
                        }
                        break;
                }
            break;
            case SceneDivision.Result://リザルトシーンで実行したいこと
                if (!_isPanel)
                {
                    StartCoroutine(_uiManager.FinishInGame(PlayerWin));
                    _isPanel = true;
                }
            break;
        }
    }
    public void SetScript()
    {
        _player = FindAnyObjectByType<Player>();
        _enemy = FindAnyObjectByType<Enemy>();
        _uiManager = FindAnyObjectByType<UIManager>();
        _uiManager.ResetState();
        GamePhase = InGamePhase.Explanation;
        _special = false;
        _isPanel = false;
        _changeBGM = false;
        PlayerWin = false;
        _titleBGM = false;
        Miss = false;
        _weakPoint = false;
    }
}
