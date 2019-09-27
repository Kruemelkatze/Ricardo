using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Phone : MonoBehaviour
{
    public SpriteRenderer coloredSprite;

    public Vector2 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color color)
    {
        if (coloredSprite != null)
        {
            coloredSprite.color = color;
        }
    }
}
