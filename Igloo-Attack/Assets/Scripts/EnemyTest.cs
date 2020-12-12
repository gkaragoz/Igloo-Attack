using UnityEngine;

public class EnemyTest : MonoBehaviour {

    [SerializeField]
    private float _movementSpeed = 1f;

    private void Update() {
        transform.position += transform.forward * Time.deltaTime * _movementSpeed;
    }

}
