using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player _player;
    private Enemy _enemy;
    private UIManager _uiManager;
    public InGamePhase _gamePhase;
    private SceneDivision _currentScene;
    public Coroutine _coroutine;
    public bool Hit = false;
    public bool Miss = false;
    public bool IsSpecialFinish = false;
    private bool _special;
    private bool _weakPoint = false;
    private bool _enemyWeak,_spcialCreate = false;
    public float Damage;
    private float _point;
    [Tooltip("�G�Ɏ�_�������m��"), SerializeField] private float _parcent = 0.9f;
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

        //���switch�ɓ���ăG���[�΍�s��
        _player = FindAnyObjectByType<Player>();
        _enemy = FindAnyObjectByType<Enemy>();
        _uiManager = FindAnyObjectByType<UIManager>();

        _gamePhase = InGamePhase.Start;
        _currentScene = SceneDivision.InGame;
        _uiManager.ResetState();
        _special = false;
    }


    // Update is called once per frame
    void Update()
    {
        switch (_currentScene)
        {
            case SceneDivision.Title://�^�C�g���V�[���Ŏ��s����������

            break;
            case SceneDivision.InGame://�C���Q�[���V�[���Ŏ��s����������
                switch (_gamePhase)
                {
                    case InGamePhase.Start:
                        if (Input.GetMouseButtonDown(0))
                        {
                            _gamePhase = InGamePhase.CountDown;
                            _uiManager.InGameStart(true);
                            _player.PlayerStateReset();
                            _enemy.EnemyStateReset();
                            _spcialCreate = false;
                            StartCoroutine(_uiManager.CountDown());
                        }
                        break;
                    case InGamePhase.CountDown:
                        //UIManager�̕��ŃJ�E���g�_�E������
                    break;
                    case InGamePhase.Chose:
                        if (!_spcialCreate)
                        {
                            StartCoroutine(_uiManager.CreateSpecialGauge());
                            _spcialCreate = true;
                            Debug.Log("�K�E�����t���OON�I");
                        }
                        if (Hit)//��_�U����
                        {
                            if(_coroutine == null) _coroutine = StartCoroutine(_uiManager.AttackMotion(Hit));
                            _enemy.EnemyDamaged(Damage);
                            _uiManager.TimerChecker(true);
                            Hit = false;
                            _weakPoint = false;
                            if(_enemy.EnemyCurrentHP <= 0)//������
                            {
                                _gamePhase = InGamePhase.Start;
                                _currentScene = SceneDivision.Result;
                            }
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
                            if (Miss)//��_�O�����m
                            {
                                if (_coroutine == null) _coroutine = StartCoroutine(_uiManager.AttackMotion(Hit));
                                _player.PlayerDamaged(1);
                                _uiManager.TimerChecker(true);
                                Miss = false;
                                _weakPoint = false;
                                if (_player.PlayerCurrentHP <= 0)//�s�k��
                                {
                                    _gamePhase = InGamePhase.Start;
                                    _currentScene = SceneDivision.Result;
                                }
                            }
                        }
                            break;
                    case InGamePhase.Attack://�K�E�Z
                        if (!_special)
                        {
                            _uiManager.TimerChecker(true);
                            _uiManager.UseSpecial();
                            _special = true;
                        }
                        if (Hit)
                        {
                            StartCoroutine(_uiManager.AttackMotion(Hit));
                            _enemy.EnemyDamaged(Damage);
                            Hit = false;
                            if (_enemy.EnemyCurrentHP <= 0)//������
                            {
                                _gamePhase = InGamePhase.Start;
                                _currentScene = SceneDivision.Result;
                            }
                        }
                        if (IsSpecialFinish)
                        {
                            _gamePhase = InGamePhase.Chose;
                            _uiManager.ResetCharactorSprite();
                            _weakPoint = false;
                            _spcialCreate = false;
                            _special = false;
                            Hit = false;
                            Miss = false;
                            IsSpecialFinish = false;
                        }
                    break;
                }
            break;
            case SceneDivision.Result://���U���g�V�[���Ŏ��s���������� 

                Debug.Log("���ʔ��\�`�`�`�`�`");
            break;
        }
    }
}
