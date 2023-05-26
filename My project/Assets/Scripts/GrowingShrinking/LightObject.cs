using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour, IGrowable, IPickUp
{
    public LayerMask mask;
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
    private float boundary = 3.2f;
    private AudioManager am;
    private Transform player;

    void Start(){
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();

        scaleFactor = transform.localScale;
        minScale = new Vector3(0.2f,0.2f,0.2f);
    }

    void Update(){
        rb.useGravity = false;
        rb.mass = scaleFactor.x + scaleFactor.y + scaleFactor.z / 3;
        transform.localScale = scaleFactor;
    }

    void FixedUpdate(){
        rb.AddForce(Vector3.down * (rb.mass * 50 - 200));
    }

    public void Grow(){
        float yGrowth = Mathf.Clamp(transform.localScale.y, 1.0f, 5.0f);
        float xGrowth = Mathf.Clamp(transform.localScale.x, 1.0f, 5.0f);
        float zGrowth = Mathf.Clamp(transform.localScale.z, 1.0f, 5.0f);

        if( CheckBlocked(new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), transform.up, scaleFactor.y/boundary) ||
            CheckBlocked(new Vector3(transform.localScale.x/5, transform.localScale.y/2,transform.localScale.z/2), transform.right, scaleFactor.x/boundary) || 
            CheckBlocked(new Vector3(transform.localScale.x/2, transform.localScale.y/2,transform.localScale.z/5), transform.forward, scaleFactor.z/boundary)){
            Debug.Log("Blocked");
            yGrowth = 0;
            xGrowth = 0;
            zGrowth = 0;
        }

        scaleFactor += new Vector3(xGrowth,yGrowth, zGrowth) * Time.deltaTime;
    }

    private bool CheckBlocked(Vector3 size,Vector3 direction, float distance){
        return (Physics.BoxCast(transform.position, size, direction, transform.rotation,distance, mask) &&
                Physics.BoxCast(transform.position, size, -direction, transform.rotation,distance, mask));
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
            am.Play("plush1", volume, pitch);
        }else{
            am.Play("plush2", volume, pitch);
        }

    }
}
