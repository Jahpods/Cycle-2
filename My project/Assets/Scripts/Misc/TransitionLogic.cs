using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLogic : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    
    private AudioManager am;

    public static float OriginalGlobalVolume;

    void Start(){
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        if(OriginalGlobalVolume != 0){
            am.SetGlobalVolume(OriginalGlobalVolume);
        }
    }

    public void LoadScne(string scene){
        StartCoroutine(LoadNewScene(scene));
        StartCoroutine(DullSounds());
    }

    private IEnumerator LoadNewScene(string scene){
        anim.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene(scene);
    }

    IEnumerator DullSounds(){
        OriginalGlobalVolume = am.GetGlobalVolume();
        am.SetGlobalVolume(OriginalGlobalVolume * 0.5f);
        yield return new WaitForSecondsRealtime(0.5f);
        am.SetGlobalVolume(OriginalGlobalVolume * 0.25f);
    }
}
