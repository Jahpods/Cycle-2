using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Mathf.Sin(Time.time) * Time.deltaTime * 0.1f;
        transform.Rotate(Vector3.up, Time.deltaTime * 10f);
    }
}
