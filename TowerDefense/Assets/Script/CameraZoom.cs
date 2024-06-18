using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 0.5f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    public float mapWidth = 100f;
    public float mapHeight = 100f;

    public float dragSpeed = 2f;    // Speed of dragging
    private Vector3 dragOrigin;


    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null || !cam.orthographic)
        {
            Debug.LogError("This script requires an Orthographic Camera.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleZoom();
        HandleDrag();
    }

    void HandleZoom()
    {
        //zoom with mouse scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

        //zoom with touch gestures
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchtoDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchOneMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchtoDeltaMag - touchOneMag;

            cam.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }

        // Clamp the camera position
        float cameraHalfWidth = cam.orthographicSize * cam.aspect;
        float cameraHalfHeight = cam.orthographicSize;

        float clampedX = Mathf.Clamp(transform.position.x, cameraHalfWidth, mapWidth - cameraHalfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, cameraHalfHeight, mapHeight - cameraHalfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += difference;
            ClampCamera();
        }
    }

    void ClampCamera()
    {
        float cameraHalfWidth = cam.orthographicSize * cam.aspect;
        float cameraHalfHeight = cam.orthographicSize;

        float clampedX = Mathf.Clamp(cam.transform.position.x, cameraHalfWidth, mapWidth - cameraHalfWidth);
        float clampedY = Mathf.Clamp(cam.transform.position.y, cameraHalfHeight, mapHeight - cameraHalfHeight);

        cam.transform.position = new Vector3(clampedX, clampedY, cam.transform.position.z);
    }
}
