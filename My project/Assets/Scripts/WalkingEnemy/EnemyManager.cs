using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGrowable
{
    EnemyBaseState currentState;
    public EnemyWanderState wanderState = new EnemyWanderState();
    public EnemyPursuitState pursuitState = new EnemyPursuitState();
    public EnemyDeadState deadState = new EnemyDeadState();

    public float moveRange;
    public float posRange;
    public float viewRange;
    public float speed;
    [HideInInspector]
    public Vector3 randomPosition;
    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public Rigidbody rb;



    [SerializeField]
    private Vector3 minScale;

    public Vector3 scaleFactor {
        get;
        set;
    }
    // Start is called before the first frame update
    void Start()
    {
        scaleFactor = transform.localScale;
        minScale = transform.localScale/2;
        rb = GetComponent<Rigidbody>();
        currentState = wanderState;
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentState.EnterState(this);   
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        transform.localScale = scaleFactor;
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Player")){
            currentState.OnCollisionEnter(this);
        }
    }

    public void SwitchState(EnemyBaseState state){
        currentState = state;
        state.EnterState(this);
    }

    void OnDrawGizmos(){
        Gizmos.color = new Color(1,0,0,0.8f);
        Gizmos.DrawWireSphere(transform.position, moveRange);

        Gizmos.color = new Color(0,1,0,0.8f);
        Gizmos.DrawWireSphere(startPosition, posRange);


        Gizmos.color = new Color(0,0,1,0.8f);
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }

    public void Grow(){
        float yGrowth = Mathf.Clamp(transform.localScale.y, 1.0f, 5.0f);
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

        scaleFactor += new Vector3(xGrowth,yGrowth, zGrowth) * Time.deltaTime;
    }
    public void Shrink(){
        if(scaleFactor.x < minScale.x || scaleFactor.y < minScale.y || scaleFactor.z < minScale.z){
            return;
        }

        scaleFactor -= transform.localScale * Time.deltaTime;
    }
}
