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
    private AudioManager am;
    private Transform player;

    void Start(){
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

        Vector3 growVector = new Vector3(xGrowth,yGrowth, zGrowth);

        scaleFactor += growVector * Time.deltaTime;
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

    void OnCollisionEnter(Collision collision){
        float velocityAdjust = Mathf.Max(rb.velocity.magnitude-0.2f, 0f);
        float volume = Mathf.Clamp(velocityAdjust/(Vector3.Distance(transform.position, player.position)*4), 0f, 0.5f);
        if(collision.transform.CompareTag("Player")){
            volume = volume/3;
        }
        float pitch = Random.Range(0.3f, 1.3f);
        int randSound = Random.Range(0,2);
        if(randSound == 0){
            am.Play("hit1", volume, pitch);
        }else{
            am.Play("hit2", volume, pitch);
        }

    }
}
