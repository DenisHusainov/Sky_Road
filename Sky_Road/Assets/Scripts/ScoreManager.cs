using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private const int AcelaretionMultiplierPointUp = 2;
    private const int AccelaretionMultiplierPointDown = 1;
    private const int BonusPointAsteroidPassed = 5;

    [SerializeField] private Text _scoreBox;
    [SerializeField] private Text _recordBox;

    private int _score;
    private int _record;
    private int _recordDelay = 1;
    private int _oneSecPoint;
    
    private Coroutine _recordCor = null;

    private void OnEnable()
    {
        GameManager.Started += GameManager_OnStarted;
        GameManager.Fineshed += GameManager_OnFinished;
        MovementController.AccelerationStarted += MovementController_AccelarationStarted;
        MovementController.AccelerationFinished += MovementController_AccelarationFinished;
        AsteroidController.AsteroidPassed += AsteroidController_AsteroidPassed;
    }

    private void OnDisable()
    {
        GameManager.Started -= GameManager_OnStarted;
        GameManager.Fineshed -= GameManager_OnFinished;
        MovementController.AccelerationStarted -= MovementController_AccelarationStarted;
        MovementController.AccelerationFinished -= MovementController_AccelarationFinished;
        AsteroidController.AsteroidPassed -= AsteroidController_AsteroidPassed;
    }

    private void Start()
    {
        _recordBox.text = PlayerPrefs.GetInt("bestRecord").ToString("F0");
        _record = PlayerPrefs.GetInt("bestRecord");
    }

    IEnumerator Record()
    {
        var waiter = new WaitForSeconds(_recordDelay);
        _oneSecPoint = 1;
        while (true)
        {
            _score += _oneSecPoint;
            if (_score <= 9)
            {
                _scoreBox.text = "0" + _score;
            }
            else
            {
                _scoreBox.text = "" + _score;
            }

            if (_record < _score)
            {
                _record = _score;

                PlayerPrefs.SetInt("bestRecord", _record);
                PlayerPrefs.Save();

                if (_record <= 9)
                {
                    _recordBox.text = "0" + _record;
                }
                else
                {
                    _recordBox.text = "" + _record;
                }
            }

            yield return waiter;
        }
       
    }

    private void GameManager_OnStarted()
    {
        _recordCor = StartCoroutine(Record());
    }

    private void GameManager_OnFinished()
    {
        StopCoroutine(_recordCor);
    }

    private void MovementController_AccelarationStarted()
    {
        _oneSecPoint = AcelaretionMultiplierPointUp;
    }

    private void MovementController_AccelarationFinished()
    {
        _oneSecPoint = AccelaretionMultiplierPointDown;
    }

    private void AsteroidController_AsteroidPassed()
    {
        _score += BonusPointAsteroidPassed;
    }
}
