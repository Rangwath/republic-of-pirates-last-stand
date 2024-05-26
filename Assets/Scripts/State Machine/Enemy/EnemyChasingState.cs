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

        RotateCannonTowardsPlayer();

        if (IsPlayerInCannonRange() && Time.time >= stateMachine.CannonReadyTime)
        {
            Debug.Log(stateMachine.gameObject.name + " : Player in Cannon Range and Ready to Fire, Switching to EnemyAttackingState");
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
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

    private void RotateCannonTowardsPlayer()
    {
        // Calculate the direction to the player in world space
        Vector2 directionToPlayer = (stateMachine.PlayerTransform.position - stateMachine.transform.position).normalized;

        // Convert the direction to the local space of the ship
        Vector2 localDirection = stateMachine.transform.InverseTransformDirection(directionToPlayer);

        // Calculate the target rotation based on the local direction
        float angle = Mathf.Atan2(localDirection.y, localDirection.x) * Mathf.Rad2Deg - 90f;
        stateMachine.Cannon.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
