using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandCooldownUIPrefab : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    Image icon;
    [SerializeField]
    Image cooldownMask;

    [SerializeField]
    Command command;
    [SerializeField]
    CommandSO commandSO;

    public void Setup(Command command, CommandSO commandSO)
    {
        this.command = command;
        this.commandSO = commandSO;
        text.text = commandSO.name;
    }

    public void UpdateCooldownGraphic()
    {
        cooldownMask.fillAmount = command.GetCooldownRemaining();
        if(command.GetCooldownRemaining() == 0)
        {
            icon.sprite = commandSO.brightSprite;
        } else
        {
            icon.sprite = commandSO.dullSprite;
        }
    }

    private void Update()
    {
        UpdateCooldownGraphic();
    }
}
