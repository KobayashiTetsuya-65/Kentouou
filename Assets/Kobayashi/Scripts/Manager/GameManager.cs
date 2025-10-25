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

        ChangePhase(InGamePhase.Chose);
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
                    case InGamePhase.CountDown:

                    break;
                    case InGamePhase.Chose:
                        if (Input.GetMouseButtonDown(0))
                        {
                            _uiManager.SpawnWeakPoint(true);
                        }
                        if (Input.GetMouseButtonDown(1))
                        {
                            _uiManager.SpawnWeakPoint(false);
                        }
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
