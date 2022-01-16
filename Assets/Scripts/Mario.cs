using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mario : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private PlayerInput _playerInput;
    private InputMaster _inputMaster;
    private float _moveSpeed = 5f;
    private float _jumpStrength = 2f;


    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();

        _inputMaster = new InputMaster();
        _inputMaster.Player.Enable();
        _inputMaster.Player.Move.started += Move;
        _inputMaster.Player.Jump.started += Jump;



    }
    

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
            _direction = Vector2.up * _jumpStrength;
        else
        {
            _direction.y += Physics2D.gravity.y * Time.deltaTime;
        }
            
        
        Vector2 inputVector = _inputMaster.Player.Move.ReadValue<Vector2>();
        _direction.x = inputVector.x * _moveSpeed;
        _direction.y = Math.Max(_direction.y, -1f);



    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition((_rigidbody.position + _direction * Time.fixedDeltaTime));
    }

    // Used to some test with new input system
    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase + " : Je Move les boys !!!");
        
    }
    
    // Used to some test with new input system
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase + " : Je Jump les boys !!!");
    }
}
