using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events
{
    //public static readonly Evt<Vector2> onShootRaycast = new Evt<Vector2>();
    //public static readonly Evt onGameWon = new Evt();
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

