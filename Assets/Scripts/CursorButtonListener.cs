using UnityEngine;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CursorButtonListener : MonoBehaviour
{
    private GameEvents gameEvents;

#region Singleton
    public static CursorButtonListener Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
#endregion


    private void Start()
    {
        gameEvents = GameEvents.Instance;
    }

    private void Update()
    {
        // Check for mouse button events
        if (Input.GetMouseButtonDown(0))
        {
            // Left mouse button down
            gameEvents.TriggerLeftClickDown();
        }
        if (Input.GetMouseButton(0))
        {
            // Left mouse button down
            gameEvents.TriggerLeftClickHold();
        }
        if (Input.GetMouseButtonUp(0))
        {
            // Left mouse button up 
            gameEvents.TriggerLeftClickUp();
        }
        if (Input.GetMouseButtonDown(1))
        {
            // Right mouse button down
            gameEvents.TriggerRightClickDown();
        }
        if (Input.GetMouseButtonUp(1))
        {
            // Right mouse button up
            gameEvents.TriggerRightClickUp();
        }
    }
}

