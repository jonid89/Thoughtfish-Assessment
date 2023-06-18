using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        Instance = this;
    }

}


/*
using System;

public class GameEvents
{
    public static GameEvents Instance;

    public event Action OnPointerDown;
    public event Action OnPointerUp;
    public event Action OnPointerEnter;
    public event Action OnPointerExit;

    public static void Initialize()
    {
        Instance = new GameEvents();
    }

    public void TriggerPointerDown()
    {
        OnPointerDown?.Invoke();
    }

    public void TriggerPointerUp()
    {
        OnPointerUp?.Invoke();
    }

    public void TriggerPointerEnter()
    {
        OnPointerEnter?.Invoke();
    }

    public void TriggerPointerExit()
    {
        OnPointerExit?.Invoke();
    }
}

*/