using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log(stateMachine.gameObject.name + " : Entered EnemyAttackingState"); 
        Fire();
        stateMachine.ResetCannonReadyTime();
    }

    public override void Tick(float deltaTime)
    {
        if (IsPlayerInDetectionRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        else
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        
    }

    private void Fire()
    {
        Debug.Log("FIRE!");
        stateMachine.Cannon.Fire(stateMachine.RigidBody.velocity, stateMachine.gameObject.layer);
    }
}
