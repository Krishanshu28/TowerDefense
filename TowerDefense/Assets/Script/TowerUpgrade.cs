using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;

public class TowerUpgrade : MonoBehaviour
{
    public GameObject canvas;
    //public CanvasTake upgradeCanvas;
    private Camera camera;
    private Ray ray;
    private RaycastHit hit;
    private bool menuCheck;


    private Vector3 offset;
    private float zCoord;
    private Tilemap tilemap;
    private bool isDragging = false;
    private int touchID; 

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        tilemap = FindObjectOfType<Tilemap>();
    }

    //private void Update()
    //{
        /*// Handle mouse input
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            OnMouseDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }*/
    //}
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
                menuCheck = true;
            }
            else
            {
                menuCheck = false;
            }
            if (menuCheck)
            {
                //canvas.SetActive(false);
               // upgradeCanvas.OnActive();

            }
            else
            {
                //canvas.SetActive(true);
                //upgradeCanvas.OnDisable();

            }

        }
        

        // Handle touch input
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                OnTouchDown(touch);
            }
            else if (touch.phase == TouchPhase.Moved && isDragging && touch.fingerId == touchID)
            {
                OnTouchDrag(touch);
            }
            else if (touch.phase == TouchPhase.Ended && touch.fingerId == touchID)
            {
                isDragging = false;
            }
        }*/

    }

    void HandleRaycast(Vector2 input)
    {
        ray = camera.ScreenPointToRay(input);
        if(Physics.Raycast(ray, out hit) )
        {
            print("block");
        }

    }

    /*}
    void OnTouchDown(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            isDragging = true;
            touchID = touch.fingerId;

            // Save the distance between the object and the camera
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

            // Calculate offset
            offset = gameObject.transform.position - GetTouchAsWorldPoint(touch.position);
        }
    }

    private Vector3 GetTouchAsWorldPoint(Vector3 touchPosition)
    {
        // Pixel coordinates of touch (x,y)
        Vector3 touchPoint = touchPosition;

        // z coordinate of game object on screen
        touchPoint.z = zCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(touchPoint);
    }

    void OnTouchDrag(Touch touch)
    {
        // Update object position with touch position and offset
        Vector3 targetPosition = GetTouchAsWorldPoint(touch.position) + offset;
        Vector3Int cellPosition = tilemap.WorldToCell(targetPosition);
        Vector3 snappedPosition = tilemap.GetCellCenterWorld(cellPosition);

        transform.position = snappedPosition;

    /*void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            isDragging = true;

            // Save the distance between the object and the camera
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            zCoord += (zCoord * -1);
            print(zCoord);
            // Calculate offset
            offset = gameObject.transform.position - GetMouseAsWorldPoint();
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = zCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        // Update object position with mouse position and offset
        Vector3 targetPosition = GetMouseAsWorldPoint() + offset;
        transform.position = targetPosition;
    }

    */
}
