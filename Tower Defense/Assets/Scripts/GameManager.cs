using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
  

    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private int _maxEnemiesOnTheScreen;
    [SerializeField] private int _totalEnemies;
    [SerializeField] private int _enemiesPerSpawn;

               

               const float SpawnDelay =0.5f;

   public List<Enemy> EnemiesList=new List<Enemy>();
   
    void Start()
    {
        StartCoroutine(Spawn());
    }

    


    private IEnumerator Spawn()
    {
        if (_enemiesPerSpawn > 0 && EnemiesList.Count < _totalEnemies)
        {
            for (int i = 0; i < _enemiesPerSpawn; i++)
            {
                if (EnemiesList.Count < _maxEnemiesOnTheScreen)
                {
                    GameObject newEnemy = Instantiate(_enemies[1]);
                    newEnemy.transform.position = _spawnPoint.transform.position;
               
                }
            }
        }
         
         yield return new WaitForSeconds(SpawnDelay);

        StartCoroutine(Spawn());
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemiesList.Add(enemy);

    }
    public void UnRegisterEnemy(Enemy enemy)
    {
        EnemiesList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestoryAllEnemy()
    {
        foreach (Enemy enemy in EnemiesList)
        {
           Destroy(enemy.gameObject);
        }
        EnemiesList.Clear();
    }

}
