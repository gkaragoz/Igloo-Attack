using UnityEngine;

public interface ITargetable {

    TargetFlag TargetFlag { get; }
    Transform Transform { get; }

}
