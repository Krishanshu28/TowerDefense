using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject canvas;
    public GameObject tower;
    public GameObject map;

    public Camera cam;

    private void Awake()
    {
        instance = this; 
        
    }

    public void CreateTower(Vector3 pos)
    {
        print("1");

        //RectTransform rect = tower.GetComponent<RectTransform>();
        //rect.anchoredPosition = new Vector2(pos.x, -pos.y);

        //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        //tower = (Instantiate(tower, point, Quaternion.identity, map.transform));



    }
    public void SpawnObjectAtPosition(Vector2 position)
    {
        // Reference to your prefab
        GameObject objectToSpawn = Resources.Load<GameObject>("Tower1/Circle");

        // Calculate the tile position (assuming your tiles are 1x1 units)
        Vector2Int tilePosition = new Vector2Int(
            Mathf.RoundToInt(position.x),
            Mathf.RoundToInt(position.y)
        );

        // Convert tile position back to world position
        Vector2 snappedPosition = new Vector2(tilePosition.x, tilePosition.y);

        // Instantiate the object at the snapped position
        Instantiate(objectToSpawn, snappedPosition, Quaternion.identity, map.transform);
    }
}
