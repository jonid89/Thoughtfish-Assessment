using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class LoadRecording: IInitializable
{
    Recorder _recorder;
    private List<RecordedAction> recordedActions = new List<RecordedAction>();
    private GameEvents gameEvents;
    PlaybackCursor _playbackCursor;

    public LoadRecording(Recorder recorder, PlaybackCursor playbackCursor)
    {
        _playbackCursor = playbackCursor;
        _recorder = recorder;
    }

    public void Initialize()
    {
        gameEvents = GameEvents.Instance;
    }

    public void LoadRecordingFromFile(string recordingName)
    {
        
        string filePath = Application.persistentDataPath + "/" + recordingName + ".txt";
        Debug.Log("File Path: " + filePath);

        string[] lines = System.IO.File.ReadAllLines(filePath);
        List<RecordedAction> loadedRecording = new List<RecordedAction>();

        foreach (string line in lines)
        {
            string[] data = line.Split(',');
            if (data.Length == 8 && 
                float.TryParse(data[0], out float time) &&
                float.TryParse(data[1], out float x) && 
                float.TryParse(data[2], out float y) &&
                bool.TryParse(data[3], out bool isLeftClickDown) && 
                bool.TryParse(data[4], out bool isLeftClickHold) && 
                bool.TryParse(data[5], out bool isLeftClickUp) && 
                bool.TryParse(data[6], out bool isRightClickDown) &&
                bool.TryParse(data[7], out bool isRightClickUp) 
                )
            {
                Vector2 position = new Vector2(x, y);
                RecordedAction recordedAction = new RecordedAction(time, position, isLeftClickDown, isLeftClickHold, isLeftClickUp, isRightClickDown, isRightClickUp);
                loadedRecording.Add(recordedAction);
            }
        }

        if (loadedRecording.Count > 0)
        {
            recordedActions = loadedRecording;
            PlayRecording(recordedActions);
        }
        else
        {
            Debug.LogWarning("Failed to load recording from File: " + recordingName);
        }
    }

    public void PlayRecording(List<RecordedAction> recording)
    {
        _recorder.StartCoroutine(PlayRecordingCoroutine(recording));
    }

    private System.Collections.IEnumerator PlayRecordingCoroutine(List<RecordedAction> recording)
    {
        Debug.Log("Playing recording...");
        
        foreach (RecordedAction recordedAction in recording)
        {
            yield return new WaitForSeconds(recordedAction.time - Time.time);
            
            _playbackCursor.MoveCursor(recordedAction.position);

            if (recordedAction.isLeftClickDown)
            {
                gameEvents.TriggerLeftClickDown();
            }
            if (recordedAction.isLeftClickHold)
            {
                gameEvents.TriggerLeftClickHold();
            }
            if (recordedAction.isLeftClickUp)
            {
                gameEvents.TriggerLeftClickUp();
            }
            if (recordedAction.isRightClickDown)
            {
                gameEvents.TriggerRightClickDown();
            }
            if (recordedAction.isRightClickUp)
            {
                gameEvents.TriggerRightClickUp();
            }
        }
        _playbackCursor.BackToOriginalPosition();
    }

}
