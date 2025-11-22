using UnityEngine;

public class RecoverState : IState
{
    private readonly Enemy enemy;
    public float recoverTime = 2f;
    private float timer;
    
    public RecoverState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        timer = recoverTime;
        enemy.sr.color = Color.orange;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f) enemy.StateMachine.ChangeState(enemy.IdleState);
    }

    public void FixedUpdate()
    {
        
    }
}
