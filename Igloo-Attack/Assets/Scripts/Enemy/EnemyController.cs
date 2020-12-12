using UnityEngine;

public class EnemyController : MonoBehaviour, IEntity {

    [Header("Initializations")]
    [SerializeField]
    private EntityEnum _entityEnum;

    private EnemyMotor _enemyMotor = null;
    private EnemyAttack _enemyAttack = null;

    public Transform Transform {
        get {
            return this.transform;
        }
    }

    public EntityEnum EntityEnum {
        get {
            return this._entityEnum;
        }
    }

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
