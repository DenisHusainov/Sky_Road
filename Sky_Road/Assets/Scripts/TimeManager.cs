using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    private const int TimerDelay = 1;

    [SerializeField] private Text _secBox;
    [SerializeField] private Text _minBox;

    private Coroutine _timeCor;
    
    private int _min = 0;
    private int _sec = 0;

    private void OnEnable()
    {
        GameManager.Started += GameManager_OnStarted;
        GameManager.Fineshed += GameManager_OnFinished;
    }

    private void OnDisable()
    {
        GameManager.Started -= GameManager_OnStarted;
        GameManager.Fineshed -= GameManager_OnFinished;
    }

    IEnumerator Timer()
    {
        var waiter = new WaitForSeconds(TimerDelay);
        while (true)
        {
            _sec++;
            if (_sec <= 9)
            {
                _secBox.text = "0" + _sec;
            }
            else
            {
                _secBox.text = "" + _sec;
            }

            if (_sec >= 60)
            {
                _sec = 0;
                _min += 1;
            }

            if (_min <= 9)
            {
                _minBox.text = "0" + _min + ":";
            }
            else
            {
                _minBox.text = "" + _min + ":";
            }

            yield return waiter;
        }
       
    }

    private void GameManager_OnStarted()
    {
        _timeCor = StartCoroutine(Timer());
    }

    private void GameManager_OnFinished()
    {
        StopCoroutine(_timeCor);
    }
}
