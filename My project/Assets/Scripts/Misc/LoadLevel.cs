using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public void LoadScene(string scene){
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("click");
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TransitionLogic>().LoadScne(scene);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
