using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale =
            new Vector3(Camera.main.orthographicSize * 2.0f * Camera.main.pixelWidth / Camera.main.pixelHeight,
                Camera.main.orthographicSize * 2.0f, 0.1f);
    }
}