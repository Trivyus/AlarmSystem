using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class ThiefMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _stoppingDistance = 0.1f;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _rightMouseButton = 1;

    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_rightMouseButton))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
                _targetPosition = hit.point;

            _isMoving = true;
        }

        if (_isMoving && _targetPosition != null)
            MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 direction = (_targetPosition - transform.position).normalized;
        _rigidbody.MovePosition(transform.position + _speed * Time.deltaTime * direction);

        if ((_targetPosition - transform.position).sqrMagnitude < _stoppingDistance * _stoppingDistance)
            _isMoving = false;
    }
}