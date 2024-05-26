using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsPlayerInDetectionRange()
    {
        float distanceToPlayerSqr = (stateMachine.PlayerTransform.position - stateMachine.transform.position).sqrMagnitude;

        return distanceToPlayerSqr <= stateMachine.DetectionRange * stateMachine.DetectionRange;
    }

    protected bool IsPlayerInCannonRange()
    {
        float distanceToPlayerSqr = (stateMachine.PlayerTransform.position - stateMachine.transform.position).sqrMagnitude;

        return distanceToPlayerSqr <= stateMachine.CannonRange * stateMachine.CannonRange;
    }

    protected bool IsTargetInCannonRange()
    {
        // LayerMask targetLayerMask = LayerMask.GetMask("PlayerBase"); 

        // Use OverlapCircle to check if any colliders within the range
        Collider2D targetCollider = Physics2D.OverlapCircle(stateMachine.transform.position, stateMachine.CannonRange, stateMachine.PlayerBaseLayer);

        // Check if the detected collider belongs to the target
        if (targetCollider != null && targetCollider.transform == stateMachine.Target)
        {
            return true;
        }

        return false;
    }

    protected void RotateCannonTowardsTarget(Transform target)
    {
        // Calculate the direction to the target in world space
        Vector2 direction = (target.position - stateMachine.transform.position).normalized;

        // Convert the direction to the local space of the ship
        Vector2 localDirection = stateMachine.transform.InverseTransformDirection(direction);

        // Calculate the target rotation based on the local direction
        float angle = Mathf.Atan2(localDirection.y, localDirection.x) * Mathf.Rad2Deg - 90f;
        stateMachine.Cannon.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
