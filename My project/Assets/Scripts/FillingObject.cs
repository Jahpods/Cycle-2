using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingObject : MonoBehaviour, IGrowable
{
    private Vector3 minScale;
    private Vector3 sF;
    public Vector3 scaleFactor {
        get {return sF;}
        set {sF = value;}
    }

    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();

        scaleFactor = transform.localScale;
        minScale = scaleFactor/2f;
    }

    void Update(){
        rb.mass = scaleFactor.x + scaleFactor.y + scaleFactor.z / 3;
        transform.localScale = scaleFactor;
    }

    public void Grow(){       
        float yGrowth = Mathf.Clamp(transform.localScale.y, 1.0f, Mathf.Infinity);
        if(Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), 
                           transform.up, transform.rotation,
                           scaleFactor.y/3.2f) &&
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), 
                           -transform.up, transform.rotation,
                           scaleFactor.y/3.2f)){
            yGrowth = 0;
        }

        float xGrowth = Mathf.Clamp(transform.localScale.x, 1.0f, Mathf.Infinity);
        if(Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/5, transform.localScale.y/2,transform.localScale.z/2), 
                           transform.right, transform.rotation,
                           scaleFactor.x/3.2f) && 
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/5, transform.localScale.y/2,transform.localScale.z/2), 
                           -transform.right, transform.rotation,
                           scaleFactor.x/3.2f)){
            xGrowth = 0;
        }

        float zGrowth = Mathf.Clamp(transform.localScale.z, 1.0f, Mathf.Infinity);
        if(Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/2,transform.localScale.z/5), 
                           transform.forward, transform.rotation,
                           scaleFactor.z/3.2f) && 
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/2,transform.localScale.z/5), 
                           -transform.forward, transform.rotation,
                           scaleFactor.z/3.2f)){
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
