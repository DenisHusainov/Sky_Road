using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private const int _amountToPoolAsteroid = 30;
    private const int _amountToPoolRoad = 10;

    [SerializeField] private GameObject _objectToPoolAsteroid;
    [SerializeField] private GameObject _objectToPool;

    private List<GameObject> _poolObjects = new List<GameObject>();
    private List<GameObject> _poolObjectsAsteroid = new List<GameObject>();

    public static PoolManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        MovementController.Died += MovementController_Died;
    }

    private void OnDisable()
    {
        MovementController.Died -= MovementController_Died;
    }

    private void Start()
    {
        GameObject tmp;
        for (int i = 0; i < _amountToPoolRoad; i++)
        {
            tmp = Instantiate(_objectToPool, Vector3.zero, Quaternion.identity, transform);
            tmp.SetActive(false);
            _poolObjects.Add(tmp);
        }

        for (int i = 0; i < _amountToPoolAsteroid; i++)
        {
            tmp = Instantiate(_objectToPoolAsteroid, Vector3.zero, Quaternion.identity, transform);
            tmp.SetActive(false);
            _poolObjectsAsteroid.Add(tmp);
        }
    }

    public GameObject GetPooledObject(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < _amountToPoolRoad; i++)
        {
            if (!_poolObjects[i].activeInHierarchy)
            {
                _poolObjects[i].transform.SetPositionAndRotation(position, rotation);

                return _poolObjects[i];
            }
        }
        return null;
    }

    public GameObject GetPoolAsteroid(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < _amountToPoolAsteroid; i++)
        {
            if (!_poolObjectsAsteroid[i].activeInHierarchy)
            {
                _poolObjectsAsteroid[i].transform.SetPositionAndRotation(position, rotation);

                return _poolObjectsAsteroid[i];
            }
        }
        return null;
    }

    private void MovementController_Died()
    {
        for (int i = 0; i < _amountToPoolAsteroid; i++)
        {
            if (_poolObjectsAsteroid[i].activeInHierarchy)
            {
                _poolObjectsAsteroid[i].gameObject.SetActive(false);
            }
        }
    }
}
