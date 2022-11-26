using System.Collections;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _max_velocity = 12f;

    private Rigidbody _rb;
    public Transform _playerRotator;

    private bool _canMove = true;

    [SerializeField]
    private bool _isInteracting = false;

    Vector3 direction;

    public bool CanMove
    {
        get => _canMove;
        set => _canMove = value;
    } 
    public bool IsInteracting
    {
        get => _isInteracting;
        set => _isInteracting = value;
    } 
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        direction = transform.right * x + transform.forward * z;
    }

    private void FixedUpdate()
    {
        if (!CanMove)     
            return;
        
        if (_rb.velocity.magnitude >= _max_velocity)
            return;

        _rb.MovePosition(transform.position + (direction * Speed * Time.deltaTime));
    }
}

