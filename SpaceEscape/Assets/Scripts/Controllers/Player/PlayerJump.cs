using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private float _jumpForce = 300f;
    [SerializeField]
    GroundCheck groundCheck;
    [SerializeField]
    private bool _shouldJump;
    [SerializeField]
    private FirstPersonMovement _player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InputEventHandler.instance.onSpaceButtonPressed += SpaceButtonPressed;
        _player = PlayerReference.instance.GetComponent<FirstPersonMovement>();
    }

    private void SpaceButtonPressed()
    {
        if (!_player.IsInteracting)
        {
            _shouldJump = groundCheck._isGrounded;
        }   
    }

    private void FixedUpdate()
    {
        if (_shouldJump)
        {
            rb.AddForce(transform.up * _jumpForce);
            _shouldJump = false;
        }

    }

    private void OnDestroy()
    {
        InputEventHandler.instance.onSpaceButtonPressed -= SpaceButtonPressed;
    }

    
}
