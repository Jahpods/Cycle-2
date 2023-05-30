using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private bool IsPaused;

    [SerializeField]
    private GameObject menuScreen;
    [SerializeField]
    private GunComponent component;

    private bool def;

    void Start(){
        menuScreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        def = component.enabled;
        Debug.Log(def);
        component.enabled = false;
        menuScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void UnPause(){
        component.enabled = def;
        menuScreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
