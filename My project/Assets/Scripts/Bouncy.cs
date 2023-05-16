using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour
{
    void OnCollisionStay(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();

            /*if(Vector3.Dot(rb.velocity, transform.position - collision.transform.position) > 0){
                rb.velocity = -rb.velocity;  
            }*/
            bool Above = Vector3.Dot(Vector3.up, collision.transform.position - transform.position) > 0;
            Debug.Log(Above);
            if(Above){
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.velocity += Vector3.up * 35;   
            }    
        }
    }
}
