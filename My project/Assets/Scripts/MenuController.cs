using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private bool IsPaused;

    [SerializeField]
    private GameObject menuScreen;
    [SerializeField]
    private GunComponent[] components;

    private bool wasFalse;

    void Start(){
        UnPause();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)){
            IsPaused = !IsPaused;
            if(IsPaused){
                StartPause();
            }else{
                UnPause();
            }
        }
    }

    void StartPause(){
        foreach(GunComponent c in components){
            wasFalse = c.enabled == false;
            c.enabled = false;
        }
        menuScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void UnPause(){
        foreach(GunComponent c in components){
            if(!wasFalse){
               c.enabled = true; 
            }  
        }
        menuScreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
