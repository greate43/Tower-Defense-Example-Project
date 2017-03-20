using UnityEngine;

namespace Tower
{
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
}
