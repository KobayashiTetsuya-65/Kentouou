using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player _player;
    private Enemy _enemy;
    private UIManager _uiManager;
    public InGamePhase _gamePhase;
    private SceneDivision _currentScene;
    public bool Hit = false;
    private bool _isWeakPont, _weakPoint = false, _enemyWeak;
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

        //後でswitchに入れてエラー対策行う
        _player = FindAnyObjectByType<Player>();
        _enemy = FindAnyObjectByType<Enemy>();
        _uiManager = FindAnyObjectByType<UIManager>();

        ChangePhase(InGamePhase.Start);
        _uiManager.ResetState();
    }


    // Update is called once per frame
    void Update()
    {
        switch (_currentScene)
        {
            case SceneDivision.Title://タイトルシーンで実行したいこと

            break;
            case SceneDivision.InGame://インゲームシーンで実行したいこと
                switch (_gamePhase)
                {
                    case InGamePhase.Start:
                        if (Input.GetMouseButtonDown(0))
                        {
                            _gamePhase = InGamePhase.CountDown;
                            _uiManager.InGameStart(true);
                            _player.PlayerStateReset();
                            _enemy.EnemyStateReset();
                            StartCoroutine(_uiManager.CountDown());
                        }
                        break;
                    case InGamePhase.CountDown:
                        //UIManagerの方でカウントダウン処理
                    break;
                    case InGamePhase.Chose:
                        if (Hit)
                        {
                            if (_enemyWeak)
                            {
                                _enemy.EnemyDamaged(Damage);
                            }
                            else
                            {
                                _enemy.EnemyDamaged(Damage);
                                //_player.PlayerDamaged(Damege);
                            }
                            Hit = false;
                            _weakPoint = false;
                            if(_player.PlayerCurrentHP <= 0)
                            {
                                _gamePhase = InGamePhase.Start;
                                _currentScene = SceneDivision.Result;
                            }
                            else if(_enemy.EnemyCurrentHP <= 0)
                            {
                                _gamePhase = InGamePhase.Start;
                                _currentScene = SceneDivision.Result;
                            }
                        }
                        else
                        {
                            if (!_weakPoint)
                            {
                                _point = Random.Range(0f, 1f);
                                _enemyWeak = _point <= _parcent;
                                _uiManager.SpawnWeakPoint(_enemyWeak);
                                _weakPoint = true;
                            }
                        }
                            break;
                    case InGamePhase.Attack:

                    break;
                }
            break;
            case SceneDivision.Result://リザルトシーンで実行したいこと 
                Debug.Log("結果発表～～～～～");
            break;
        }
    }
    private void ChangePhase(InGamePhase phaseName)
    {
        _gamePhase = phaseName;
        _currentScene = SceneDivision.InGame;
    }
}
