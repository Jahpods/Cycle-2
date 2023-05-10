using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingObject : MonoBehaviour, IGrowable, IPickUp
{
    private Vector3 minScale;
    private Vector3 sF;
    public Vector3 scaleFactor {
        get {return sF;}
        set {sF = value;}
    }
    [HideInInspector]
    public bool IsHeld{
        get;
        set;
    }

    private Rigidbody rb;
    private float boundary = 3.13f;

    void Start(){
        rb = GetComponent<Rigidbody>();

        scaleFactor = transform.localScale;
        minScale = scaleFactor/2f;
    }

    void Update(){
        rb.mass = scaleFactor.x + scaleFactor.y + scaleFactor.z / 3;
        transform.localScale = scaleFactor;

        if(Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), 
                           transform.up, transform.rotation,
                           scaleFactor.y / boundary) &&
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), 
                           -transform.up, transform.rotation,
                           scaleFactor.y / boundary)){
            Debug.Log("Blocked");
        }
    }

    public void Grow(){       
        float yGrowth = Mathf.Clamp(transform.localScale.y, 0.1f, 5.0f);
        if(Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), 
                           transform.up, transform.rotation,
                           scaleFactor.y / boundary) &&
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), 
                           -transform.up, transform.rotation,
                           scaleFactor.y / boundary)){
            yGrowth = 0;
        }

        float xGrowth = Mathf.Clamp(transform.localScale.x, 0.1f, 5.0f);
        if(Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/5, transform.localScale.y/2,transform.localScale.z/2), 
                           transform.right, transform.rotation,
                           scaleFactor.x / boundary) && 
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/5, transform.localScale.y/2,transform.localScale.z/2), 
                           -transform.right, transform.rotation,
                           scaleFactor.x / boundary)){
            xGrowth = 0;
        }

        float zGrowth = Mathf.Clamp(transform.localScale.z, 0.1f, 5.0f);
        if(Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/2,transform.localScale.z/5), 
                           transform.forward, transform.rotation,
                           scaleFactor.z / boundary) && 
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/2,transform.localScale.z/5), 
                           -transform.forward, transform.rotation,
                           scaleFactor.z / boundary)){
            zGrowth = 0;
        }





        scaleFactor += new Vector3(xGrowth,yGrowth, zGrowth) * Time.deltaTime;
    }
    public void Shrink(){
        scaleFactor -= transform.localScale * Time.deltaTime;
        if(scaleFactor.x < minScale.x){
            sF.x = minScale.x;
        }
        if(scaleFactor.y < minScale.y){
            sF.y = minScale.y;
        }
        if(scaleFactor.z < minScale.z){
            sF.z = minScale.z;
        }
    }
}
