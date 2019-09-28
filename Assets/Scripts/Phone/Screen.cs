using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Screen : MonoBehaviour
{
    public string sortingLayerName = "Default";
    public int orderInLayer;

    public Camera AssignedCamera;
    private Renderer _renderer;
    private Vector2 _camSize;

    private bool fixedTexture = false;

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
        if (!fixedTexture)
        {
            // Get LowerBound
            var offset = GetTextureOffset();
            _renderer.material.mainTextureOffset = offset;
        }
    }

    private Vector2 GetTextureOffset()
    {
        var bounds = _renderer.bounds.min;
        var offsetX = (bounds.x + _camSize.x / 2) / _camSize.x;
        var offsetY = (bounds.y + _camSize.y / 2) / _camSize.y;
        return new Vector2(offsetX, offsetY);
    }

    public Texture2D CreateSnapshot()
    {
        if (fixedTexture)
        {
            return _renderer.material.mainTexture as Texture2D;
        }

        var renderTexture = _renderer.material.mainTexture as RenderTexture;
        
        var snapshot = new Texture2D(renderTexture.width, renderTexture.height);

        RenderTexture.active = renderTexture;
        snapshot.ReadPixels(new Rect(0,0, renderTexture.width, renderTexture.height), 0, 0);
        snapshot.Apply();

        fixedTexture = true;
        _renderer.material.mainTexture = snapshot;
        return snapshot;
    }

    public Texture2D FaultyCreateSnapshot()
    {
        if (fixedTexture)
        {
            return _renderer.material.mainTexture as Texture2D;
        }

        var renderTexture = _renderer.material.mainTexture as RenderTexture;
        var offset = GetTextureOffset();
        var factorX = 1f;
        var factorY = 1f;

        switch (transform.rotation.z)
        {
            case (0):
                factorY = 2f;
                break;
            case (180):
            case (-180):
                factorY = 2f;
                break;
            case (-90):
                factorX = 0.5f;
                break;
            case (90):
                factorX = 2;
                break;
        }

        offset = new Vector2(offset.x * renderTexture.width * factorX, offset.y * renderTexture.height * factorY);
        var size = _renderer.material.mainTextureScale;
        size = new Vector2(size.x * renderTexture.width, size.y * renderTexture.height);

        var snapshot = new Texture2D((int) size.x, (int) size.y);

        RenderTexture.active = renderTexture;
        snapshot.ReadPixels(new Rect((int) offset.x, (int) offset.y, (int) size.x, (int) size.y), 0, 0);
        snapshot.Apply();

        fixedTexture = true;
        _renderer.material.mainTexture = snapshot;
        _renderer.material.mainTextureOffset = Vector2.zero;
        _renderer.material.mainTextureScale = Vector2.one;

        return snapshot;
    }
}