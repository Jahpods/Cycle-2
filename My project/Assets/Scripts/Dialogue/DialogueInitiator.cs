using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogueInitiator : MonoBehaviour
{
    [SerializeField]
    private string message;

    private DialogueManager dm;
    private bool completed;
    // Start is called before the first frame update
    void Start()
    {
     dm = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            if(!completed){
                completed = true;
                StartCoroutine(dm.startDialogue(message));
            }
        }
    }
}
