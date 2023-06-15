using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class ButtonView : MonoBehaviour, IDisposable
{
    [SerializeField] private Text text; 
    [SerializeField] private Button button;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private Image image;

    public Button myButton => button; 
    public int buttonNumber;

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

    public void SetColor(Color color){
        var buttonColors = button.colors;
        buttonColors.normalColor = color;
        buttonColors.highlightedColor = color;
        buttonColors.selectedColor = color;
        button.colors = buttonColors;
    }

    public void SetSprite(Sprite sprite){
        image.sprite = sprite;
    }

    public void Dispose(){

    }
    
    public class Factory : PlaceholderFactory<ButtonView>
    {
    }
    
}
