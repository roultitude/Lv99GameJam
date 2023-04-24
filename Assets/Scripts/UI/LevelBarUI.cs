using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarUI : MonoBehaviour
{
    [SerializeField]
    Image levelBar1;
    [SerializeField]
    Image levelBar2;
    [SerializeField]
    TextMeshProUGUI levelText;

    private void Update()
    {
        levelBar1.fillAmount = Player.Instance.GetEXPFill();
        levelBar2.fillAmount = Player.Instance.GetEXPFill();
        levelText.text = "Level " + Player.Instance.currentLevel.ToString();
    }
}
