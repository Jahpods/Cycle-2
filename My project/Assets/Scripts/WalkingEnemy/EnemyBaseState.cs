using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyManager enemy);

    public abstract void UpdateState(EnemyManager enemy);

    public abstract void OnCollisionEnter(EnemyManager enemy);
}
