using UnityEngine;

public class StandbyState : IState
{
    private readonly Enemy enemy;

    public StandbyState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        enemy.sr.color = Color.grey;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (enemy.CanSeePlayer()) enemy.RotateTowardsPlayer();
        
        if (!enemy.PlayerInSightRange())
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
        
        if (enemy.PlayerInAttackRange())
        {
            enemy.StateMachine.ChangeState(enemy.TelegraphState);
        }
    }

    public void FixedUpdate()
    {
        
    }
}
