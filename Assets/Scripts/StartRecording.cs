using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class StartRecording
{
    Recorder recorder;
    SaveRecording saveRecording;
    private string _recordingName;

    private List<RecordedAction> recordedActions = new List<RecordedAction>();

    public StartRecording(SaveRecording saveRecording)
    {
        
        this.saveRecording = saveRecording;
    }

    public void StartRecordingMethod(string recordingName)
    {
        Debug.Log("StartRecordingMethod called");
        _recordingName = recordingName;
        recordedActions.Clear();
    }

    public void RecordAction()
    {
        Debug.Log("RecordAction Method called");
        float time = Time.time;
        Vector2 position = Input.mousePosition;
        bool isLeftClickDown = Input.GetMouseButtonDown(0);
        bool isLeftClickHold = Input.GetMouseButton(0);
        bool isLeftClickUp = Input.GetMouseButtonUp(0);
        bool isRightClickDown = Input.GetMouseButtonDown(1);
        bool isRightClickUp = Input.GetMouseButtonUp(1);

        RecordedAction recordedAction = new RecordedAction(time, position, isLeftClickDown, isLeftClickHold, isLeftClickUp, isRightClickDown, isRightClickUp);
        recordedActions.Add(recordedAction);
    }

    public void StopRecording()
    {
        saveRecording.SaveRecordingToFile(_recordingName, recordedActions);
    }

}
