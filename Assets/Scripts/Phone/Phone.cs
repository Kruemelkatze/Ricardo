using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Phone : MonoBehaviour
{
    public SpriteRenderer coloredSprite;

    public Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    public const float smoothTime = 0.3F;
    private const float StartOffset = 6;

    private Screen _screen;

    public bool isHorizontal;
    
    void Start()
    {
        _screen = GetComponentInChildren<Screen>();
        
        var camBounds = GetCameraBounds();
        // Get nearest edge of targetPosition
        var diffTop = Math.Abs(camBounds.y + camBounds.height - targetPosition.y);
        var diffBottom = Math.Abs(targetPosition.y - camBounds.y);
        var diffToLeft = Math.Abs(targetPosition.x - camBounds.x);
        var diffToRight = Math.Abs(camBounds.x + camBounds.width - targetPosition.x);

        var min = Math.Min(Math.Min(diffTop, diffBottom), Math.Min(diffToLeft, diffToRight)); // Thanks, Unity
        var tolerance = 0.05f;
        if (Math.Abs(min - diffTop) < tolerance)
        {
            // Top
            transform.Rotate(Vector3.forward, 180);
            _screen.transform.Rotate(Vector3.forward, -180);
        }
        else if (Math.Abs(min - diffBottom) < tolerance)
        {
            // Bottom
            // Everything is fine
        }
        else if (Math.Abs(min - diffToLeft) < tolerance)
        {
            // Left
            transform.Rotate(Vector3.forward, -90);
            _screen.transform.Rotate(Vector3.forward, 90);
            // Flip screen size
            _screen.transform.localScale = FlipV3(_screen.transform.localScale);
            isHorizontal = true;
        }
        else
        {
            // Right
            transform.Rotate(Vector3.forward, 90);
            _screen.transform.Rotate(Vector3.forward, -90);
            // Flip screen size
            _screen.transform.localScale = FlipV3(_screen.transform.localScale);
            isHorizontal = true;
        }
        
        transform.Translate(Vector3.down * StartOffset);
    }

    private Vector3 FlipV3(Vector3 v)
    {
        return new Vector3(v.y, v.x, v.z);
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    Rect GetCameraBounds()
    {
        var camHeight = 2 * Camera.main.orthographicSize;
        var camWidth = camHeight * Camera.main.aspect;

        var camPos = Camera.main.transform.position;
        return new Rect(
            camPos.x - camWidth / 2,
            camPos.y - camHeight / 2,
            camPos.x + camWidth,
            camPos.y + camHeight
        );
    }

    public void SetColor(Color color)
    {
        if (coloredSprite != null)
        {
            coloredSprite.color = color;
        }
    }

    public void Despawn()
    {
        targetPosition = targetPosition - (transform.rotation * Vector3.up * 1.5f *StartOffset);
        Destroy(gameObject, smoothTime + 0.01f);
    }
}