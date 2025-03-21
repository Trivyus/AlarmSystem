using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmAudioSource;
    [SerializeField] private ThiefDetector _thiefDetector;
    [SerializeField] private float _fadeSpeed = 0.3f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0f;

    private Coroutine _alarmCoroutine;

    private void OnEnable()
    {
        _thiefDetector.ThiefEntered += Activate;
        _thiefDetector.ThiefEscaped += Deactivate;
    }

    private void OnDisable()
    {
        _thiefDetector.ThiefEntered -= Activate;
        _thiefDetector.ThiefEscaped -= Deactivate;
    }

    private void Activate()
    {
        if (_alarmCoroutine != null)
        {
            StopCoroutine(_alarmCoroutine);
        }
        _alarmCoroutine = StartCoroutine(IncreaseVolume());
    }

    private void Deactivate()
    {
        if (_alarmCoroutine != null)
        {
            StopCoroutine(_alarmCoroutine);
        }
        _alarmCoroutine = StartCoroutine(TurnDownVolume());
    }

    private IEnumerator IncreaseVolume()
    {
        if (!_alarmAudioSource.isPlaying)
            _alarmAudioSource.Play();

        while (Mathf.Approximately(_alarmAudioSource.volume, _maxVolume) == false)
        {
            _alarmAudioSource.volume = Mathf.MoveTowards(_alarmAudioSource.volume, _maxVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator TurnDownVolume()
    {
        while (Mathf.Approximately(_alarmAudioSource.volume, _minVolume) == false)
        {
            _alarmAudioSource.volume = Mathf.MoveTowards(_alarmAudioSource.volume, _minVolume, _fadeSpeed * Time.deltaTime);
            yield return null;
        }

        if (_alarmAudioSource.volume <= 0 && _alarmAudioSource.isPlaying)
            _alarmAudioSource.Stop();
    }
}
