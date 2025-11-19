using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "Player Logic/Idle")]
public class PlayerIdle : PlayerIdleSO
{
    private Vector2 lastMoveDirection;
    public float prueba;
    public override void DoAnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEvent(triggerType);
    }

    public override void DoEnterState()
    {
        base.DoEnterState();
        Debug.Log("funciona");
    }

    public override void DoExitState()
    {
        base.DoExitState();
    }

    public override void DoFrameUpdate()
    {
        base.DoFrameUpdate();
        player.GetInputs();

        if (player.moveInput.magnitude > 0.1) player.stateMachine.ChangeState(player.runState);
    }
    public override void Initialize(GameObject gameObject, Player player)
    {
        base.Initialize(gameObject, player);
    }

    public override void DoPhysicsUpdate()
    {
        base.DoPhysicsUpdate();
        Debug.Log("Ola");
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
