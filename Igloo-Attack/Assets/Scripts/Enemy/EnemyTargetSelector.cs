﻿using System.Collections;
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

                SearchTarget();
            }
        }

        _searchCoroutine = StartCoroutine(ISearch());
    }

    private void SearchTarget() {
        _enemies = FindObjectsOfType<EnemyController>().ToList();
        _enemies.Remove(GetComponent<EnemyController>());

        if (_enemies.Count == 0) {
            SelectedTarget = null;
            return;
        }

        float distance = _searchDistance;
        bool hasTargetFound = false;
        foreach (EnemyController targetEnemy in _enemies) {
            float targetDistance = Vector3.Distance(targetEnemy.transform.position, transform.position);

            if (targetDistance >= _searchDistance) {
                continue;
            }

            if (SelectedTarget == null) {
                SelectedTarget = targetEnemy;
                distance = targetDistance;
                hasTargetFound = true;

                continue;
            }

            if (targetDistance <= distance) {
                SelectedTarget = targetEnemy;
                distance = targetDistance;
                hasTargetFound = true;
            }
        }

        if (!hasTargetFound) {
            SelectedTarget = null;
        }
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