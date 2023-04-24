using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public static UpgradeUI instance;

    [SerializeField]
    CommandSO[] upgrades = new CommandSO[3];
    [SerializeField]
    Button[] upgradeButtons = new Button[3];
    [SerializeField]
    TextMeshProUGUI[] upgradeText = new TextMeshProUGUI[3];

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }

    public void SetupUpgrades(CommandSO[] upgrades)
    {
        this.upgrades[0] = upgrades[0];
        this.upgrades[1] = upgrades[1];
        this.upgrades[2] = upgrades[2];
        for (int i= 0; i< upgradeButtons.Length; i++)
        {
            upgradeButtons[i].image.sprite = upgrades[i].dullSprite;
            SpriteState tempState = upgradeButtons[i].spriteState;
            tempState.highlightedSprite = upgrades[i].brightSprite;
            tempState.pressedSprite = upgrades[i].brightSprite;
            tempState.selectedSprite = upgrades[i].brightSprite;
            tempState.disabledSprite = upgrades[i].dullSprite;
            upgradeButtons[i].spriteState = tempState;

            upgradeText[i].text = upgrades[i].name;

        }
    }

    public void SelectUpgrade(int index)
    {
        UpgradeManager.instance.SelectUpgrade(upgrades[index]);
    }

    public void OnHover(int index)
    {
        upgradeText[index].text = upgrades[index].description;
    }

    public void OnStopHover(int index)
    {
        upgradeText[index].text = upgrades[index].name;
    }
}
