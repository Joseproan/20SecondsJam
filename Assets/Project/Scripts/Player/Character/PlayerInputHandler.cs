using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    //CONTROLS SCRIPT
    public Controls playerControls;

    //INPUTS
    private InputAction move;
    private InputAction look;
    private InputAction dash;
    private InputAction pause;
    private InputAction infoWindow;

    //BOOLS
    private Vector2 moveInput;
    private Vector2 mousePos;
    private bool dashing,spinningOrb,spinningRoulette,setPause,infoWindowOpening;
    private float timer, rollLongTime;
    private Animator anim;

    [SerializeField] private float longRoll = 0.55f,shortRoll = 1.5f;
    private void Awake()
    {
        playerControls = new Controls();
        anim = GetComponent<Animator>();
        rollLongTime = 0.2f;
    }

    private void Update()
    {
        MoveInput();
        MousePosition();

        if (dash.IsPressed())
        {
            timer += Time.deltaTime;

            if (timer > 0.15f)
            {
                anim.speed = longRoll;
            }
            else
            {
                anim.speed = shortRoll;
            }

        }
        else
        {
            anim.speed = 1f;
            timer = 0;
        }
    }
    private void OnEnable()
    {
        // Movimiento: habilitar la acci�n de movimiento.
        move = playerControls.Player.Movement;
        move.Enable();

        look = playerControls.Player.MousePos;
        look.Enable();

        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += RollInput;

        //spinOrb = playerControls.Player.SpinOrb;
        //spinOrb.Enable();
        //spinOrb.performed += SpinOrbInput;

        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += PauseMenu;

        infoWindow = playerControls.Player.InfoWindow;
        infoWindow.Enable();
        infoWindow.performed += InfoWindowOpen;
    }

    private void InfoWindowOpen(InputAction.CallbackContext obj)
    {
        infoWindowOpening = true;
    }

    private void OnDisable()
    {
        // Movimiento: deshabilitar la acci�n de movimiento.
        move.Disable();
        look.Disable();
        dash.Disable();

        pause.Disable();
        
        infoWindow.Disable();
    }
    //MOVE INPUT
    public void MoveInput()
    {
        moveInput = move.ReadValue<Vector2>();
    }
    public void MousePosition()
    {
        mousePos = look.ReadValue<Vector2>();
    }
    //SETTERS
    private void RollInput(InputAction.CallbackContext context)
    {
        dashing = true; // Se activa solo una vez al presionar el botón
    }
    private void SpinOrbInput(InputAction.CallbackContext context)
    {
        spinningOrb = true;
    }
    private void SpinRoulette(InputAction.CallbackContext context)
    {
        spinningRoulette = true;
    }    
    private void PauseMenu(InputAction.CallbackContext context)
    {
        setPause = true;
    }
    //GETTERS
    public Vector2 GetMoveInput()
    {
        return moveInput;
    }
    public Vector2 GetMousePosition()
    {
        return mousePos;
    }
    public bool GetRollInput(bool input)
    {
        input = dashing;
        dashing = false;
        return input;
    }
    public bool GetSpinOrbInput(bool input)
    {
        input = spinningOrb;
        spinningOrb = false;
        return input;
    }
    public bool GetSpinRouletteInput(bool input)
    {
        input = spinningRoulette;
        spinningRoulette = false;
        return input;
    }
    public bool GetPause(bool input)
    {
        input = setPause;
        setPause = false;
        return input;
    }

    public bool GetInfoWindowOpening(bool input)
    {
        input = infoWindowOpening;
        infoWindowOpening = false;
        return input;
    }
}
