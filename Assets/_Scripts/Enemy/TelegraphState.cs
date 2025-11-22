using UnityEngine;

public class TelegraphState : IState
{
    public float telegraphTime = 0.8f;
    
    private readonly Enemy enemy;
    private Vector2 storedPlayerPos;
    private float timer;
    
    public TelegraphState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        timer = telegraphTime;
        storedPlayerPos = enemy.player.position;
        enemy.sr.color = Color.yellow;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        enemy.RotateTowards(storedPlayerPos);

        timer -= Time.deltaTime;
        if (timer <= 0f)
            enemy.StateMachine.ChangeState(enemy.AttackState);
    }

    public void FixedUpdate()
    {
        
    }
}
