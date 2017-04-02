using Tower;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager>
{
    private TowerBtn _towerPressed;
    private SpriteRenderer _spriteRenderer;
    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider.CompareTag("BuildSites"))
            {
                hit.collider.tag = "BuildSiteFull";
                PlaceTower(hit);
                
            }

            
        }
        if (_spriteRenderer.enabled)
        {
            FallowMouse();
        }


    }

    private void FallowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x,transform.position.y);
    }

    public void EnableDragSprite(Sprite sprite)
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = sprite;
    }
    public void DisableDragSprite()
    {
        _spriteRenderer.enabled = false;
      
    }
    private void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && _towerPressed != null)
        {
            GameObject newTower = Instantiate(_towerPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            DisableDragSprite();
        }
    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        _towerPressed = towerSelected;
         EnableDragSprite(_towerPressed.DragSprite);
    }
}