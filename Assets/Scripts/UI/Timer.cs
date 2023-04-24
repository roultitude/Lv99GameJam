using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float gameTime;
    [SerializeField]
    TextMeshProUGUI timeText;

    private void Awake()
    {
        gameTime = 0;
    }

    private void Update()
    {
        gameTime+= Time.deltaTime;
        timeText.text = gameTime.ToString("F2");
    }
}
