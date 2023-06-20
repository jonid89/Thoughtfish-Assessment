using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Zenject;

public class Recorder : MonoBehaviour
{
    private bool isRecording = false;
    public Text nameText;
    private string recordingName;
    private GameEvents gameEvents;

    [Inject]
    StartRecording _startRecording;
    [Inject]
    SaveRecording _saveRecording;
    [Inject]
    LoadRecording _loadRecording;

#region Singleton
    public static Recorder Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
#endregion

    public Recorder(StartRecording startRecording, SaveRecording saveRecording, LoadRecording loadRecording)
    {
        _startRecording = startRecording;
        _saveRecording = saveRecording;
        _loadRecording = loadRecording;
    }

    private void Start()
    {
        gameEvents = GameEvents.Instance;
    }

    private void Update()
    {   
        if (isRecording)
        {
            _startRecording.RecordAction();
        }
    }

    public void StartRecording()
    {
        isRecording = true;
        recordingName = nameText.text.ToString();
        _startRecording.StartRecordingMethod(recordingName);
    }

    public void StopRecording()
    {
        _startRecording.StopRecording();
        isRecording = false;
    }

    public void LoadRecordingFromFile()
    {
        recordingName = nameText.text.ToString();
        _loadRecording.LoadRecordingFromFile(recordingName);
    }
}


