using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private int _maxEnemiesOnTheScreen;
    [SerializeField] private int _totalEnemies;
    [SerializeField] private int _enemiesPerSpawn;
                     private int _enemiesOnTheScreen =0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (_enemiesPerSpawn > 0 && _enemiesOnTheScreen < _totalEnemies)
        {
            for (int i = 0; i < _enemiesPerSpawn; i++)
            {
                if (_enemiesOnTheScreen < _maxEnemiesOnTheScreen)
                {
                    GameObject newEnemy = Instantiate(_enemies[0]);
                    newEnemy.transform.position = _spawnPoint.transform.position;
                    _enemiesOnTheScreen += 1;
                }
            }
        }
    }
   
}
