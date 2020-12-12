using UnityEngine;

[ExecuteAlways]
public class GizmosMonoBehaviour : MonoBehaviour {

    [Header("Gizmos")]
    [SerializeField]
    protected Color _gizmosColor;
    [SerializeField]
    protected float _gizmosRadius = 5f;
    [SerializeField]
    protected float _gizmosDotRadius = 0.1f;
    [SerializeField]
    protected float _gizmosFrequency = 1f;
    [SerializeField]
    protected bool _drawGizmos = true;

    public virtual void OnDrawGizmosSelected() {
        Gizmos.color = _gizmosColor;

        if (_gizmosFrequency <= 0) {
            _gizmosFrequency = 1;
        }

        for (int ii = 0; ii < 360 / _gizmosFrequency; ii++) {
            Vector3 position = Quaternion.AngleAxis(ii * _gizmosFrequency, Vector3.up) * new Vector3(_gizmosRadius, 0f, _gizmosRadius);

            Gizmos.DrawSphere(transform.position + position, _gizmosDotRadius);
        }
    }

}