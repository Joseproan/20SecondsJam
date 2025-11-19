using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollSO : ScriptableObject
{
    protected Player player;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Player player)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.player = player;
    }

    public virtual void DoAnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {

    }

    public virtual void DoEnterState()
    {

    }

    public virtual void DoExitState()
    {
        ResetValues();
    }

    public virtual void DoFrameUpdate()
    {

    }

    public virtual void DoPhysicsUpdate()
    {

    }

    public virtual void ResetValues() { }
}
