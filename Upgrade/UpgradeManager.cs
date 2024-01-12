using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int conditionalOccurrence, counter;
    public GameAction upgradeOnAction;
    
    // List of all upgrades
    public List<UpgradeBase> upgrades;

    void OnEnable()
    {
        upgradeOnAction.Raise += CheckForUpgrades;
    }

    void OnDisable()
    {
        upgradeOnAction.Raise -= CheckForUpgrades;
    }

    private void CheckForUpgrades(GameAction action)
    {
        if ((conditionalOccurrence == 0 && counter == 0) || conditionalOccurrence % counter == 0)
        {
            counter = 0;
            foreach (var upgrade in upgrades)
            {
                PerformUpgrade(upgrade);
            }
        }
        else
        {
            counter++;
        }
    }

    private void PerformUpgrade(UpgradeBase upgrade)
    {
        upgrade.ApplyUpgrade();
    }
}
