using UnityEngine;

public class AttackState : IState
{
    private readonly Enemy enemy;
    public float attackTime = 0.5f;
    private float timer;
    
    public AttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
        timer = attackTime;
        enemy.sr.color = Color.red;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f) enemy.StateMachine.ChangeState(enemy.RecoverState);
    }

    public void FixedUpdate()
    {
        
    }
}
