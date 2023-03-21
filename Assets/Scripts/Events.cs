using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events
{
    //public static readonly Evt<Vector2> onShootRaycast = new Evt<Vector2>();
    //public static readonly Evt onGameWon = new Evt();
    public static readonly Evt<float, Note> onNoteHit = new Evt<float, Note>();
    public static readonly Evt onNoteMiss = new Evt();
    public static readonly Evt<int, int> onScoreChanged = new Evt<int, int>();
}

public class Evt
{
    private event Action action = delegate { };

    public void Invoke()
    {
        action.Invoke();
    }

    public void AddListener(Action listener)
    {
        action += listener;
    }

    public void RemoveListener(Action listener)
    {
        action -= listener;
    }
}

public class Evt<T>
{
    private event Action<T> action = delegate { };

    public void Invoke(T param)
    {
        action.Invoke(param);
    }

    public void AddListener(Action<T> listener)
    {
        action += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        action -= listener;
    }
}

public class Evt<T,T2>
{
    private event Action<T,T2> action = delegate { };

    public void Invoke(T param, T2 param2)
    {
        action.Invoke(param, param2);
    }

    public void AddListener(Action<T, T2> listener)
    {
        action += listener;
    }

    public void RemoveListener(Action<T, T2> listener)
    {
        action -= listener;
    }
}
