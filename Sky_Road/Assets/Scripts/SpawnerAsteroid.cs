using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAsteroid : MonoBehaviour
{
    private const int SpawnFrequencyDelayAst = 10;

    [SerializeField] private List<float> _rowsCoordination;
    [SerializeField] private GameObject _player;

    private float _asteroidsSpawnDelay = 2f;

    private Coroutine _spawnAstCor;
    private Coroutine _frequencySpawnAstCor;

    private void OnEnable()
    { 
        GameManager.Started += GameManager_GameStarted;
        GameManager.Fineshed += GameManager_GameFinished;
    }

    private void OnDisable()
    {
        GameManager.Started -= GameManager_GameStarted;
        GameManager.Fineshed -= GameManager_GameFinished;
    }

    IEnumerator SpawnAst()
    {
        while (true)
        {
            yield return new WaitForSeconds(_asteroidsSpawnDelay);
            Vector3 _spawnPosition = new Vector3(Random.Range(_rowsCoordination[0], _rowsCoordination.Count - 1), 1f, _player.transform.position.z + 150f);

            GameObject astPrefab = PoolManager.Instance.GetPoolAsteroid(_spawnPosition, Quaternion.identity);
            if (astPrefab != null)
            {
                astPrefab.transform.position = _spawnPosition;
                astPrefab.SetActive(true);
            }
        }
    }
  
    IEnumerator FrequencySpawnAst()
    {
        var waiter = new WaitForSeconds(SpawnFrequencyDelayAst);

        while (true)
        {
            yield return waiter;

            _asteroidsSpawnDelay -= 0.2f;

            Mathf.Clamp(_asteroidsSpawnDelay, 1f, 4f);
        }
    }

    private void GameManager_GameStarted()
    {
        _spawnAstCor = StartCoroutine(SpawnAst());
        _frequencySpawnAstCor = StartCoroutine(FrequencySpawnAst());
    }

    private void GameManager_GameFinished()
    {
        StopCoroutine(_spawnAstCor);
        StopCoroutine(_frequencySpawnAstCor);
    }
}
