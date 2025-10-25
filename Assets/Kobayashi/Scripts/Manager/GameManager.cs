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

        ChangePhase(InGamePhase.Start);
        _uiManager.ResetState();
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
                            StartCoroutine(_uiManager.CountDown());
                        }
                        break;
                    case InGamePhase.CountDown:
                        //UIManager�̕��ŃJ�E���g�_�E������
                    break;
                    case InGamePhase.Chose:

                    break;
                    case InGamePhase.Attack:

                    break;
                }
            break;
            case SceneDivision.Result://���U���g�V�[���Ŏ��s���������� 

            break;
        }
    }
    private void ChangePhase(InGamePhase phaseName)
    {
        _gamePhase = phaseName;
        _currentScene = SceneDivision.InGame;
    }
}
