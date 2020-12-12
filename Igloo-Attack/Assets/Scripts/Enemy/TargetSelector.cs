using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour, ITargetable, IGizmosCircle {

    [Header("Initializations")]
    [SerializeField]
    [EnumFlags]
    private TargetFlag _targetFlag;
    [SerializeField]
    private float _searchDistance = 10f;
    [SerializeField]
    private float _searchRate = 1f;

    [Header("Debug")]
    [SerializeField]
    private IEntity _selectedTarget = null;
    [SerializeField]
    private List<IEntity> _enemies = new List<IEntity>();

    private Coroutine _searchCoroutine = null;

    public IEntity SelectedTarget { 
        get {
            return _selectedTarget;
        }
        private set {
            _selectedTarget = value;
        }
    }

    public bool HasTarget {
        get {
            return _selectedTarget != null;
        }
    }

    public TargetFlag TargetFlag {
        get {
            return this._targetFlag;
        }
    }

    public Transform Transform {
        get {
            return this.transform;
        }
    }

    private void Awake() {
        IEnumerator ISearch() {
            while (true) {
                yield return new WaitForSeconds(_searchRate);

                SelectedTarget = GetClosestTarget();
            }
        }

        _searchCoroutine = StartCoroutine(ISearch());
    }

    private IEntity GetClosestTarget() {
        //_enemies = GameObject.FindObjectOfType<IEntity>();

        if (_enemies.Count == 0) {
            return null;
        }

        float tempDistance = _searchDistance;
        IEntity closestTarget = null;

        for (int ii = 0; ii < _enemies.Count; ii++) {
            IEntity potentialTarget = _enemies[ii];
            float targetDistance = Vector3.Distance(potentialTarget.Transform.position, transform.position);

            // Potential target is closer than my current closestTarget?
            if (targetDistance <= tempDistance) {
                closestTarget = potentialTarget;
                tempDistance = targetDistance;
            }
        }

        return closestTarget;
    }

    private void OnDrawGizmosSelected() {
        if (HasTarget) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(SelectedTarget.Transform.position, 0.25f);
        }
    }

    public Vector2 GetRadius() {
        return new Vector2(_searchDistance, _searchDistance);
    }

    private void OnDestroy() {
        StopCoroutine(_searchCoroutine);
    }

}
