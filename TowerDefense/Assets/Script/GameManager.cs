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

    public GameObject objectToSpawn;

    public Camera cam;

    private void Awake()
    {
        instance = this; 
        
    }

    
    public void SpawnObjectAtPosition(Vector2 position)
    {
        // Reference to your prefab
        //GameObject objectToSpawn = Resources.Load<GameObject>("Tower1/Circle");

        // Calculate the tile position (assuming your tiles are 1x1 units)
        //Vector2 tilePosition = new Vector2(
         //   (position.x),
         //   (position.y)
       // );

        // Convert tile position back to world position
        //Vector2 snappedPosition = new Vector2(tilePosition.x, tilePosition.y);

        // Instantiate the object at the snapped position
        Instantiate(objectToSpawn, position, Quaternion.identity);
       

    }
}
