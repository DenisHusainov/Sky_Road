using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _point;

    private Coroutine _destroyRoadCor;

    private int _roadDestroyDelay = 20;

    private void OnEnable()
    {
        MovementController.Died += MovementController_Died;
        GameManager.Started += GameManager_Started;

        if (GameManager.Instance != null && GameManager.Instance.IsStarted)
        {
            _destroyRoadCor = StartCoroutine(DestroyRoadCor());
        }
    }

    private void OnDisable()
    {
        MovementController.Died -= MovementController_Died;
        GameManager.Started -= GameManager_Started;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MovementController>())
        {
            GameObject roadPrefab = PoolManager.Instance.GetPooledObject(_point.position, _point.rotation);
            if (roadPrefab != null)
            {
                roadPrefab.SetActive(true);
            }
        }
    }

    IEnumerator DestroyRoadCor()
    {
        yield return new WaitForSeconds(_roadDestroyDelay);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    private void MovementController_Died()
    {
        StopCoroutine(_destroyRoadCor);
    }

    private void GameManager_Started()
    {
        _destroyRoadCor = StartCoroutine(DestroyRoadCor());
    }
}
