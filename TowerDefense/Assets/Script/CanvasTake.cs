using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTake : MonoBehaviour
{
    public GameObject canvas;

    public void OnActive()
    {
        canvas.SetActive(true);
    }

    public void OnDisable()
    {
        canvas.SetActive(false);
    }
}
