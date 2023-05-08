using UnityEngine;

public class EnemyPursuitState : EnemyBaseState
{
    public override void EnterState(EnemyManager enemy){
        Debug.Log("Pursuing");
    }

    public override void UpdateState(EnemyManager enemy){
        if(Vector3.Distance(enemy.player.position, enemy.transform.position) > 0.5f){
            Vector3 targetPos = new Vector3(enemy.player.position.x, enemy.transform.position.y, enemy.player.position.z);
            enemy.transform.forward = (targetPos - enemy.transform.position).normalized;
            enemy.transform.position += (targetPos - enemy.transform.position).normalized * Time.deltaTime;
        }

        if(Vector3.Distance(enemy.transform.position, enemy.player.position) > enemy.viewRange){
            enemy.SwitchState(enemy.wanderState);
        }
    }

    public override void OnCollisionEnter(EnemyManager enemy){
        if((enemy.player.position - enemy.transform.position).normalized.y > 0.8){
            enemy.SwitchState(enemy.deadState);
        }else{
            Debug.Log("Took Damage");
        }
    }
}

