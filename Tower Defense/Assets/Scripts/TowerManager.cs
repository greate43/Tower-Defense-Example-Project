using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager>
{
    private TowerBtn _towerPressed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if(hit.collider.CompareTag("BuildSites"))
            {

                hit.collider.tag = "BuildSiteFull";
                PlaceTower(hit);
                }
        }
	}

    private void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && _towerPressed != null)
        {
            GameObject newTower = Instantiate(_towerPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
        }

    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        _towerPressed = towerSelected;
    }

}
