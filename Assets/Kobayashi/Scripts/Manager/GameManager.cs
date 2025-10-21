using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player _player;
    private Enemy _enemy;
    private InGamePhase _gamePhase;
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

        _player = FindAnyObjectByType<Player>();
        _enemy = FindAnyObjectByType<Enemy>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
