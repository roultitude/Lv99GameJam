using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class CommandCooldownUI : MonoBehaviour
{
    public static CommandCooldownUI instance;

    [SerializeField]
    GameObject commandCooldownPanel;
    [SerializeField]
    CommandCooldownUIPrefab commandCooldownUIPrefab;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        instance= this;
    }
    public void AddCommandToUi(Command command, CommandSO commandSO)
    {
        Instantiate(commandCooldownUIPrefab, commandCooldownPanel.transform).Setup(command,commandSO);
        
    }
}
