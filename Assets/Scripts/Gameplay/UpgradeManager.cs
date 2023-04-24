using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public GameObject upgradeUIPanel;
    public CommandSO[] startingUpgradeChoice;


    public List<CommandSO> upgradePool;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Start()
    {
        ShowUpgrades();
    }
    public void ShowUpgrades()
    {
        
        upgradeUIPanel.gameObject.SetActive(true);

        List<CommandSO> pool = new List<CommandSO> (upgradePool);
        CommandSO[] chosenUpgrades = new CommandSO[3];

        Shuffle(pool);

        for (int i = 0; i < 3; i++)
        {
            chosenUpgrades[i] = pool[0];
            pool.RemoveAt(0);
            Debug.Log(chosenUpgrades[i]);
        }
        if (Player.Instance.currentLevel == 1)
        {
            chosenUpgrades = startingUpgradeChoice;
        }
        UpgradeUI.instance.SetupUpgrades(chosenUpgrades);
        Time.timeScale = 0;
    }

    public void SelectUpgrade(CommandSO upgrade)
    {
        SpellCaster.instance.AddNewCommand(upgrade);
        upgradeUIPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
        if(upgradePool.Count > 3)
        {
            upgradePool.Remove(upgrade);
        }
        
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
