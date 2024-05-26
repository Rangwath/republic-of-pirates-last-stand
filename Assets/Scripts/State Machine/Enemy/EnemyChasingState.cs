using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log(stateMachine.gameObject.name + " : Entered EnemyChasingState"); 
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.AI.destination = stateMachine.PlayerTransform.position;

        RotateCannonTowardsTarget(stateMachine.PlayerTransform);

        if (IsPlayerInCannonRange() && Time.time >= stateMachine.CannonReadyTime)
        {
            Debug.Log(stateMachine.gameObject.name + " : Player in Cannon Range and Ready to Fire, Switching to EnemyFiringState");
            stateMachine.SwitchState(new EnemyFiringState(stateMachine));
            return;
        }

        if (!IsPlayerInDetectionRange())
        {
            Debug.Log(stateMachine.gameObject.name + " : Player NOT in Detection Range, Switching to EnemyIdleState");
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
