using System;
using UnityEngine;

public enum Phase { Boot, Menu, Playing, Paused, GameOver }

public class GameFlow : MonoBehaviour
{
    public static GameFlow I { get; private set; }
    public Phase Phase { get; private set; }
    public event Action<Phase> Changed;

    void Awake() => I = this;
    public void Go(Phase next)
    {
        if (next == Phase) return;
        Phase = next;
        Time.timeScale = next == Phase.Paused ? 0f : 1f;
        Changed?.Invoke(next);
    }
}