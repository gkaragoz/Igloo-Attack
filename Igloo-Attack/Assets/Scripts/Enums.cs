using System;

public enum EntityEnum {
    None,
    Igloo,
    Bird,
    Penguin
}

[Flags]
public enum TargetFlag {
    None = 0,
    Igloo = 1,
    Bird = 2,
    Penguin = 4
}

//// A helper extension is provided to check for flags
//if (Animal.HasFlag(AnimalType.Cat)) {
//    Debug.Log("Cat flag is set");
//}

//if (Animal.HasFlag(AnimalType.Cat | AnimalType.Fish)) {
//    Debug.Log("Cat & Fish flags are set");
//}
