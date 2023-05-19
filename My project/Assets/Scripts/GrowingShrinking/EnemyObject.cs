using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyObject : MonoBehaviour, IGrowable, IPickUp
{
    [SerializeField]
    private GameObject parts;
    [SerializeField]
    private Vector3 sF;
    [HideInInspector]
    public bool IsHeld{
        get;
        set;
    }

    public Vector3 scaleFactor {
        get {return sF;}
        set {sF = value;}
    }

    private Rigidbody rb;
    [SerializeField]
    private Vector3 enemyPosition;
    private bool shrinking;
    private int rando;
    private Transform playerTf;

    private AudioManager am;
    private float startLaughCooldown = 0.6f;
    private float laughCooldown;

    [SerializeField]
    private GameObject deathMessage;

    void Start(){
        enemyPosition = transform.position;
        am = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        scaleFactor = transform.localScale;
        playerTf = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        rb.mass = scaleFactor.x + scaleFactor.y + scaleFactor.z / 3f;
        transform.localScale = scaleFactor;

        rb.useGravity = false;
        
        transform.LookAt(playerTf);
    }

    void FixedUpdate(){
        if(!IsHeld && !shrinking){
            Vector3 direction = enemyPosition - transform.position;
            if(Vector3.Dot(direction, rb.velocity) < 0.1f){
                rb.velocity -= rb.velocity * 0.3f;
            }else{
                rb.velocity = Vector3.zero;
            }
            transform.position = Vector3.Lerp(transform.position, enemyPosition, 0.05f);
        }
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
        /*if(!IsHeld){
            Vector3 moveDir = Vector3.Cross(Vector3.up, GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
            moveDir = ((rando == 1) ? -1 : 1) * moveDir;
            rb.MovePosition(transform.position + moveDir * Time.deltaTime * 40);
        }else{
            scaleFactor -= transform.localScale * Time.deltaTime;
            if(scaleFactor.x < 0.1f || scaleFactor.y < 0.1f || scaleFactor.z < 0.1f){
                Instantiate(parts, transform.position, Quaternion.identity);
                GameObject.FindGameObjectWithTag("Volume").GetComponent<EditLook>().UpdateLook();
                GameObject.FindGameObjectWithTag("Time").GetComponent<TimerLogic>().EnemyDies();
                Destroy(gameObject);
            }
        }*/
        if(laughCooldown > 0){
            laughCooldown -= Time.deltaTime;
        }

        if(!IsHeld){
            if(laughCooldown <= 0){
                RandomSound();
                laughCooldown = startLaughCooldown + Random.Range(0, 1f);
            }
            return;
        }

        scaleFactor -= transform.localScale * Time.deltaTime;
        if(scaleFactor.x < 0.1f || scaleFactor.y < 0.1f || scaleFactor.z < 0.1f){
            Instantiate(parts, transform.position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("Volume").GetComponent<EditLook>().UpdateLook();
            GameObject.FindGameObjectWithTag("Time").GetComponent<TimerLogic>().EnemyDies();
            Instantiate(deathMessage, transform.position, Quaternion.identity);
            am.Play("dies");
            Destroy(gameObject);
        }

    }

    void RandomSound(){
        float pitch = Random.Range(0.8f, 1.5f);
        int randSound = Random.Range(0,4);
        if(randSound == 0){
            am.Play("laugh1", 0.3f, pitch);
        }else if(randSound == 1){
            am.Play("laugh2", 0.3f, pitch);
        }else if(randSound == 2){
            am.Play("laugh3", 0.3f, pitch);
        }else if(randSound == 3){
            am.Play("laugh4", 0.3f, pitch);
        }
    }
}
