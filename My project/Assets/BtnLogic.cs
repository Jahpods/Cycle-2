using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BtnLogic : MonoBehaviour
{
    [SerializeField]
    private Transform btn;
    [SerializeField]
    private DecalProjector dp;
    [SerializeField]
    private Transform door;
    [SerializeField]
    private Color[] colours;
    private bool btnPressed;
    private Vector3 doorPos;
    void OnTriggerEnter(Collider other){
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
        //Set The wiring so it looks right
        //Position the wire in between the door and the button
        Vector3 dist = (door.position - transform.position);
        Vector3 wirePos = transform.position + dist / 2f;
        wirePos = new Vector3(wirePos.x, 0, wirePos.z);
        dp.transform.position = wirePos;
        //Rotate the wire so it is in line
        dp.transform.forward = wirePos.normalized;
        //increase width
        dp.size = new Vector3(dp.size.x, dp.size.y, dist.magnitude);



        doorPos = door.localPosition;
    }

    void Update(){
        if(btnPressed){
            door.localPosition = doorPos + Vector3.down * door.localScale.y;
            btn.localPosition = Vector3.up * 0.4f;
            dp.material.SetColor("_Color", colours[0]);
        }else{
            door.localPosition = doorPos;
            btn.localPosition = Vector3.up * 0.8f;
            dp.material.SetColor("_Color", colours[1]);
        }
    }
}
