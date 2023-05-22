using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectInitiator : MonoBehaviour
{
    [SerializeField]
    private GameObject go;
    private bool completed;
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerEnter(Collider other){
        if(!completed){
            if(other.gameObject.CompareTag("Player")){                
                completed = true;
                go.SetActive(true);
            }
        }
    }
}
