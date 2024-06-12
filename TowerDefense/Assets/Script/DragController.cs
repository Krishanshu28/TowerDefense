using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rt;
    
    [NonSerialized]public Image img;

    private Vector3 originPos;
    private bool drag;
    private bool isCreated = true;


    
    
    private void Awake()
    {
        
        rt = GetComponent<RectTransform>();
        
        
        img = GetComponent<Image>();
        originPos = rt.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        drag = true;
        
        img.maskable = false;
    }

    public void OnDrag(PointerEventData eventData) 
    {
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 screenPosition = eventData.position;
        // Convert screen position to world position
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // You can call your function to spawn the object here
        GameManager.instance.SpawnObjectAtPosition(worldPosition);
        //Color c = img.color;
        //c.a = 0f;
        //img.color = c;

        rt.anchoredPosition = originPos;
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
    }*/

    private void OnEnable()
    {
        drag = false;
        
        img.maskable = true;
        //rt.anchoredPosition = originPos;

        Color c = img.color;
        c.a = 1f;
        img.color = c;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }
    
}
