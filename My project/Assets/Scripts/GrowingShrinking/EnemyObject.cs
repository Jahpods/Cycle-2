using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyObject : MonoBehaviour, IGrowable, IEnemy
{
    [SerializeField]
    private GameObject parts;
    [SerializeField]
    private Vector3 sF;
    private bool isHeld;
    [HideInInspector]
    public bool Holding{
        get {return isHeld;}
        set {isHeld = value;}
    }

    public Vector3 scaleFactor {
        get {return sF;}
        set {sF = value;}
    }

    private Rigidbody rb;
    private Vector3 enemyPosition;
    private bool shrinking;
    private int rando;
    private Transform playerTf;

    void Start(){
        enemyPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        scaleFactor = transform.localScale;
        playerTf = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        rb.mass = scaleFactor.x + scaleFactor.y + scaleFactor.z / 3f;
        transform.localScale = scaleFactor;

        rb.useGravity = false;
        if(rb.velocity.magnitude > 0){
            rb.AddForce(-rb.velocity * 0.8f);
        }

        if(!shrinking){
            rando = Random.Range(0,2);
            if(Vector3.Distance(transform.position, enemyPosition) > 1f){
                rb.MovePosition(transform.position + (enemyPosition - transform.position) * Time.deltaTime * 4);
            }
        }else{
            shrinking = false;
        }
        
        transform.LookAt(playerTf);
    }

    public void Grow(){
        /*float yGrowth = Mathf.Clamp(transform.localScale.y, 1.0f, 5.0f);
        float xGrowth = Mathf.Clamp(transform.localScale.x, 1.0f, 5.0f);
        float zGrowth = Mathf.Clamp(transform.localScale.z, 1.0f, 5.0f);
        if((Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), 
                           transform.up, transform.rotation,
                           scaleFactor.y/3.2f) &&
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/5,transform.localScale.z/2), 
                           -transform.up, transform.rotation,
                           scaleFactor.y/3.2f)) ||
            (Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/5, transform.localScale.y/2,transform.localScale.z/2), 
                           transform.right, transform.rotation,
                           scaleFactor.x/3.2f) && 
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/5, transform.localScale.y/2,transform.localScale.z/2), 
                           -transform.right, transform.rotation,
                           scaleFactor.x/3.2f)) ||
            (Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/2,transform.localScale.z/5), 
                           transform.forward, transform.rotation,
                           scaleFactor.z/3.2f) && 
            Physics.BoxCast(transform.position, 
                           new Vector3(transform.localScale.x/2, transform.localScale.y/2,transform.localScale.z/5), 
                           -transform.forward, transform.rotation,
                           scaleFactor.z/3.2f))){
            yGrowth = 0;
            xGrowth = 0;
            zGrowth = 0;
        }

        scaleFactor += new Vector3(xGrowth,yGrowth, zGrowth) * Time.deltaTime;*/
    }
    public void Shrink(){
        shrinking = true;
        if(!isHeld){
            Vector3 moveDir = Vector3.Cross(Vector3.up, GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
            moveDir = ((rando == 1) ? -1 : 1) * moveDir;
            rb.MovePosition(transform.position + moveDir * Time.deltaTime * 40);
        }else{
            scaleFactor -= transform.localScale * Time.deltaTime;
            if(scaleFactor.x < 0.1f || scaleFactor.y < 0.1f || scaleFactor.z < 0.1f){
                Instantiate(parts, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

    }
}
