using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject screenBlocker; 
    [SerializeField] private GameObject panel; 
    [SerializeField] private Text text; 

#region Singleton
    public static Popup Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
#endregion

    public void OpenPopup(int number){
        Debug.Log("OpenPopup() called");
        screenBlocker.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        text.text = "Popup was opened by Button " + number;
    }
    
}
