using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidPassed : MonoBehaviour
{
    [SerializeField]private Text _countAsteroidBox;

    private static int _amountOfAsteroidPassed;

    private void OnEnable()
    {
        AsteroidController.AsteroidPassed += AsteroidController_ChangeValueAsteroidPassed;
        StartCoroutine(AmountOfAsteroidPassed());
    }

    private void OnDisable()
    {
        AsteroidController.AsteroidPassed -= AsteroidController_ChangeValueAsteroidPassed;
    }

    IEnumerator AmountOfAsteroidPassed()
    {
        var waiter = new WaitForSeconds(0.1f);
        _amountOfAsteroidPassed = 0;
        while (true)
        {
            yield return waiter;
            if (_amountOfAsteroidPassed <= 9)
            {
                _countAsteroidBox.text = "0" + _amountOfAsteroidPassed;
            }
            else
            {
                _countAsteroidBox.text = "" + _amountOfAsteroidPassed;
            }
        }
    }

    private void AsteroidController_ChangeValueAsteroidPassed()
    {
        _amountOfAsteroidPassed++;
    }
}
