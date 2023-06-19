using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Recorder : MonoBehaviour
{
    private bool isRecording = false;
    public bool IsRecording => isRecording;

    private List<RecordedAction> recordedActions = new List<RecordedAction>();
    public Text nameText;
    private string recordingName;

    public PlaybackCursor playbackCursor;
    private float pointerDownTime;
    private GameEvents gameEvents;

    [System.Serializable]
    public struct RecordedAction
    {
        public float time;
        public Vector2 position;
        public bool isLeftClickDown;
        public bool isLeftClickHold;
        public bool isLeftClickUp;
        public bool isRightClickDown;
        public bool isRightClickUp;

        public RecordedAction(float time, Vector2 position, bool isLeftClickDown, bool isLeftClickHold, bool isLeftClickUp, bool isRightClickDown, bool isRightClickUp)
        {
            this.time = time;
            this.position = position;
            this.isLeftClickDown = isLeftClickDown;
            this.isLeftClickHold =isLeftClickHold;
            this.isLeftClickUp =isLeftClickUp;
            this.isRightClickDown = isRightClickDown;
            this.isRightClickUp = isRightClickUp;
        }
    }

    private void Start()
    {
        gameEvents = GameEvents.Instance;
        recordedActions = new List<RecordedAction>();
    }

    public void StartRecording()
    {
        isRecording = true;
        recordedActions.Clear();
        recordingName = nameText.text.ToString();
        Debug.Log("Recording name: " + recordingName);
    }

    public void StopRecording()
    {
        SaveRecording();
        isRecording = false;
    }

    public void PlayRecording(List<RecordedAction> recording)
    {
        //recordingName = nameText.text.ToString();
        playbackCursor.gameObject.SetActive(true);
        StartCoroutine(PlayRecordingCoroutine(recording));
    }

    public void LoadRecordingFromFile()
    {
        recordingName = nameText.text.ToString();
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
            Debug.LogWarning("Failed to load recording from file: " + recordingName);
        }
    }


    private void Update()
    {   
        if (isRecording)
        {
            RecordAction();
        }
    }

    public void RecordAction()
    {
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

    private System.Collections.IEnumerator PlayRecordingCoroutine(List<RecordedAction> recording)
    {
        Debug.Log("Playing recording...");
        
        foreach (RecordedAction recordedAction in recording)
        {
            yield return new WaitForSeconds(recordedAction.time - Time.time);
            
            playbackCursor.MoveCursor(recordedAction.position);

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
        playbackCursor.gameObject.SetActive(false);
    }

    /*private void MoveCursor(Vector2 position)
    {
        cursorRectTransform.position = position;
    }*/


    private void SaveRecording()
    {
        if (isRecording)
        {
            string filePath = Application.persistentDataPath + "/" + recordingName + ".txt";
            List<string> lines = new List<string>();

            foreach (RecordedAction recordedAction in recordedActions)
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

}


