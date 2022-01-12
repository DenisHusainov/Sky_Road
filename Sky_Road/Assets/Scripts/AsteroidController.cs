using UnityEngine;
using System;
using System.Collections;

public class AsteroidController : MonoBehaviour
{
    public static event Action AsteroidPassed = delegate { };

    [SerializeField] private GameObject _asteroid;
    private float _currentPlaeyrPossitionZ;
    private bool _isAsteroidPassed;

    private int _asteroidDestroyDelay = 12;
    private Coroutine DiactiveAsteroidCor;


    private void OnEnable()
    {
        _isAsteroidPassed = false;
        MovementController.OnPlayerPossitionChanged += MovementController_PlayerPossitionChanched;
        MovementController.Died += MovementController_Died;
        DiactiveAsteroidCor = StartCoroutine(DiactiveAsteroid());
    }

    private void OnDisable()
    {
        MovementController.OnPlayerPossitionChanged -= MovementController_PlayerPossitionChanched;
        MovementController.Died -= MovementController_Died;
    }

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.AngleAxis(3, Vector3.up);
        transform.rotation *= rotation;
    }

    private void Update()
    {
        OnAsteroidPassed();
    }

    IEnumerator DiactiveAsteroid()
    {
        yield return new WaitForSeconds(_asteroidDestroyDelay);

        gameObject.SetActive(false);
    }

    private void OnAsteroidPassed()
    {
        if (_asteroid.transform.position.z < _currentPlaeyrPossitionZ && !_isAsteroidPassed)
        {
            _isAsteroidPassed = true;
            AsteroidPassed();
        }
    }

    private void MovementController_PlayerPossitionChanched(float _playerTransformPossitionZ)
    {
        _currentPlaeyrPossitionZ = _playerTransformPossitionZ;
    }

    private void MovementController_Died()
    {
        StopCoroutine(DiactiveAsteroidCor);
    }
}
