using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Run", menuName = "Player Logic/Run")]
public class PlayerRun : PlayerRunSO
{
    private Vector2 lastMoveDirection;
    public override void DoAnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEvent(triggerType);
    }

    public override void DoEnterState()
    {
        base.DoEnterState();
    }

    public override void DoExitState()
    {
        base.DoExitState();
    }

    public override void DoFrameUpdate()
    {
        base.DoFrameUpdate();

        if (player.moveInput.x == 0 && player.moveInput.y == 0) player.stateMachine.ChangeState(player.idleState);
        player.anim.SetFloat("LastMoveX", lastMoveDirection.x);
        player.anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    public override void Initialize(GameObject gameObject, Player player)
    {
        base.Initialize(gameObject, player);
    }

    public override void DoPhysicsUpdate()
    {
        base.DoPhysicsUpdate();
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
