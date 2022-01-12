using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private float _distance = 8.0f;
    [SerializeField] private float _height = 5.0f;
    [SerializeField] private float _heightDamping = 2.0f;
    [SerializeField] private float _rotationDamping = 3.0f;
    [SerializeField] private Transform _target;
    
     private bool _isAcceleration = false;
    
    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!_target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = _target.eulerAngles.y;
        float wantedHeight = _target.position.y + _height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = _target.position - currentRotation * Vector3.forward * _distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(_target);
    }

    private void Update()
    {
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
        if (_isAcceleration)
        {
            _distance = 4f;
            _height = 2.5f;
        }
        else
        {
            _distance = 8f;
            _height = 5f;
        }
    }
}