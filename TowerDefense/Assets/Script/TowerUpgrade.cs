using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class TowerUpgrade : MonoBehaviour
{
    public GameObject canvas;
    public GameObject upgradeCanvas;
    private Camera camera;
    private Ray ray;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get the screen position of the mouse click or touch
            Vector3 screenPosition = Input.mousePosition;

            // Convert the screen position to a world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            // Perform a raycast at the world position
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                canvas.SetActive(false);
                upgradeCanvas.SetActive(true);

            }
        }
    }

    void HandleRaycast(Vector2 input)
    {
        ray = camera.ScreenPointToRay(input);
        if(Physics.Raycast(ray, out hit) )
        {
            print("block");
        }

    }
    

}
