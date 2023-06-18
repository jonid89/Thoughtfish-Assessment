using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityCursorControl;
using UnityEngine.InputSystem;

public class Recorder : MonoBehaviour
{
    private bool isRecording = false;
    public bool IsRecording => isRecording;

    private List<RecordedAction> recordedActions = new List<RecordedAction>();
    public Text nameText;
    private string recordingName;

    public RectTransform cursorRectTransform;
    private Vector2 originalCursorPosition;

    [System.Serializable]
    public struct RecordedAction
    {
        public float time;
        public Vector2 position;
        public bool isLeftClick;
        public bool isRightClick;
        public bool isDrag;

        public RecordedAction(float time, Vector2 position, bool isLeftClick, bool isRightClick, bool isDrag)
        {
            this.time = time;
            this.position = position;
            this.isLeftClick = isLeftClick;
            this.isRightClick = isRightClick;
            this.isDrag = isDrag;
        }
    }

    private void Start()
    {
        recordedActions = new List<RecordedAction>();
        originalCursorPosition = cursorRectTransform.anchoredPosition;
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
        recordingName = nameText.text.ToString();
        cursorRectTransform.gameObject.SetActive(true);
        StartCoroutine(PlayRecordingCoroutine(recording));
    }

    public void LoadRecordingFromFile()
    {
        string filePath = Application.persistentDataPath + "/" + recordingName + ".txt";
        Debug.Log("File Path: " + filePath);
        string[] lines = System.IO.File.ReadAllLines(filePath);
        List<RecordedAction> loadedRecording = new List<RecordedAction>();

        foreach (string line in lines)
        {
            string[] data = line.Split(',');
            if (data.Length == 6 && float.TryParse(data[0], out float time) &&
                float.TryParse(data[1], out float x) && float.TryParse(data[2], out float y) &&
                bool.TryParse(data[3], out bool isLeftClick) && bool.TryParse(data[4], out bool isRightClick) &&
                bool.TryParse(data[5], out bool isDrag))
            {
                Vector2 position = new Vector2(x, y);
                RecordedAction recordedAction = new RecordedAction(time, position, isLeftClick, isRightClick, isDrag);
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
        bool isLeftClick = Input.GetMouseButtonDown(0);
        bool isRightClick = Input.GetMouseButtonDown(1);
        bool isDrag = Input.GetMouseButton(0);

        RecordedAction recordedAction = new RecordedAction(time, position, isLeftClick, isRightClick, isDrag);
        recordedActions.Add(recordedAction);
    }

    private System.Collections.IEnumerator PlayRecordingCoroutine(List<RecordedAction> recording)
    {
        Debug.Log("Playing recording...");

        foreach (RecordedAction recordedAction in recording)
        {
            yield return new WaitForSeconds(recordedAction.time - Time.time);
            
            MoveCursor(recordedAction.position);

            if (recordedAction.isLeftClick)
            {
                ClickAction(recordedAction.position);
            }
            else if (recordedAction.isRightClick)
            {
                RightClickAction(recordedAction.position);
            }
            else if (recordedAction.isDrag)
            {
                DragAction(recordedAction.position);
            }
        }
    }

private void ClickAction(Vector2 position)
{
    GameObject targetObject = GetGameObjectUnderCursor(position);
    if (targetObject != null)
    {
        ExecuteEvents.Execute(targetObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
    }
}


private void RightClickAction(Vector2 position)
{
    GameObject targetObject = GetGameObjectUnderCursor(position);
    Debug.Log("RC object: " + targetObject.name);
    if (targetObject != null)
    {
        ExecuteEvents.Execute(targetObject, new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Right }, ExecuteEvents.pointerClickHandler);
    }
}

private void DragAction(Vector2 position)
{
    GameObject targetObject = GetGameObjectUnderCursor(position);
    if (targetObject != null)
    {
        ExecuteEvents.Execute(targetObject, new PointerEventData(EventSystem.current), ExecuteEvents.beginDragHandler);
        ExecuteEvents.Execute(targetObject, new PointerEventData(EventSystem.current), ExecuteEvents.dragHandler);
        ExecuteEvents.Execute(targetObject, new PointerEventData(EventSystem.current), ExecuteEvents.endDragHandler);
    }
}


   
private GameObject GetGameObjectUnderCursor(Vector2 position)
{
    PointerEventData eventData = new PointerEventData(EventSystem.current);
    eventData.position = position;

    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventData, results);

    foreach (RaycastResult result in results)
    {
        GameObject targetObject = result.gameObject;
        Button button = targetObject.GetComponentInParent<Button>();
        if (button != null)
        {
            return targetObject;
        }
    }

    return null;
}




    private void MoveCursor(Vector2 position)
    {
        cursorRectTransform.position = position;
        //CursorControl.SetLocalCursorPos(position);
    }

#region old Click Action Methods
/*
    private void ClickAction(Vector2 position)
    {
        MoveCursor(position);
        ExecuteEvents.Execute<IPointerClickHandler>(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
        //CursorControl.SimulateLeftClick();
    }

    private void RightClickAction(Vector2 position)
    {
        MoveCursor(position);
        //CursorControl.SimulateRightClick();
    }

    private void DragAction(Vector2 position)
    {
        MoveCursor(position);
        //CursorControl.SimulateLeftClick();
    }
*/
#endregion

    private void SaveRecording()
    {
        if (isRecording)
        {
            string filePath = Application.persistentDataPath + "/" + recordingName + ".txt";
            List<string> lines = new List<string>();

            foreach (RecordedAction recordedAction in recordedActions)
            {
                string line = string.Format("{0},{1},{2},{3},{4},{5}",
                    recordedAction.time, recordedAction.position.x, recordedAction.position.y,
                    recordedAction.isLeftClick, recordedAction.isRightClick, recordedAction.isDrag);
                lines.Add(line);
            }

            System.IO.File.WriteAllLines(filePath, lines.ToArray());
            Debug.Log("Recording saved to file: " + filePath);
        }
    }

}
