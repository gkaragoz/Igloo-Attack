﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IGizmosCircle {

    [SerializeField]
    private GameEvent _onSomethingSpawned = null;

    [Header("Initializations")]
    [SerializeField]
    private Transform _originTransform = null;
    [SerializeField]
    private Transform _spawnContainerTransform = null;
    [SerializeField]
    private EnemyController _enemyPrefab = null;
    [SerializeField]
    private float _spawnRate = 1f;
    [SerializeField]
    private bool _isSpawnerActive = true;
    [SerializeField]
    private float _spawnRange = 5f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private List<IEntity> _enemies = new List<IEntity>();

    private Coroutine _spawnerCoroutine = null;

    private void Awake() {
        IEnumerator Spawner() {
            while (true) {
                yield return new WaitForSeconds(_spawnRate);

                if (_isSpawnerActive) {
                    Spawn();
                }
            }
        }

        _spawnerCoroutine = StartCoroutine(Spawner());
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

        EnemyController newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, _spawnContainerTransform);
        newEnemy.name = "ENEMY(" + _enemies.Count + ")";

        // TODO: Refactor. Split this rotation to another method.
        newEnemy.transform.LookAt(_originTransform.position, Vector3.up);

        _enemies.Add(newEnemy);

        _onSomethingSpawned.Raise();
    }

    public Vector2 GetRadius() {
        return new Vector2(_spawnRange, _spawnRange);
    }

    public void DebugTest() {
        Debug.Log(_enemies.Count);
    }

    private void OnDestroy() {
        StopCoroutine(_spawnerCoroutine);
    }

}
