using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class SaveRecording
{
    private List<RecordedAction> recordedActions = new List<RecordedAction>();

    public void SaveRecordingToFile(string recordingName, List<RecordedAction> recordedActionsToSave)
    {
        recordedActions = recordedActionsToSave;
        string filePath = Application.persistentDataPath + "/" + recordingName + ".txt";
        List<string> lines = new List<string>();

        foreach (RecordedAction recordedAction in recordedActionsToSave)
        {
            string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                recordedAction.time, 
                recordedAction.position.x, 
                recordedAction.position.y,
                recordedAction.isLeftClickDown, 
                recordedAction.isLeftClickHold,
                recordedAction.isLeftClickUp,  
                recordedAction.isRightClickDown,
                recordedAction.isRightClickUp);
            lines.Add(line);
        }

        System.IO.File.WriteAllLines(filePath, lines.ToArray());
        Debug.Log("Recording saved to file: " + filePath);

    }

}
