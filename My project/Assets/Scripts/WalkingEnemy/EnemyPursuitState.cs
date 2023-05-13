using UnityEngine;

public class EnemyPursuitState : EnemyBaseState
{
    public override void EnterState(EnemyManager enemy){
        Debug.Log("Pursuing");
    }

    public override void UpdateState(EnemyManager enemy){
        if(Vector3.Distance(enemy.player.position, enemy.transform.position) > 0.5f){
            Vector3 targetPos = new Vector3(enemy.player.position.x, enemy.transform.position.y, enemy.player.position.z);
            Vector3 lookDir = (targetPos - enemy.transform.position).normalized;
            enemy.transform.forward = new Vector3(lookDir.x, 0, lookDir.z);
            Vector3 dir = (targetPos - enemy.transform.position).normalized * Time.deltaTime * enemy.speed;
            enemy.rb.velocity = new Vector3(dir.x, enemy.rb.velocity.y, dir.z);
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

