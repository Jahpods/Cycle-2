using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadScene(string scene){
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().Play("click");
        SceneManager.LoadScene(scene);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
