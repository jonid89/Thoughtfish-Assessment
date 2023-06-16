using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class ButtonView : MonoBehaviour, IDisposable, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text text; 
    [SerializeField] private Button button;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private Image image;

    public Button myButton => button; 
    public Color myColor;
    public int buttonNumber;
    public bool pointerOnButton;

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
        myColor = color;
        var buttonColors = button.colors;
        buttonColors.normalColor = myColor;
        buttonColors.highlightedColor = myColor;
        buttonColors.selectedColor = myColor;
        button.colors = buttonColors;
    }

    public void SetSprite(Sprite sprite){
        image.sprite = sprite;
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerOnButton = true;
    }

        public void OnPointerExit(PointerEventData eventData)
    {
        pointerOnButton = false;
    }

    public void Dispose(){

    }

    public class Factory : PlaceholderFactory<ButtonView>
    {
    }
    
}
