using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    Vector2 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        transform.position = new Vector3(Mathf.Lerp(-1.0f, 1.0f, mousePosition.x/Screen.width), Mathf.Lerp(-1.0f, 1.0f, mousePosition.y/Screen.height), 0);
    }
}
