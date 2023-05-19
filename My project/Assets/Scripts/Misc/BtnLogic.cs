using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLogic : MonoBehaviour
{
    [SerializeField]
    private Transform btn;
    [SerializeField]
    private Transform door;

    [SerializeField]
    private float speed;

    private bool btnPressed;
    private Vector3 doorPos;
    private AudioManager am;
    void OnTriggerStay(Collider other){
        if(btnPressed == false){
            am.Play("button");
        }
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("GrowingObject")){
            btnPressed = true;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("GrowingObject")){
            btnPressed = false;
        }
    }

    void Start(){
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        doorPos = door.localPosition;
    }


    void Update(){
        if(btnPressed){
            door.localPosition = Vector3.MoveTowards(door.localPosition, doorPos + Vector3.down * door.localScale.y, Time.deltaTime * speed);
            btn.localPosition = Vector3.MoveTowards(btn.localPosition, Vector3.up * 0.4f, Time.deltaTime * speed);
        }else{
            btn.localPosition = Vector3.MoveTowards(btn.localPosition, Vector3.up * 0.8f, Time.deltaTime);
            door.localPosition = Vector3.MoveTowards(door.localPosition, doorPos, Time.deltaTime * speed);
        }
    }
}
