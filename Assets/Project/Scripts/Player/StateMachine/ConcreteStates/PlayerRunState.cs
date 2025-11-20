using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerState
{
    private GameObject _footstep;
    public PlayerRunState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

    }

    public override void EnterState()
    {
        base.EnterState();
        if (player.moveInput.magnitude <= 0) player.stateMachine.ChangeState(player.idleState);

        //if(player.isHit) player.stateMachine.ChangeState(player.hitState);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.rb.linearVelocity = Vector2.zero;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();


        //Animations();

        if (player.moveInput.magnitude < -0.1f || (player.moveInput.x == 0 && player.moveInput.y == 0))
        {
            player.stateMachine.ChangeState(player.idleState);
        }
        //else if (player.rolling && !player.playerHealth.isSlowDown) player.stateMachine.ChangeState(player.rollState);
    }

    private void RotateTowardsMovement()
    {
       
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.rb.linearVelocity = player.moveInput * player.speed;
        //player.rb.AddForce(player.moveInput * player.speed,ForceMode2D.Force);
    }

    public void Animations()
    {
        player.anim.SetFloat("MoveX", player.moveInput.x);
        player.anim.SetFloat("MoveY", player.moveInput.y);
        player.anim.SetFloat("MoveMagnitude", player.moveInput.magnitude);

        if (player.moveInput.magnitude > 0.1)
        {
            player.lastMoveInput = player.moveInput;
            player.anim.SetFloat("LastMoveX", player.lastMoveInput.x);
            player.anim.SetFloat("LastMoveY", player.lastMoveInput.y);
        }
    }
}
