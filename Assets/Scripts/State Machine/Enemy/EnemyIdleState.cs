using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log(stateMachine.gameObject.name + " : Entered EnemyIdleState"); 
    }

    public override void Tick(float deltaTime)
    {
        if (IsPlayerInDetectionRange())
        {
            Debug.Log(stateMachine.gameObject.name + " : Player in Detection Range, Switching to EnemyChasingState");
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        else
        {
            Debug.Log(stateMachine.gameObject.name + " : Player NOT in Detection Range, Switching to EnemyMovingToTargetState");
            stateMachine.SwitchState(new EnemyMovingToTargetState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
