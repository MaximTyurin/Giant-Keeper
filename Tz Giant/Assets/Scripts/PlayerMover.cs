using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSmoothVelosity;
    [SerializeField] private float _turnSmoothTime = 0.1f;

    private Rigidbody _rigidbody;
    private FixedJoystick _fixedJoystick;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _fixedJoystick = FindObjectOfType<FixedJoystick>();
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(_fixedJoystick.Horizontal, 0.0f, _fixedJoystick.Vertical);
        _rigidbody.MovePosition(transform.position + movement * _moveSpeed * Time.fixedDeltaTime);
        
        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelosity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

    }
}
