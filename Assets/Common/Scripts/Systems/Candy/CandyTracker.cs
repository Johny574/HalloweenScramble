using System;
using UnityEngine;

public class CandyTracker : Singleton<CandyTracker>
{
    [SerializeField] int _basketSize = 5;
    int _collected = 0;
    public Action CandyCollected;
    public Action<int> CandyDropped;
    public string HUDDisplay;
    [SerializeField] AudioSource _collectAudio, _dropAudio;

    void Start() {
        CandyCollected += OnCandyCollected;
        HUDDisplay = $"Candy {_collected}/{_basketSize}";
    }

    public bool HasSpace() => _collected < _basketSize;

    public void DropCandy()
    {
        CandyDropped?.Invoke(_collected);
        _collected = 0;
        HUDDisplay = $"Candy {_collected}/{_basketSize}";
        _dropAudio.Play();
    }

    public void OnCandyCollected()
    {
        _collected++;
        HUDDisplay = $"Candy {_collected}/{_basketSize}";
        _collectAudio.Play();
    }

    public bool CanCollect() => _collected < _basketSize;


#if UNITY_INCLUDE_TESTS

    public int Collected => _collected;
    #endif
}
