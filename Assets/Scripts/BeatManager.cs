using System;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float BPM = 120.0f;
    [SerializeField] private float windowSize = 0.1f;

    private float beatInterval;
    private float nextBeatTime;
    private float halfWindowSize;

    public float NextBeatTime => nextBeatTime;
    public float WindowSize => windowSize;

    private void Start()
    {
        SetBPM(BPM);
        SetWindowSize(windowSize);
        Reset();
    }

    private void Update()
    {
        nextBeatTime -= Time.deltaTime;
        if (nextBeatTime <= 0)
        {
            nextBeatTime += beatInterval;
        }
    }

    public bool IsInputWithinWindow()
    {
        return Mathf.Abs(nextBeatTime) <= halfWindowSize;
    }

    private void SetBPM(float bpm)
    {
        if (bpm <= 0)
            throw new ArgumentException("BPM must be greater than 0");

        BPM = bpm;
        beatInterval = 60.0f / bpm;
    }

    private void SetWindowSize(float windowSize)
    {
        if (windowSize <= 0)
            throw new ArgumentException("WindowSize must be greater than 0");

        this.windowSize = windowSize;
        halfWindowSize = windowSize / 2.0f;
    }

    private void Reset()
    {
        nextBeatTime = beatInterval;
    }
}
