using UnityEngine;

public class EnemyWanderState : EnemyBaseState
{
    public override void EnterState(EnemyManager enemy){
        Debug.Log("Wandering");
        PickNewPosition(enemy);
    }

    public override void UpdateState(EnemyManager enemy){
        if(Vector3.Distance(enemy.randomPosition, enemy.transform.position) > 0.5f){
            enemy.transform.forward = (enemy.randomPosition - enemy.transform.position).normalized;
            enemy.transform.position += (enemy.randomPosition - enemy.transform.position).normalized * Time.deltaTime;
        }else{
            PickNewPosition(enemy);
        }

        if(Vector3.Distance(enemy.transform.position, enemy.player.position) < enemy.viewRange){
            enemy.SwitchState(enemy.pursuitState);
        }
    }

    public override void OnCollisionEnter(EnemyManager enemy){
        if((enemy.player.position - enemy.transform.position).normalized.y > 0.8){
            enemy.SwitchState(enemy.deadState);
        }else{
            Debug.Log("Took Damage");
        }
    }

    void PickNewPosition(EnemyManager enemy){
        enemy.randomPosition = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        if(Vector3.Distance(enemy.transform.position, enemy.startPosition) > enemy.posRange){
            enemy.randomPosition = enemy.startPosition;
        }
        while(Vector3.Distance(enemy.randomPosition, enemy.startPosition) > enemy.posRange){
            Vector3 randomDirection = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f)).normalized * Random.Range(0.0f,enemy.moveRange);
            enemy.randomPosition = enemy.transform.position + randomDirection;
        }
    }
}

