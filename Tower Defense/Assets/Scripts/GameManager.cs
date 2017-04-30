using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    Next,Play,GameOver,Win
}
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int _totalWave = 10;
    [SerializeField] private Text _totalMoneyLbl;
    [SerializeField] private Text _currentWaveLbl;
    [SerializeField] private Text _playButtonLbl;
    [SerializeField] private Button _playButton;
    [SerializeField] private Text _totalEscapedLbl;

    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private int _maxEnemiesOnTheScreen;
    [SerializeField] private int _totalEnemies;
    [SerializeField] private int _enemiesPerSpawn;

    private int _waveNumber = 0;
    private int _totalMoney = 10;
    
    private int _totalEscaped = 0;
    private int _roundEscaped = 0;
    private int _totalKilled = 0;
    private int _whichEnemyToSpawn = 0;
 
    private GameStatus currentGameStatus=GameStatus.Play;

    const float SpawnDelay = 0.5f;

    public List<Enemy> EnemiesList = new List<Enemy>();

    public int TotalMoney
    {
        get { return _totalMoney; }
        set
        {
            _totalMoney = value;
            _totalMoneyLbl.text = _totalMoney.ToString();
        }
    }

    void Start()
    {
      _playButton.gameObject.SetActive(false);
        StartCoroutine(Spawn());
        ShowMenu();
    }

    void Update()
    {
        HandleEscape();
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

    public void AddMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void SubtractMoney(int amount)
    {
        TotalMoney -= amount;
    }


    public void ShowMenu()
    {
        switch (currentGameStatus)
        {
                case GameStatus.Play:
                    _playButtonLbl.text = "Play";

                break;
                case GameStatus.Next:
                    _playButtonLbl.text = "Next Wave";

                break;
                case GameStatus.Win:
                    _playButtonLbl.text = "Play";

                break;
                case GameStatus.GameOver:
                    _playButtonLbl.text = "Play Again!";

                break;
        }
        _playButton.gameObject.SetActive(true);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TowerManager.Instance.DisableDragSprite();
            TowerManager.Instance.TowerPressed = null;
        }
    }
}