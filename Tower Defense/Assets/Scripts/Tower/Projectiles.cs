using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProType
{
    Rock,
    Arrow,
    Fireball
}
public class Projectiles : MonoBehaviour
{

    [SerializeField] private int _attackStrenght;
   [SerializeField] private ProType _projectileType;

    public int AttackStrenght
    {
        get { return _attackStrenght; }
    }
    public ProType ProjectileType
    {
        get { return _projectileType; }
    }

}
