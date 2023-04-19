using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGun : MonoBehaviour
{
    [SerializeField]
    private GameObject Gun;
    [SerializeField]
    private GunComponent gc;

    void Start(){
        Gun.SetActive(false);
        gc.enabled = false;
    }

    void Update(){
        transform.position += Vector3.up * Mathf.Sin(Time.time)/5 * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            Gun.SetActive(true);
            gc.enabled = true;
            gameObject.SetActive(false);
        }
    }


}
