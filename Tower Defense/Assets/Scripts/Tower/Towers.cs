using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    public class Towers : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenAttacks;
        [SerializeField] private float _attackRadius;
        [SerializeField] private Projectiles _projectile;
        private bool _isAttack = false;
        private Enemy _targetEnemy = null;
        private float _attackCounter;
  

        void Start()
        {
          
        }

        public virtual void Update()
        {
            _attackCounter -= Time.deltaTime;
            if (_targetEnemy == null ||_targetEnemy.IsDead)
            {
                Enemy nearestEnemy = GetNearestEnemyInRange();
                if (nearestEnemy != null && Vector2.Distance(transform.position, nearestEnemy.transform.position) <=
                    _attackRadius)
                {
                    _targetEnemy = nearestEnemy;
                }
            }
            else
            {
                if (_attackCounter <= 0f)
                {
                    _isAttack = true;
                    // Reset attack counter
                    _attackCounter = _timeBetweenAttacks;
                }
                else
                {
                    _isAttack = false;
                }
                if (Vector2.Distance(transform.position, _targetEnemy.transform.position) > _attackRadius)
                {
                    _targetEnemy = null;
                }
            }
        }

        void FixedUpdate()
        {
            if (_isAttack)
            {
                Attack();
            }
        }

        public void Attack()
        {
            _isAttack = false;
            Projectiles newProjectile = Instantiate(_projectile) as Projectiles;
            newProjectile.transform.localPosition = transform.localPosition;

            if (_targetEnemy == null)
            {
                Destroy(newProjectile);
            }
            else
            {
                StartCoroutine(MoveProjectile(newProjectile));
            }
        }

        IEnumerator MoveProjectile(Projectiles projectile)
        {
            while (GetTargetDistance(_targetEnemy) > 0.20f && projectile != null && _targetEnemy != null)
            {
                var dir = _targetEnemy.transform.localPosition - transform.localPosition;
                var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
                projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition,
                    _targetEnemy.transform.localPosition, 5f * Time.deltaTime);
                yield return null;
            }

            if (projectile != null || _targetEnemy == null)
            {
                Destroy(projectile);
            }
        }

        private float GetTargetDistance(Enemy thisEnemy)
        {
            if (thisEnemy == null)
            {
                thisEnemy = GetNearestEnemyInRange();
                if (thisEnemy == null)
                {
                    return 0f;
                }
            }
            return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
        }

        private List<Enemy> GetEnemiesInRange()
        {
            List<Enemy> enemiesInRange = new List<Enemy>();
            foreach (Enemy enemy in GameManager.Instance.EnemiesList)
            {
                if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= _attackRadius)
                {
                    enemiesInRange.Add(enemy);
                }
            }
            return enemiesInRange;
        }

        private Enemy GetNearestEnemyInRange()
        {
            Enemy nearestEnemy = null;
            float smallestDistance = float.PositiveInfinity;
            foreach (Enemy enemy in GetEnemiesInRange())
            {
                if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance)
                {
                    smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                    nearestEnemy = enemy;
                }
            }
            return nearestEnemy;
        }
    }
}