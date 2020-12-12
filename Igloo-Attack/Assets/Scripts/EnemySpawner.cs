using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private Transform _originTransform = null;
    [SerializeField]
    private Transform _spawnContainerTransform = null;
    [SerializeField]
    private GameObject _enemyPrefab = null;
    [SerializeField]
    private float _spawnRate = 1f;
    [SerializeField]
    private bool _isSpawnerActive = true;
    [SerializeField]
    private float _spawnRange = 5f;

    [Header("Gizmos")]
    [SerializeField]
    private bool _drawGizmos = true;
    [SerializeField]
    private Color _gizmosColor;
    [SerializeField]
    private int _gizmosFrequency = 2;
    [SerializeField]
    private float _gizmosRadius = 0.1f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private List<GameObject> _enemies = new List<GameObject>();

    private void Awake() {
        IEnumerator Spawner() {
            while (_isSpawnerActive) {
                yield return new WaitForSeconds(_spawnRate);

                Spawn();
            }
        }

        StartCoroutine(Spawner());
    }

    public Vector3 GetSpawnPosition() {
        Vector3 originPosition = _originTransform.position;
        float randomDegree = Random.Range(0f, 360f);

        originPosition = Quaternion.AngleAxis(randomDegree, Vector3.up) * new Vector3(1, 0, 1);
        originPosition *= _spawnRange;

        return originPosition;
    }


    public Vector3 GetSpawnPosition(float degree) {
        Vector3 originPosition = _originTransform.position;

        originPosition = Quaternion.AngleAxis(degree, Vector3.up) * new Vector3(1, 0, 1);
        originPosition *= _spawnRange;

        return originPosition;
    }

    public void Spawn() {
        Vector3 spawnPosition = GetSpawnPosition();

        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, _spawnContainerTransform);

        // TODO: Refactor. Split this rotation to another method.
        newEnemy.transform.LookAt(_originTransform.position, Vector3.up);

        _enemies.Add(newEnemy);
    }

    private void OnDrawGizmos() {
        if (!_drawGizmos) {
            return;
        }

        Gizmos.color = _gizmosColor;

        if (_gizmosFrequency <= 0) {
            _gizmosFrequency = 1;
        }

        for (int ii = 0; ii < 360 / _gizmosFrequency; ii++) {
            Gizmos.DrawSphere(GetSpawnPosition(ii * _gizmosFrequency), _gizmosRadius);
        }
    }

}
