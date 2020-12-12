using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private float _attackRate = 1.5f;

    [Header("Debug")]
    [SerializeField]
    [Utils.ReadOnly]
    private bool _isAttacking = false;

    private void Awake() {
        IEnumerator Attacker() {
            while (true) {
                yield return new WaitForSeconds(_attackRate);

                if (_isAttacking) {
                    Attack();
                }
            }
        }

        StartCoroutine(Attacker());
    }

    private void Attack() {
        Debug.LogWarning(this.gameObject.name + " attacking...");
    }

    public void Run() {
        _isAttacking = true;
    }

    public void Stop() {
        _isAttacking = false;
    }

}
