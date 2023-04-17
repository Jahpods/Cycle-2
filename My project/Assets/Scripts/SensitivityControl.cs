using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityControl : MonoBehaviour
{
    private Slider sld;

    private PlayerMovement pm;

    void Start(){
        sld = GetComponent<Slider>();
        if(GameObject.FindGameObjectWithTag("Player") != null){
            pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            sld.value = pm.GetSens();
        }
    }

    void Update(){
        pm.SetSens(sld.value);
    }
}
