using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        //player.anim.SetFloat("LastMoveX", player.lastMoveInput.x);
        //player.anim.SetFloat("LastMoveY", player.lastMoveInput.y);
        //player.anim.SetTrigger("Idle");
        //player.anim.SetFloat("MoveMagnitude", -0.1f);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        //if (player.moveInput.x == 0 && player.moveInput.y == 0) lastMoveDirection = player.moveInput;
        if(player.moveInput.magnitude > 0.1f) player.stateMachine.ChangeState(player.runState);

        if (player.moveInput.magnitude > 0.1)
        {
            player.lastMoveInput = player.moveInput;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
