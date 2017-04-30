
using UnityEngine;
using UnityEngine.Assertions;

namespace Tower
{
    public class TowerBtn : MonoBehaviour {

        [SerializeField] private GameObject _towerObject;
        [SerializeField] private Sprite _dragSprite;
        [SerializeField] private int _towerPrice;

        private void Awake()
        {
            Assert.IsNotNull(_dragSprite);
            Assert.IsNotNull(_towerObject);
        }

        public Sprite DragSprite
        {
            get { return _dragSprite; }
        }

        public GameObject TowerObject
        {
            get
            {
                return _towerObject;
            }
        }

        public int TowerPrice
        {
            get { return _towerPrice; }
        }

    }
}
