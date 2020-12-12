using UnityEngine;

public class EnemyController : MonoBehaviour {

    private EnemyMotor _enemyMotor = null;
    private EnemyAttack _enemyAttack = null;
    
    private void Awake() {
        _enemyMotor = GetComponent<EnemyMotor>();
        _enemyAttack = GetComponent<EnemyAttack>();

        StartMovement();
    }

    public void StartMovement() {
        _enemyMotor.Run();
    }

    public void StopMovement() {
        _enemyMotor.Stop();
    }

    public void StartAttack() {
        _enemyAttack.Run();
    }

    public void StopAttack() {
        _enemyAttack.Stop();
    }

    public void Die() {
        StopMovement();
        StopAttack();

        // Die;
    }

}
