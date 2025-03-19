using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmAudioSource;
    [SerializeField] private float _fadeSpeed = 0.3f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0f;

    private bool _isThiefInside = false;
    private Coroutine _alarmCoroutine;

    public void ThiefEntered()
    {
        _isThiefInside = true;

        if (_alarmCoroutine == null)
            _alarmCoroutine = StartCoroutine(UpdateAlarmVolume());
    }

    public void ThiefExited()
    {
        _isThiefInside = false;

        if (_alarmCoroutine == null)
            _alarmCoroutine = StartCoroutine(UpdateAlarmVolume());
    }

    private IEnumerator UpdateAlarmVolume()
    {
        while (enabled)
        {
            float targetVolume = _isThiefInside ? _maxVolume : _minVolume;

            _alarmAudioSource.volume = Mathf.MoveTowards(_alarmAudioSource.volume, targetVolume, _fadeSpeed * Time.deltaTime);

            if (_alarmAudioSource.volume > 0 && !_alarmAudioSource.isPlaying)
            {
                _alarmAudioSource.Play();
            }
            else if (_alarmAudioSource.volume <= 0 && _alarmAudioSource.isPlaying)
            {
                _alarmAudioSource.Stop();
            }

            if (Mathf.Approximately(_alarmAudioSource.volume, targetVolume))
            {
                _alarmCoroutine = null;
                yield break;
            }

            yield return null;
        }
    }
}
