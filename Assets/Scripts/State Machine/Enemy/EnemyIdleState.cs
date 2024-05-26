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
    }

    public override void Exit()
    {
        
    }
}
