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

        //後でswitchに入れてエラー対策行う
        _player = FindAnyObjectByType<Player>();
        _enemy = FindAnyObjectByType<Enemy>();
        _uiManager = GetComponent<UIManager>();
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
                    case InGamePhase.CountDown:

                    break;
                    case InGamePhase.Chose:

                    break;
                    case InGamePhase.Attack:

                    break;
                }
            break;
            case SceneDivision.Result://リザルトシーンで実行したいこと 

            break;
        }
    }
}
