using System;
using UnityEngine;

public class ThiefDetector : MonoBehaviour
{
    private int _numberOfThiefs = 0;
    public event Action ThiefEntered;
    public event Action ThiefEscaped;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ThiefMover>(out _))
        {
            _numberOfThiefs++;

            if (_numberOfThiefs == 1)
            {
                ThiefEntered?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ThiefMover>(out _))
        {
            _numberOfThiefs--;

            if (_numberOfThiefs <= 0)
            {
                ThiefEscaped?.Invoke();
            }
        }
    }
}