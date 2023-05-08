using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public override void EnterState(EnemyManager enemy){
        Debug.Log("Enemy Killed");
        GameObject.Destroy(enemy.gameObject);
    }

    public override void UpdateState(EnemyManager enemy){

    }

    public override void OnCollisionEnter(EnemyManager enemy){

    }
}