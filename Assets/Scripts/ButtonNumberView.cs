using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class ButtonNumberView : MonoBehaviour, IDisposable
{
    [SerializeField] private Text text; 
    [SerializeField] private Button button;
    [SerializeField] private GameObject tooltip;

    public Button myButton => button; 
    private int buttonNumber;

    private void Start()
    {
        text.text = "Button " + buttonNumber;
    }

    public void SetNumber(int number){
        buttonNumber =  number;
    }

    public void SetPosition(Vector2 positionInGrid){
        this.transform.position = positionInGrid + new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f);
    }

    public void Dispose(){

    }
    
    public class Factory : PlaceholderFactory<ButtonNumberView>
    {
    }
    
}
