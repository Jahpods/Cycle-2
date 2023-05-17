using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volControl : MonoBehaviour
{
    private Slider sld;

    private AudioManager am;

    void Start(){
        sld = GetComponent<Slider>();
        if(GameObject.FindGameObjectWithTag("AudioManager") != null){
            am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            am.SetGlobalVolume(1f);
            sld.value = am.GetGlobalVolume();
        }
    }

    void Update(){
       am.SetGlobalVolume(sld.value);
    }
}
