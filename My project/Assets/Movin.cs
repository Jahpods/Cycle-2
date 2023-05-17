using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movin : MonoBehaviour
{
    [SerializeField]
    private Transform[] parrallaxItems;
    [SerializeField]
    private float magnitude;

    [SerializeField]
    private List<Vector3> startingPositions;

    void Start(){
        foreach(Transform p in parrallaxItems){
            startingPositions.Add(p.position);
        }
    }

    void Update(){
        Vector3 mousePos = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height, 0);
        mousePos = mousePos - new Vector3(0.5f, 0.5f,0);
        for(int i = 0; i < parrallaxItems.Length; i++){
            float distance = 1;
            if(i <= 1) {distance = 0.5f;}
            else if(i == 2) { distance = 2f;}
            else if(i == 3) {distance = 1.5f;}
            else if(i == 4) {distance = 1f;}
            else if(i == 5) {distance = 1.9f;}
            parrallaxItems[i].position = startingPositions[i] + (mousePos * magnitude * distance);
        }
    }
}
