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
        /*if(inDialogue){
            if(Input.anyKey){
                StartCoroutine(endDialogue());
            }
        }*/
    }

    public IEnumerator startDialogue(string val){
        if(!inDialogue){
            txtBox.SetActive(true);
            anim.Play("Enter");
            yield return new WaitForSecondsRealtime(0.5f);
            inDialogue = true;
        }

        for (int i = 0; i < (val.Length + 1); i++)
        {
            txt.text = val.Substring(0,i);
            yield return new WaitForSecondsRealtime(0.02f);
        }
;
        
    }

    IEnumerator endDialogue(){
        inDialogue = false;
        anim.Play("Leave");
        yield return new WaitForSecondsRealtime(0.5f);
        txtBox.SetActive(false);
        txt.text = "";
        
    }
}
