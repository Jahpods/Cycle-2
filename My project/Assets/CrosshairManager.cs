using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    public enum CrosshairState {Nothing, Something, Holding, SomethingBig};
    public CrosshairState cs;

    [SerializeField]
    private Image crosshair;
    [SerializeField]
    private Color[] colours;

    void FixedUpdate(){
        if(cs == CrosshairState.Nothing){
            crosshair.color = Color.Lerp(crosshair.color, colours[0], 0.2f);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.1f, 0.2f);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0,0,45), 0.2f);
        }else if(cs == CrosshairState.Something){
            crosshair.color = Color.Lerp(crosshair.color, colours[1], 0.2f);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.12f, 0.2f);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, 0.2f);
        }else if(cs == CrosshairState.Holding){
            crosshair.color = Color.Lerp(crosshair.color, colours[2], 0.2f);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.08f, 0.2f);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, 0.2f);
        }else{
            crosshair.color = Color.Lerp(crosshair.color, colours[3], 0.2f);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.12f, 0.2f);
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.zero, 0.2f);
        }
    }

    public void ChangeCursor(CrosshairState state){
        cs = state;
    }
}
