using UnityEngine;

public class EnemyWinningState : EnemyBaseState
{
    public EnemyWinningState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log(stateMachine.gameObject.name + " : Entered EnemyWinningState"); 
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}
