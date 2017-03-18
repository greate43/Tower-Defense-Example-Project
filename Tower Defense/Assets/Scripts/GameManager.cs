using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
  

    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private int _maxEnemiesOnTheScreen;
    [SerializeField] private int _totalEnemies;
    [SerializeField] private int _enemiesPerSpawn;

                     private int _enemiesOnTheScreen =0;

               const float _spawnDelay =0.5f;

   
    void Start()
    {
        StartCoroutine(Spawn());
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

    public void RemoveEnemyFromScreen()
    {
        if (_enemiesOnTheScreen > 0)
        {
            _enemiesOnTheScreen -= 1;
            print(_enemiesOnTheScreen);
        }
    }
    
    IEnumerator Spawn()
    {
        if (_enemiesPerSpawn > 0 && _enemiesOnTheScreen < _totalEnemies)
        {
            for (int i = 0; i < _enemiesPerSpawn; i++)
            {
                if (_enemiesOnTheScreen < _maxEnemiesOnTheScreen)
                {
                    GameObject newEnemy = Instantiate(_enemies[1]);
                    newEnemy.transform.position = _spawnPoint.transform.position;
                    _enemiesOnTheScreen += 1;
                }
            }
        }
         
         yield return new WaitForSeconds(_spawnDelay);

        StartCoroutine(Spawn());
    }


   
}
