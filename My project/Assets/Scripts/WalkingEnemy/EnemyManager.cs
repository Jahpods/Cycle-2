using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyBaseState currentState;
    public EnemyWanderState wanderState = new EnemyWanderState();
    public EnemyPursuitState pursuitState = new EnemyPursuitState();
    public EnemyDeadState deadState = new EnemyDeadState();

    public float moveRange;
    public float posRange;
    public float viewRange;

    [HideInInspector]
    public Vector3 randomPosition;
    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        currentState = wanderState;
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentState.EnterState(this);   
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
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
}
