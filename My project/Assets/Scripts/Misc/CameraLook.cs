using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField]
    private float moveDist;
    Vector2 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        transform.position = new Vector3(Mathf.Lerp(-moveDist, moveDist, mousePosition.x/Screen.width), Mathf.Lerp(-moveDist, moveDist, mousePosition.y/Screen.height), 0);
    }
}
