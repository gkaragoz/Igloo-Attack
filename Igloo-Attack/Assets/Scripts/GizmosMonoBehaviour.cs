using UnityEngine;

[ExecuteAlways]
public class GizmosMonoBehaviour : MonoBehaviour {

    [Header("Gizmos")]
    [SerializeField]
    protected Color _gizmosColor;
    [SerializeField]
    protected float _gizmosRadius_X = 5f;
    [SerializeField]
    protected float _gizmosRadius_Y = 5f;
    [SerializeField]
    protected float _gizmosDotSize = 0.1f;
    [SerializeField]
    protected float _gizmosFrequency = 1f;
    [SerializeField]
    protected bool _drawGizmos = true;

    public virtual void OnDrawGizmosSelected() {
        if (!_drawGizmos) {
            return;
        }

        Gizmos.color = _gizmosColor;
        Utils.DrawCircle(transform.position, _gizmosFrequency, _gizmosRadius_X, _gizmosRadius_Y, _gizmosDotSize);
    }

}