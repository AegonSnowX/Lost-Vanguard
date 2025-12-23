using UnityEngine;
using System;

public class ControlRoomManager : MonoBehaviour
{
    public static ControlRoomManager Instance;

    public bool IsMainPowerOn { get; private set; }

    public event Action OnMainPowerRestored;

    private void Awake()
    {
        Instance = this;
        IsMainPowerOn = false;
    }

    public void RestoreMainPower()
    {
        if (IsMainPowerOn) return;

        IsMainPowerOn = true;
        Debug.Log("Main power has been restored.");

        OnMainPowerRestored?.Invoke();
    }
}
