using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Roll", menuName = "Player Logic/Roll")]
public class PlayerRoll : PlayerRollSO
{
    public override void DoAnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEvent(triggerType);
    }

    public override void DoEnterState()
    {
        base.DoEnterState();
        Debug.Log("Ola");
    }

    public override void DoExitState()
    {
        base.DoExitState();
    }

    public override void DoFrameUpdate()
    {
        base.DoFrameUpdate();
        Debug.Log("Ola");
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
