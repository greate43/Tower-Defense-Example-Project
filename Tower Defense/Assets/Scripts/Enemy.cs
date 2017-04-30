using Tower;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _target = 0;
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private Transform[] _wayPoint;
    [SerializeField] private float _navigationUpdate;
    [SerializeField] private int _healthPoints = 100;
    [SerializeField] private int _rewardAmount;
    private Transform _enemy;
    public bool IsDead { get; private set; }
    private float _navigationTime = 0;
    private Animator _anim;
    private Collider2D _enemyCollider2D;
    // Use this for initialization
    void Start()
    {
        _enemy = GetComponent<Transform>();
        _enemyCollider2D = GetComponent<Collider2D>();
        _anim = GetComponent<Animator>();
        GameManager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (_wayPoint != null && !IsDead)
        {
            _navigationTime += Time.deltaTime;
            if (_navigationTime > _navigationUpdate)
            {
                if (_target < _wayPoint.Length)
                {
                    _enemy.position = Vector2.MoveTowards(_enemy.position, _wayPoint[_target].position,
                        _navigationTime);
                }
                else
                {
                    _enemy.position = Vector2.MoveTowards(_enemy.position, _exitPoint.position, _navigationTime);
                }
                _navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            _target += 1;
        }
        else if (other.CompareTag("Finish"))
        {
            GameManager.Instance.UnRegisterEnemy(this);
        }
        else if (other.CompareTag("Projectiles"))
        {
            Projectiles newProjectiles = other.GetComponent<Projectiles>();
            EnemyHit(newProjectiles.AttackStrenght);
            Destroy(other.gameObject);
        }
    }

    public void EnemyHit(int hitpoints)
    {
        if (_healthPoints - hitpoints > 0)
        {
            _healthPoints -= hitpoints;
            //call hurt animation
            _anim.Play("hurt");
        }
        else
        {
            // die animations 
            Die();
            _anim.SetTrigger("DidDie");
        }

    }

    public void Die()
    {
        IsDead = true;
        _enemyCollider2D.enabled = false;
 
    }
}