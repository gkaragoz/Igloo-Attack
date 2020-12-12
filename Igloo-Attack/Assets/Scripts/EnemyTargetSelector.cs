using UnityEngine;

public class EnemyTargetSelector : GizmosMonoBehaviour {

    [Header("Initializations")]
    [SerializeField]
    private float _searchDistance = 10f;

    public override void OnDrawGizmosSelected() {
        _gizmosRadius = _searchDistance;

        base.OnDrawGizmosSelected();
    }

}
