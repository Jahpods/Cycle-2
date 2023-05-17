using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{
    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();

            /*if(Vector3.Dot(rb.velocity, transform.position - collision.transform.position) > 0){
                rb.velocity = -rb.velocity;  
            }*/
            bool Above = Vector3.Dot(Vector3.up, other.transform.position - transform.position) > 0;

            if(Above){
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.velocity += Vector3.up * 35;   
            }    
        }
    }
}
