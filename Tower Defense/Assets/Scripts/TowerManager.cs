using System.Collections.Generic;
using Tower;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager>
{
    public TowerBtn TowerPressed { get; set; }
    public List<GameObject> TowerList = new List<GameObject>();
    public List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D buildTile;
    private SpriteRenderer _spriteRenderer;
    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
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
                // hit.collider.tag = "BuildSiteFull";
                buildTile = hit.collider;
                buildTile.tag = "BuildSiteFull";
                RegisterBuildSite(buildTile);
                PlaceTower(hit);
                
            }

            
        }
        if (_spriteRenderer.enabled)
        {
            FallowMouse();
        }


    }
    public void RegisterBuildSite(Collider2D buildTag)
    {
        // site.collider.tag = "BuildSiteFull";
        BuildList.Add(buildTag);
    }

    public void RenameTagsBuildSites()
    {
        foreach (Collider2D buildTag in BuildList)
        {
            buildTag.tag = "BuildSites";
        }
        BuildList.Clear();
    }

    public void DestroyAllTowers()
    {
        foreach (GameObject tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void RegisterTower(GameObject tower)
    {
        TowerList.Add(tower);
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
        if (!EventSystem.current.IsPointerOverGameObject() && TowerPressed != null && TowerPressed.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            GameObject newTower = Instantiate(TowerPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            RegisterTower(newTower);
            BuyTower(TowerPressed.TowerPrice);
            DisableDragSprite();
        }
    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        if (towerSelected.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            TowerPressed = towerSelected;
           
            EnableDragSprite(towerSelected.DragSprite);
        }
    }

    public void BuyTower(int price)
    {
        GameManager.Instance.SubtractMoney(price);
      
    }


}