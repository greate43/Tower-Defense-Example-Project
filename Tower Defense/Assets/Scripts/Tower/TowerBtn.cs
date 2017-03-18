using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBtn : MonoBehaviour {

   [SerializeField] private GameObject _towerObject;

    public GameObject TowerObject
    {
        get
        {
            return _towerObject;
        }
    } 
}
