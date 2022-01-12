using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static event Action<float> OnPlayerPossitionChanged = delegate { };
    public static event Action Died = delegate { };
    public static event Action AccelerationStarted = delegate { };
    public static event Action AccelerationFinished = delegate { };

    private const int AccelarationSpeed = 200;
    private const int DefaultSpeed = 100;

    [SerializeField] private float _turnSpeed;

    private float _speed;
    private float _xPosition;
    private Rigidbody _rigidbody;
    private Vector3 _rotation;
    private bool _isAcceleration = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!CanMove())
        {
            return;
        }

        _xPosition = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        {
            _isAcceleration = true;
        }
        else
        {
            _isAcceleration = false;
        } 
    }

    private void FixedUpdate()
    {
        if (!CanMove())
        {
            return;
        }

        _rotation = new Vector3(0, 0, _xPosition);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.8f, 2.8f), transform.position.y, transform.position.z);
        _rigidbody.AddForce(Vector3.forward * _speed, ForceMode.Force);
        _rigidbody.MoveRotation(Quaternion.Euler(0, 0, 0));

        if (_xPosition !=0)
        {
            _rigidbody.AddForce(NewVector(_xPosition, _turnSpeed));
            Quaternion deltaRotation = Quaternion.Euler(_rotation * -1000 * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
        }

        if (_isAcceleration)
        {
            _speed = AccelarationSpeed;
            AccelerationStarted();
        }
        else
        {
            _speed = DefaultSpeed;
            AccelerationFinished();
        }

        OnPlayerPossitionChanged(transform.position.z);
    }
  
    private Vector3 NewVector(float _x, float _turnSpeed)
    {
        return new Vector3(_x * Time.deltaTime * _turnSpeed, 0,0);
    }

    private bool CanMove()
    {
        return GameManager.Instance.IsStarted && !GameManager.Instance.IsFineshed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AsteroidController>())
        {
            Died();
            gameObject.SetActive(false);
        }
    }
}
