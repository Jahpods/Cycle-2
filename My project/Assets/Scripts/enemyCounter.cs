using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyCounter : MonoBehaviour
{
    [SerializeField]
    private GameObject go;
    [SerializeField]
    private float speed;
    [SerializeField]
    private TMP_Text txt;

    private trainingTimer tt;
    private bool closed;
    private Vector3 doorPos;
    // Start is called before the first frame update
    void Start()
    {
        tt = go.GetComponent<trainingTimer>();
        doorPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(go.activeSelf){
            CheckState();

            MoveDoor();
        }
    }

    void CheckState(){
        txt.text = tt.enemies.Length.ToString();
        if(tt.enemies.Length > 0){
            closed = true;
        }else{
            closed = false;
        }
    }

    void MoveDoor(){
        if(!closed){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, doorPos + Vector3.down * transform.localScale.y, Time.deltaTime * speed);
        }else{
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, doorPos, Time.deltaTime * speed);
        }
    }
}
