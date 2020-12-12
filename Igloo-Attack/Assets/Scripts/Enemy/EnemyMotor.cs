using UnityEngine;

public class EnemyMotor : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private float _movementSpeed = 1.5f;
    [SerializeField]
    private float _rotationSpeed = 1.5f;
    [SerializeField]
    private float _stoppingDistance = 1f;

    [Header("Gizmos")]
    [SerializeField]
    private Color _forwardGizmosColor;
    [SerializeField]
    private bool _forwardDrawGizmos = true;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isMoving = false;

    public bool IsMoving {
        get {
            return _isMoving;
        }
    }
    
    private TargetSelector _targetSelector = null;

    private void Awake() {
        _targetSelector = GetComponent<TargetSelector>();
    }

    private void Update() {
        if (_targetSelector.HasTarget) {
            RotateToTarget(_targetSelector.SelectedTarget.Transform);
        }

        if (HasReachedDestination()) {
            Stop();
        } else {
            Move();
        }
    }

    private bool HasReachedDestination() {
        if (_targetSelector.HasTarget) {
            float distance = Vector3.Distance(transform.position, _targetSelector.SelectedTarget.Transform.position);
            return distance <= _stoppingDistance;
        }
        return false;
    }

    private void Move() {
        if (_targetSelector.HasTarget) {
            MoveToTarget(_targetSelector.SelectedTarget.Transform);
        } else {
            MoveForward();
        }
    }

    private void MoveForward() {
        transform.position += transform.forward * _movementSpeed * Time.deltaTime;
    }

    private void MoveToTarget(Transform targetTransform) {
        Vector3 direction = targetTransform.position - transform.position;
        transform.position += direction.normalized * _movementSpeed * Time.deltaTime;
    }

    private void RotateToTarget(Transform targetTransform) {
        Quaternion targetRotation = Quaternion.LookRotation(targetTransform.position - transform.position, transform.up);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Run() {
        if (!_isMoving) {
            _isMoving = true;
        }
    }

    public void Stop() {
        if (_isMoving) {
            _isMoving = false;
        }
    }

    private void OnDrawGizmos() {
        if (_forwardDrawGizmos) {
            Gizmos.color = _forwardGizmosColor;
            Gizmos.DrawLine(transform.position + Vector3.up * 0.25f, transform.position + transform.forward + Vector3.up * 0.25f);
        }
    }

}
