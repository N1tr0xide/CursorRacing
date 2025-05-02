using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputSystem_Actions _input;
    private Rigidbody2D _rb;
    private Camera _camera;
    private Vector2 _pointerPosInput;
    private float _throttleInput;
    private float _velocity;
    [SerializeField] private bool _isCollidingWall;

    [Header("Car Settings")] 
    [SerializeField] private float _acceleration = 50; 
    [SerializeField] private float _maxSpeed = 50;
    [SerializeField] private float _turningRate = 5f;
    [SerializeField] private float _driftFactor = .95f;

    public bool ReverseEnabled;
    public float Velocity => _velocity;
    public float MaxSpeed => _maxSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
        _input = new InputSystem_Actions();
        _rb = GetComponent<Rigidbody2D>();
        _input.Enable();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    public void DisableInput()
    {
        _input.Disable();
    }

    private void Update()
    {
        _velocity = _rb.linearVelocity.magnitude;
        _throttleInput = _input.Player.Touch.ReadValue<float>();
        
#if UNITY_ANDROID
        Vector2 a = _camera.WorldToScreenPoint((Vector2)transform.position);
        _pointerPosInput = _camera.ScreenToWorldPoint(a + _input.Player.Joystick.ReadValue<Vector2>() * 15);
#else
        _pointerPosInput = _camera.ScreenToWorldPoint(_input.Player.Look.ReadValue<Vector2>()); //CURSOR INPUT
#endif
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();
        KillSideVelocity();
        ApplySteering();
    }
    
    /// Applies force forwards in the direction car is facing. Applies drag if input is zero.
    private void ApplyEngineForce()
    {
        _rb.linearDamping = _throttleInput == 0 || (_isCollidingWall && _velocity > 0)
            ? Mathf.Lerp(_rb.linearDamping, 3, Time.fixedDeltaTime * 3)
            : 0;

        if (SpeedClampCheck()) return;
       
        Vector2 engineForce = transform.up * (_acceleration * _throttleInput);
        if(ReverseEnabled) _rb.AddForce(-engineForce, ForceMode2D.Force);
        else _rb.AddForce(engineForce, ForceMode2D.Force);
    }
    
    /// Return if the max speed has been reached in any direction.
    private bool SpeedClampCheck()
    {
        float forwardSpeed = Vector2.Dot(transform.up, _rb.linearVelocity);
        if(forwardSpeed > MaxSpeed && _throttleInput > 0) return true; //max speed clamp
        if(forwardSpeed < -MaxSpeed * .25f && _throttleInput > 0) return true; //max reverse speed clamp
        return _rb.linearVelocity.sqrMagnitude > MaxSpeed * MaxSpeed && _throttleInput > 0; //max any direction speed clamp
    }
    
    /// Steer the car towards the screen position of the pointer
    private void ApplySteering()
    {
        //calc direction
        Vector2 direction = (_pointerPosInput - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        float turningThreshold = Mathf.Clamp01(_velocity / 8); //min speed required to turn.
        float rotationSpeed = turningThreshold * _turningRate;
        _rb.rotation = Mathf.LerpAngle(_rb.rotation, targetRotation.eulerAngles.z, Time.fixedDeltaTime * rotationSpeed);
    }
    
    /// Recalculate speed with modified sideways velocity for smoother turning.
    private void KillSideVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rb.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rb.linearVelocity, transform.right);
        _rb.linearVelocity = forwardVelocity + rightVelocity * _driftFactor;
    }

    public float GetLateralSpeed()
    {
        return Vector2.Dot(transform.right, _rb.linearVelocity);
            //Vector3.Dot(_rb.linearVelocity, transform.right) + Vector3.Dot(_rb.linearVelocity, transform.forward);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isCollidingWall = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isCollidingWall = false;
    }
}
