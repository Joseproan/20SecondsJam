using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

    }

    public override void EnterState()
    {
        base.EnterState();
        //player.anim.SetBool("Roll", true);
        player.rollInmunity = true;

    }

    public override void ExitState()
    {
        base.ExitState();
        player.anim.SetBool("Roll", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (player.isHit)
        {
            player.isHit = false;
            player.rb.linearVelocity = Vector2.zero;
            player.stateMachine.ChangeState(player.idleState);
        }
        /*if (player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > player.rollInmunityAnimation && player.anim.GetCurrentAnimatorStateInfo(0).IsTag("Roll"))
        {
            player.rollInmunity = false;
        }*/
        
        if (player.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f && player.anim.GetCurrentAnimatorStateInfo(0).IsTag("Roll"))
        {
            player.dashing = false;
            if (player.moveInput.magnitude <= 0.1f || (player.moveInput.x == 0 && player.moveInput.y == 0))
            {
                player.stateMachine.ChangeState(player.idleState);

            }
            else if (player.moveInput.magnitude > 0.1f) player.stateMachine.ChangeState(player.runState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector2 force = player.lastMoveInput.normalized * player.rollForce;

        player.rb.AddForce(force, ForceMode2D.Impulse);
    }
}
