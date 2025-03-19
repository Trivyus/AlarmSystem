using System;
using UnityEngine;

public class ThiefDetector : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<ThiefMover>(out _))
            _alarm.ThiefEntered();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ThiefMover>(out _))
            _alarm.ThiefExited();
    }
}