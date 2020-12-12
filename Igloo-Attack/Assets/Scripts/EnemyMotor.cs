using UnityEngine;

public class EnemyMotor : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private float _movementSpeed = 1.5f;
    [SerializeField]
    private float _rotationSpeed = 1.5f;
    [SerializeField]
    private float _stoppingDistance = 1f;
    [SerializeField]
    private float _searchDistance = 2f;

    [Header("Gizmos")]
    [SerializeField]
    private Color _searchGizmosColor;
    [SerializeField]
    private bool _searchDrawGizmos = true;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isMoving = false;

    private EnemyTargetSelector _targetSelector;

    private void Awake() {
        _targetSelector = GetComponent<EnemyTargetSelector>();
    }

    private void Update() {
        if (HasReachedDestination()) {
            Stop();
        } else {
            Move();
        }
    }

    private bool HasReachedDestination() {
        float distance = Vector3.Distance(transform.position, Vector3.zero);
        return distance <= _searchDistance;
    }

    private void Move() {
        transform.position += transform.forward * _movementSpeed * Time.deltaTime;
    }

    private void RotateToTarget() {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.zero), _rotationSpeed * Time.deltaTime);
    }

    public void Run() {
        _isMoving = true;
    }

    public void Stop() {
        _isMoving = false;
    }

    private void OnDrawGizmos() {
        if (_searchDrawGizmos) {
            Gizmos.color = _searchGizmosColor;
            Gizmos.DrawLine(transform.position, transform.position + (transform.forward * _searchDistance));
        }
    }

}
