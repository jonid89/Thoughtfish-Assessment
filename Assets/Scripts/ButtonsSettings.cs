using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ButtonsSettings : MonoBehaviour
{
    
    [System.Serializable] public class MyButton{
        public ColorChoice buttonColor;
        public Sprite buttonImage;
        public int amount;
    }
    
    public enum ColorChoice
    {
        Red=0,
        Green=10,
        Blue=20
    }

    public List<MyButton> myButtons = new List<MyButton>();

    public Dictionary<ColorChoice, Color> colorConfig = new Dictionary<ColorChoice, Color>(){
        {ColorChoice.Red, Color.red},
        {ColorChoice.Green, Color.green},
        {ColorChoice.Blue, Color.blue}
    };


#region Singleton
    public static ButtonsSettings Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

#endregion

}
