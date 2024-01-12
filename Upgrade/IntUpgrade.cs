using UnityEngine;

[CreateAssetMenu(fileName = "IntUpgrade", menuName = "Upgrades/IntUpgrade")]
public class IntUpgrade : Upgrade<IntData>
{
    public int IncreaseAmount;

    public override void ApplyUpgrade()
    {
        Data.value += IncreaseAmount;
    }
}