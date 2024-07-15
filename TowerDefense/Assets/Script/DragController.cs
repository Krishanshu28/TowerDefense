using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rt;
    
    [NonSerialized]public Image img;

    private Vector3 originPos;
   
    


    public static DragController instance;
    
    private void Awake()
    {
       


        rt = GetComponent<RectTransform>();
        
        
        img = GetComponent<Image>();
        originPos = rt.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        CameraZoom.instance.drag = false;
        
    }

    public void OnDrag(PointerEventData eventData) 
    {
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        CameraZoom.instance.drag = true;
        // Spawn the prefab at the drop position
        Vector2 screenPosition = eventData.position;
       
            // Convert screen position to world position
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // You can call your function to spawn the object here
        if (IsValidDropPosition(worldPosition))
        {
            GameManager.instance.SpawnObjectAtPosition(worldPosition);
            
            
            rt.anchoredPosition = originPos;
        }
        else
        {
            print("coll");
            rt.anchoredPosition = originPos;
        }
        
    }
    private bool IsValidDropPosition(Vector3 position)
    {
        // Check if there is any collider at the given position
        Collider2D collider = Physics2D.OverlapPoint(position);
        print(collider.tag);
        if(collider.CompareTag("Placable"))
        {
            // Return true if there is no collider at the position, meaning it's a valid drop position
            return true;
        }
        return false;
        
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Respawn")
        {
            if (isCreated)
            {
                print("in");
                GameManager.instance.CreateTower(originPos);
                isCreated = false;
            }
            
        }
        Color c = img.color;
        //c.a = 0.5f;
        //img.color = c;
    }

    private void OnEnable()
    {
        drag = false;
        
        img.maskable = true;
        //rt.anchoredPosition = originPos;

        Color c = img.color;
        c.a = 1f;
        img.color = c;

    }*/

    public void OnPointerDown(PointerEventData eventData)
    {
        //Vector2 screenPosition = eventData.position;
        // Convert screen position to world position
        //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // You can call your function to spawn the object here
        //GameManager.instance.SpawnObjectAtPosition(worldPosition);
        CameraZoom.instance.drag = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CameraZoom.instance.drag = true;
    }


}
