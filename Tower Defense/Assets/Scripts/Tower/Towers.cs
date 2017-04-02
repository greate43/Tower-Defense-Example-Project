using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    public class Towers : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenAttacks;
        [SerializeField] private float _attackRadius;
        private Projectiles _projectiles;
        private Enemy _targetEnemy;
        private float _attackCounter;


        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
        }

        private List<Enemy> GetEnemiesInRange()
        {
            List<Enemy> enemiesInRange=new List<Enemy>();
            foreach (Enemy enemy in GameManager.Instance.EnemiesList)
            {
                if (Vector2.Distance(transform.position,enemy.transform.position) <= _attackRadius)
                {
                    enemiesInRange.Add(enemy);
                }
            }
            return enemiesInRange;
        }

        private Enemy GetNearestEnemyInRage()
        {
            Enemy nearestEnemy = null;
            float smallestDistance = float.PositiveInfinity;

            foreach (Enemy enemy in GetEnemiesInRange())
            {
                if (Vector2.Distance(transform.position, enemy.transform.position) <= smallestDistance)
                {
                    smallestDistance = Vector2.Distance(transform.position, enemy.transform.position);
                    nearestEnemy = enemy;
                }
            }
            return nearestEnemy;
        }


    }
}
