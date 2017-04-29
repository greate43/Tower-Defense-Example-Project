using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private int _target = 0;
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private Transform[] _wayPoint;
    [SerializeField] private float _navigationUpdate;

    private Transform _enemy;
    private float _navigationTime = 0;
    // Use this for initialization
    void Start () {
        _enemy = GetComponent<Transform>();
        GameManager.Instance.RegisterEnemy(this);
    }
	
	// Update is called once per frame
	void Update () {
		if(_wayPoint != null)
        {
            _navigationTime += Time.deltaTime;
            if(_navigationTime > _navigationUpdate)
            {
                if(_target < _wayPoint.Length)
                {
                    _enemy.position = Vector2.MoveTowards(_enemy.position, _wayPoint[_target].position, _navigationTime);
                }
                else
                {
                    _enemy.position = Vector2.MoveTowards(_enemy.position,_exitPoint.position, _navigationTime);
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
         
        } else if (other.CompareTag("Finish"))
        {
           GameManager.Instance.UnRegisterEnemy(this);
           
        }
        else if (other.CompareTag("Projectiles"))
        {
           Destroy(other.gameObject);
        }
     
    }
}
