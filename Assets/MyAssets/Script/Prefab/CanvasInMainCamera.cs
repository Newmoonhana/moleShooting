using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasInMainCamera : MonoBehaviour
{
    Camera mainCamera;
    Canvas this_canvas;

    private void Awake()
    {
        this_canvas = this.GetComponent<Canvas>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        this_canvas.renderMode = RenderMode.ScreenSpaceCamera;
        this_canvas.worldCamera = mainCamera;
        this_canvas.sortingOrder = 2000;
    }
}
