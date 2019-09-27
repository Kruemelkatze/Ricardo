using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public string sortingLayerName = "Default";
    public int orderInLayer;
    
    public Camera AssignedCamera;
    private Renderer _renderer;
    private Vector2 _camSize;
    
    private void Awake()
    {
        AssignedCamera = AssignedCamera ? AssignedCamera : Camera.main;
        _renderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var camHeight = 2 * AssignedCamera.orthographicSize;
        var camWidth = camHeight * AssignedCamera.aspect;
        _camSize = new Vector2(camWidth, camHeight);
        
        var quadSize = _renderer.bounds.size;
        var textureWidth = quadSize.x / camWidth;
        var textureHeight = quadSize.y / camHeight;
        _renderer.material.mainTextureScale = new Vector2(textureWidth, textureHeight);

        _renderer.sortingLayerName = sortingLayerName;
        _renderer.sortingOrder = orderInLayer;
    }

    // Update is called once per frame
    void Update()
    {
        // Get LowerBound
        var bounds = _renderer.bounds.min;
        var offsetX = (bounds.x + _camSize.x / 2) / _camSize.x;
        var offsetY = (bounds.y + _camSize.y / 2) / _camSize.y;
        _renderer.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
