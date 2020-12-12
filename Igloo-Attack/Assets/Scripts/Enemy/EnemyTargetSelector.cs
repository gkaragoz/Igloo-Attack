using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTargetSelector : MonoBehaviour, IGizmosCircle {

    [Header("Initializations")]
    [SerializeField]
    private float _searchDistance = 10f;
    [SerializeField]
    private float _searchRate = 1f;

    [Header("Debug")]
    [SerializeField]
    private EnemyController _selectedTarget = null;
    [SerializeField]
    private List<EnemyController> _enemies = null;

    private Coroutine _searchCoroutine = null;

    public EnemyController SelectedTarget { 
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

    private void Awake() {
        IEnumerator ISearch() {
            while (true) {
                yield return new WaitForSeconds(_searchRate);

                SelectedTarget = GetClosestTarget();
            }
        }

        _searchCoroutine = StartCoroutine(ISearch());
    }

    private EnemyController GetClosestTarget() {
        _enemies = FindObjectsOfType<EnemyController>().ToList();
        _enemies.Remove(GetComponent<EnemyController>());

        if (_enemies.Count == 0) {
            return null;
        }

        float tempDistance = _searchDistance;
        EnemyController closestTarget = null;

        for (int ii = 0; ii < _enemies.Count; ii++) {
            EnemyController potentialTarget = _enemies[ii];
            float targetDistance = Vector3.Distance(potentialTarget.transform.position, transform.position);

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
            Gizmos.DrawWireSphere(SelectedTarget.transform.position, 0.25f);
        }
    }

    public Vector2 GetRadius() {
        return new Vector2(_searchDistance, _searchDistance);
    }

    private void OnDestroy() {
        StopCoroutine(_searchCoroutine);
    }

}
