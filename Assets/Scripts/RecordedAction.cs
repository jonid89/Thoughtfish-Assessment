using UnityEngine;

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
        this.isLeftClickHold = isLeftClickHold;
        this.isLeftClickUp = isLeftClickUp;
        this.isRightClickDown = isRightClickDown;
        this.isRightClickUp = isRightClickUp;
    }
}
