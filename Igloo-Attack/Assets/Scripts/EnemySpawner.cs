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

    public Quaternion GetSpawnRotation() {
        return Quaternion.identity;
    }

    public void Spawn() {
        Vector3 spawnPosition = GetSpawnPosition();
        Quaternion spawnRotation = GetSpawnRotation();

        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, spawnRotation, _spawnContainerTransform);

        _enemies.Add(newEnemy);
    }

}
