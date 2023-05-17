using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUI : MonoBehaviour
{
    Vector3 targetPosition;
    Vector3 newPosition;

    bool Switch;

    void Start(){
        targetPosition = transform.position;
        newPosition = targetPosition + Vector3.up * 200f;
    }

    void FixedUpdate(){
        if(Switch){
            transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
        }else{
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        }
        
    }

    public void MoveUI(){
        Switch = !Switch;
    }
}
