using UnityEngine;

public class AlarmController : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmAudioSource;
    [SerializeField] private float _fadeSpeed = 0.1f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0f;

    private bool _isThiefInside = false;
    private bool _isAlarmActive = false;

    private void Update()
    {
        if (_isAlarmActive == true)
        {
            UpdateAlarmVolume();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<ThiefController>(out _))
        {
            _isThiefInside = true;
            _isAlarmActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ThiefController>(out _))
        {
            _isThiefInside = false;
        }
    }

    private void UpdateAlarmVolume()
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
            _isAlarmActive = false;
        }
    }


}
