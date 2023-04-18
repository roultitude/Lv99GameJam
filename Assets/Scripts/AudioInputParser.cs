using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInputParser : MonoBehaviour
{

    public List<CommandSO> commands;

    // Start is called before the first frame update
    void Start()
    {
        AudioInputManager.onAudioInput += OnAudioInput;
    }

    public void OnAudioInput(string input)
    {
        print(input + "hello");
    }
}
