using UnityEngine;

public abstract class UpgradeBase : ScriptableObject
{
    public abstract void ApplyUpgrade();
}

public abstract class Upgrade<T> : UpgradeBase where T : ScriptableObject
{
    public T Data;
    public override void ApplyUpgrade() { /* Base implementation */ }
}