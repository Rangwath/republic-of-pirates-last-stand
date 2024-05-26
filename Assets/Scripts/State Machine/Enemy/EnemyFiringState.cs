using UnityEngine;

public class EnemyFiringState : EnemyBaseState
{
    public EnemyFiringState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log(stateMachine.gameObject.name + " : Entered EnemyFiringState"); 
        Fire();
        stateMachine.ResetCannonReadyTime();
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.SwitchState(new EnemyIdleState(stateMachine));
    }

    public override void Exit()
    {
        
    }

    private void Fire()
    {
        stateMachine.Cannon.Fire(stateMachine.RigidBody.velocity, stateMachine.gameObject.layer);
    }
}
