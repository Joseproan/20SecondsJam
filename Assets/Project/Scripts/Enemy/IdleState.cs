using UnityEngine;

public class IdleState : IState
{
    private readonly Enemy enemy;

    public IdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        enemy.sr.color = Color.white;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (enemy.PlayerInSightRange())
        {
            enemy.StateMachine.ChangeState(enemy.StandbyState);
        }
    }

    public void FixedUpdate()
    {
        
    }
    
}
