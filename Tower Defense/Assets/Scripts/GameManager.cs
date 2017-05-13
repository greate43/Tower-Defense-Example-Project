using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    Next,
    Play,
    GameOver,
    Win,
    GameStarted
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

    [SerializeField] private int _totalEnemies = 3;
    [SerializeField] private int _enemiesPerSpawn;

    private int _waveNumber = 0;
    private int _totalMoney = 10;

    private int _totalEscaped = 0;
    private int _roundEscaped = 0;
    private int _totalKilled = 0;
    private int _whichEnemyToSpawn = 0;


    public int TotalEscaped
    {
        set { _totalEscaped = value; }
        get { return _totalEscaped; }
    }

    public int RoundEscaped
    {
        set { _roundEscaped = value; }
        get { return _roundEscaped; }
    }

    public int TotalKilled
    {
        set { _totalKilled = value; }
        get { return _totalKilled; }
    }

    public int TotalMoney
    {
        get { return _totalMoney; }
        set
        {
            _totalMoney = value;
            _totalMoneyLbl.text = _totalMoney.ToString();
        }
    }

    private GameStatus _currentGameStatus = GameStatus.Play;

    const float SpawnDelay = 0.5f;

    public List<Enemy> EnemiesList = new List<Enemy>();


    void Start()
    {
        _playButton.gameObject.SetActive(false);
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
                if (EnemiesList.Count < _totalEnemies)
                {
                    GameObject newEnemy = Instantiate(_enemies[0]);
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

    public void IsWaveOver()
    {
        _totalEscapedLbl.text = "Eascaped " + TotalEscaped + "/10";
        if (RoundEscaped + TotalKilled == _totalEnemies)
        {
           
            SetCurrentGameState();
            ShowMenu();
        }
    }

    private void SetCurrentGameState()
    {
        if (TotalEscaped >= 10)
        {
            _currentGameStatus = GameStatus.GameOver;
        }
        else if (_waveNumber == 0 && TotalKilled + RoundEscaped == 0)
        {
            _currentGameStatus = GameStatus.Play;
        }
        else if (_waveNumber >= _totalWave)
        {
            _currentGameStatus = GameStatus.Win;
        }
        else
        {
            _currentGameStatus = GameStatus.Next;
        }
    }

    public void ShowMenu()
    {
        switch (_currentGameStatus)
        {
            case GameStatus.Play:
                _playButtonLbl.text = "Play";

                break;
            case GameStatus.Next:
                _playButtonLbl.text = "Next Wave";

                break;
            case GameStatus.Win:
                _playButtonLbl.text = "Play Again!";

                break;
            case GameStatus.GameOver:
                _playButtonLbl.text = "Play Again!";
                break;
        }
        _playButton.gameObject.SetActive(true);
    }

    public void PlaybuttonPressed()
    {
        switch (_currentGameStatus)
        {
            case GameStatus.Next:
                _waveNumber += 1;
                _totalEnemies += _waveNumber;
               
 
                break;
            default:
                _totalEnemies = 3;
                _totalEscaped = 0;
                TotalMoney = 10;
                _waveNumber = 0;
                TowerManager.Instance.RenameTagsBuildSites();
                TowerManager.Instance.DestroyAllTowers();
          
                _totalMoneyLbl.text = TotalMoney.ToString();
                _totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
                break;
        }
          
        DestoryAllEnemy();
        TotalKilled = 0;
        RoundEscaped = 0;
        _waveNumber += 1;
       _currentWaveLbl.text = "Wave " + _waveNumber ;
        StartCoroutine(Spawn());
        _playButton.gameObject.SetActive(false);
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