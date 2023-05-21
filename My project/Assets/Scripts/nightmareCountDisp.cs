using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class nightmareCountDisp : MonoBehaviour
{
    private Transform player;
    private TimerLogic tim;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tim = GameObject.FindGameObjectWithTag("Time").GetComponent<TimerLogic>();
    }

    void Update(){
        transform.GetComponent<TMP_Text>().text = tim.enemies.Length.ToString() + " Left";
        transform.forward = player.position - transform.position;
    }
}
