using UnityEngine;

[CreateAssetMenu(fileName = "FloatUpgrade", menuName = "Upgrades/FloatUpgrade")]
public class FloatUpgrade : Upgrade<FloatData>
{
    public float IncreaseAmount;

    public override void ApplyUpgrade()
    {
        Data.value += IncreaseAmount;
    }
}