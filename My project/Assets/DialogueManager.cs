using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txt;
    [SerializeField]
    private GameObject txtBox;
    [SerializeField]
    private Animator anim;


    private bool inDialogue;

    // Update is called once per frame
    void Update()
    {
        if(inDialogue){
            if(Input.anyKey){
                StartCoroutine(endDialogue());
            }
        }
    }

    public IEnumerator startDialogue(string val){
        Time.timeScale = 0;
        txtBox.SetActive(true);
        anim.Play("Enter");
        yield return new WaitForSecondsRealtime(0.5f);
        txt.text = val;
        yield return new WaitForSecondsRealtime(0.5f);
        inDialogue = true;
    }

    IEnumerator endDialogue(){
        inDialogue = false;
        anim.Play("Leave");
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(0.5f);
        txtBox.SetActive(false);
        txt.text = "";
        
    }
}
