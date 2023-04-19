using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Whisper;
using TMPro;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.Windows;

public class AudioInputManager : MonoBehaviour
{
    public WhisperManager whisper;
    public delegate void AudioInputEvent(string input);
    public static AudioInputEvent onAudioInput;
    public string pattern = @"[\[\(][^\]\)]*[\]\)]";

    [Header("Mic settings")]
    public int maxLengthSec = 999;
    public int frequency = 16000;
    public int sampleWindow = 2048;
    public float micPickupThreshold = -20;
    public float micSilentThreshold = -110;
    public int minimumSegmentLength = 4000;
    public int frontPadding = 2000;
    public int backPadding = 2000;

    [Header("UI")]
    public TextMeshProUGUI outputText;
    public TextMeshProUGUI timeText;

    [SerializeField]
    private float _recordStart;
    [SerializeField]
    private bool _isRecording;
    [SerializeField]
    private bool _isSegmentTracking;
    private AudioClip _clip;
    [SerializeField]
    private int _segmentCursor;
    private void OnEnable()
    {
        StartRecord();
    }

    private void Update()
    {
        if (!_isRecording)
            return;

        if(!_isSegmentTracking)
        {
            if(MicrophoneLevelMax() > micPickupThreshold)
            {
                StartSegment();
            }
        } else
        {
            if(MicrophoneLevelMax() < micSilentThreshold)
            {
                StartCoroutine(StopSegment());
            }
        }

        var timePassed = Time.realtimeSinceStartup - _recordStart;
        if (timePassed > maxLengthSec)
            StopRecord();
    }

    public void StartRecord()
    {
        if (_isRecording)
            return;

        _recordStart = Time.realtimeSinceStartup;
        _clip = Microphone.Start(null, false, maxLengthSec, frequency);
        _isRecording = true;
        StartSegment();
    }

    public void StartSegment()
    {
        if (!_isRecording)
            return;

        //UnityEngine.Debug.Log("Starting Segment");
        _isSegmentTracking = true;
        _segmentCursor = Microphone.GetPosition(null);
    }

    public IEnumerator StopSegment()
    {
        if (!_isRecording || !_isSegmentTracking || Microphone.GetPosition(null)-_segmentCursor < minimumSegmentLength)
            yield return null;
        _isSegmentTracking = false;
        int cursor = _segmentCursor;
        yield return new WaitForSeconds(backPadding/frequency);
        var data = GetTrimmedData(cursor);
        Transcribe(data);

        //UnityEngine.Debug.Log("Stopping Segment");
    }

    public void StopRecord()
    {
        if (!_isRecording)
            return;

        StopSegment();

        Microphone.End(null);
        _isRecording = false;
    }

    private float[] GetTrimmedData(int cursor)
    {
        // get microphone samples and current position
        var pos = Microphone.GetPosition(null);
        var data = new float[pos-cursor + frontPadding];
        var origData = new float[_clip.samples * _clip.channels];
        _clip.GetData(data, cursor - frontPadding);

        // check if mic just reached audio buffer end
        if (pos == 0)
            return data;

        // looks like we need to trim it by pos
        //var trimData = new float[pos];
        //Array.Copy(origData, trimData, pos);
        return data;
    }

    private async void Transcribe(float[] data)
    {
        
        if (data.Length < frequency / 4) return;
        //UnityEngine.Debug.Log(data.Length);
        var sw = new Stopwatch();
        sw.Start();

        var res = await whisper.GetTextAsync(data, _clip.frequency, _clip.channels);

        timeText.text = $"Time: {sw.ElapsedMilliseconds} ms";
        if (res == null || res.Result.Length == 0|| Regex.IsMatch(res.Result, pattern))
            return;

        outputText.text = res.Result;
        onAudioInput?.Invoke(res.Result);
    }


    private float MicrophoneLevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clip.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        float db = 20 * Mathf.Log10(Mathf.Abs(levelMax));
        //print(db);
        return db;
    }
    
}