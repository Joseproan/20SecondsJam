using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerDamage
{
    //Health
    bool isDead { get; set; }
    bool isHit { get; set; }
    bool rollInmunity { get; set; }
    float pushForce { get; set; }
    Vector2 hitPosition { get; set; }
}
