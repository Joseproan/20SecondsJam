using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IComponents, IInputs, IPlayerDamage
{
    #region Components
    public PlayerHealth playerHealth { get; set; }
    public Rigidbody2D rb {  get; set; }
    public Animator anim {  get; set; }
    public PlayerInputHandler inputHandler { get; set; }
    #endregion

    #region Checks

    #region Input Checks
    public bool dashing { get; set; }

    public Vector2 moveInput { get; set; }
    #endregion

    #region Health Checks
    public bool isDead { get; set; }
    public bool isHit { get; set; }
    public float pushForce { get; set; }
    public Vector2 hitPosition { get; set; }
    public bool rollInmunity { get; set; }
    #endregion

    #endregion

    #region State Machine Variables
    public PlayerStateMachine stateMachine { get; set; }
    public PlayerIdleState idleState { get; set; }
    public PlayerRunState runState { get; set; }
    public PlayerRollState rollState { get; set; }

    #endregion

    [Header("Stats")]
    public float speed;
    public float rollForce;
    public Vector2 lastMoveInput { get; set; }

    [Range(0f, 1f)] public float rollInmunityAnimation = 0.60f;
    
    private void Awake()
    {
        //Components
        playerHealth = GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this,stateMachine);
        runState = new PlayerRunState(this,stateMachine);
        rollState = new PlayerRollState(this,stateMachine);

    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        GetInputs();

        stateMachine.CurrentPlayerState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentPlayerState.PhysicsUpdate();
    }
    public void GetInputs()
    {
        moveInput = inputHandler.GetMoveInput();
        dashing = inputHandler.GetRollInput(dashing);

    }

    public void PlayerMovement()
    {
        rb.AddForce(moveInput * speed);
    }
    

    #region Animation Triggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        stateMachine.CurrentPlayerState.AnimationTriggerEvent(triggerType);
    }
    public enum AnimationTriggerType
    {
        footstep
    }
    #endregion
    

}
