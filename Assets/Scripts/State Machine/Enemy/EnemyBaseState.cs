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
}
