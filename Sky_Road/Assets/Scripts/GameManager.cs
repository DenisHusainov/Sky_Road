using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action Started = delegate { };
    public static event Action Fineshed = delegate { };

    public static GameManager Instance;

    public bool IsStarted { get; private set; }
    public bool IsFineshed { get; private set; }

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

    private void Update()
    {
        if (Input.anyKey && !IsStarted)
        {
            OnStarted();
        }
    }

    private void OnStarted()
    {
        IsStarted = true;
        Started();
    }

    private void MovementController_Died()
    {
        IsFineshed = true;
        Fineshed();
    }
}
