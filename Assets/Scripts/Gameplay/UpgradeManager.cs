using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [SerializeField]
    UpgradeUI upgradeUI;

    public List<CommandSO> upgradePool;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        instance = this;
        ShowUpgrades();
    }

    public void ShowUpgrades()
    {
        upgradeUI.gameObject.SetActive(true);

        List<CommandSO> pool = upgradePool;
        CommandSO[] chosenUpgrades = new CommandSO[3];

        Shuffle(pool);

        for (int i = 0; i < 3; i++)
        {
            chosenUpgrades[i] = pool[0];
            pool.RemoveAt(0);
            Debug.Log(chosenUpgrades[i]);
        }

        upgradeUI.SetupUpgrades(chosenUpgrades);
    }

    public void SelectUpgrade(CommandSO upgrade)
    {
        SpellCaster.instance.AddNewCommand(upgrade);
        upgradeUI.gameObject.SetActive(false);
    }


    public static void Shuffle(IList<CommandSO> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
