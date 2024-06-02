using UnityEngine;

public class EnemyMovingToTargetState : EnemyBaseState
{
    public EnemyMovingToTargetState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log(stateMachine.gameObject.name + " : Entered EnemyMovingToTargetState"); 
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Target == null)
        {
            // If the target was destroyed, switch to Winning
            stateMachine.SwitchState(new EnemyWinningState(stateMachine));
            return;
        }
        
        stateMachine.AI.destination = stateMachine.Target.position;

        RotateCannonTowardsTarget(stateMachine.Target);

        if (IsPlayerInDetectionRange())
        {
            Debug.Log(stateMachine.gameObject.name + " : Player in Detection Range, Switching to EnemyChasingState");
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }

        if (IsTargetInCannonRange() && Time.time >= stateMachine.CannonReadyTime)
        {
            Debug.Log(stateMachine.gameObject.name + " : Target in Cannon Range and Ready to Fire, Switching to EnemyFiringState");
            stateMachine.SwitchState(new EnemyFiringState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        
    }
}
