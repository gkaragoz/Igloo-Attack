using UnityEngine;

[ExecuteAlways]
public class GizmosCircle : MonoBehaviour {

    [Header("Gizmos")]
    [SerializeReference]
    private MonoBehaviour _dataScript = null;
    [SerializeField]
    private Transform _centerTransform = null;
    [SerializeField]
    private Color _gizmosColor;
    [SerializeField]
    private float _gizmosDotSize = 0.1f;
    [SerializeField]
    private float _gizmosFrequency = 1f;
    [SerializeField]
    private bool _drawGizmos = true;

    private IGizmosCircle _IGizmosCircle;

    private void OnDrawGizmosSelected() {
        if (_dataScript == null) {
            return;
        }

        if (_dataScript is IGizmosCircle) {
            _IGizmosCircle = _dataScript as IGizmosCircle;
        } else {
            _IGizmosCircle = null;
        }

        if (_IGizmosCircle == null) {
            return;
        }
        
        if (!_drawGizmos) {
            return;
        }

        if (_centerTransform == null) {
            _centerTransform = transform;
        }

        Gizmos.color = _gizmosColor;
        Utils.DrawCircle(_centerTransform.position, _gizmosFrequency, _IGizmosCircle.GetRadius().x, _IGizmosCircle.GetRadius().y, _gizmosDotSize);
    }

}