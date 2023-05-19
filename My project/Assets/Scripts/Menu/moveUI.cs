using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUI : MonoBehaviour
{
    [SerializeField]
    Vector3 targetPosition;
    [SerializeField]
    Vector3 newPosition;
    [SerializeField]
    bool Switch;

    void Start(){
        Switch = false;
        targetPosition = transform.position;
        newPosition = targetPosition + Vector3.up * Screen.height * 0.15f;
    }

    void Update(){
        Debug.Log("moving");
        if(Switch){
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.unscaledDeltaTime * 2);
        }else{
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.unscaledDeltaTime * 2);
        }
        
    }

    public void MoveUI(){
        Switch = !Switch;
    }
}
